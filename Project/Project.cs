using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Project
{
    
    public partial class Project : Form
    {
        ProjectDB ProjectDB = new ProjectDB();
        BindingSource bs = new BindingSource();

        public Project()
        {
            InitializeComponent();
        }

        private void Project_Load(object sender, EventArgs e)
        {
            loadStates();
            clear();
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.Customer);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                CUSTOMER_CODE.Text = Convert.ToString(h.c[0].Value);
                CUSTOMER_NAME.Text = Convert.ToString(h.c[1].Value);
            }
            CUSTOMER_NAME.Focus();
        }
        private void loadStates()
        {
            cmbState.DataSource = Common.loadStates();
            cmbState.DisplayMember = "NAME";
            cmbState.ValueMember = "CODE";
        }

        private void btnaddcategory_Click(object sender, EventArgs e)
        {
            CustomerMaster cm = new CustomerMaster();
            if (cm.ShowDialog() != DialogResult.Yes)
            {
                
            }
        }
        public void clear()
        {
            DataTable dt = ProjectDB.AllData();
            bs.DataSource = dt;
            dgv_project.DataSource = bs;

            txt_pid.Text = (ProjectDB.maxId()).ToString();
            
            txt_name.Text = "";
            CUSTOMER_CODE.Text = "";
            CUSTOMER_NAME.Text = "";
            txt_address.Text = "";
            txt_location.Text="";
            txt_income.Text = "0.00";
            txt_expense.Text = "0.00";
            txt_estimate.Text = "0.00";
            cmb_status.SelectedIndex = -1;
            ch_active.Checked = true;
            txt_name.Focus();
        }

        public bool Valid()
        {
            if (txt_pid.Text == "")
            {
                MessageBox.Show("Invalid Id","Sysbizz",MessageBoxButtons.OK,MessageBoxIcon.Error);                
                return false;
            }
            else if (txt_name.Text == "")
            {
                MessageBox.Show("Invalid Project name", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_name.Focus();
                return false;
            }
            else if (CUSTOMER_CODE.Text == "" || CUSTOMER_NAME.Text == "")
            {
                MessageBox.Show("Select A valid Customer.", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CUSTOMER_CODE.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                ProjectDB.Id = Convert.ToInt32(txt_pid.Text);
                if (ProjectDB.CheckProjectExist())
                {
                    DialogResult dr=MessageBox.Show("Are sure to Update the Project Information..?","Sysbizz",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        ProjectDB.Id = Convert.ToInt32(txt_pid.Text);
                        ProjectDB.DescEng = txt_name.Text;
                        ProjectDB.Customer = Convert.ToInt32(CUSTOMER_CODE.Text);
                        ProjectDB.Location = txt_location.Text;
                        ProjectDB.State = cmbState.SelectedValue.ToString();
                        ProjectDB.SiteAddress = txt_address.Text;
                        ProjectDB.EstimateAmt = txt_estimate.Text == "" ? Convert.ToDecimal("0.00") : Convert.ToDecimal(txt_estimate.Text);
                        ProjectDB.Status = cmb_status.Text == "" ? "Initial" : cmb_status.Text;                        
                        ProjectDB.LastUpdated = DateTime.Now;
                        ProjectDB.Active = ch_active.Checked;
                        if (ProjectDB.Update_Project() >= 1)
                        {
                            MessageBox.Show("Project Updated Successfully..!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Error in Updation...!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    ProjectDB.Id = Convert.ToInt32(txt_pid.Text);
                    ProjectDB.DescEng = txt_name.Text;
                    ProjectDB.Customer = Convert.ToInt32(CUSTOMER_CODE.Text);
                    ProjectDB.Location = txt_location.Text;
                    ProjectDB.State = cmbState.SelectedValue.ToString();
                    ProjectDB.SiteAddress = txt_address.Text;
                    ProjectDB.EstimateAmt = txt_estimate.Text == "" ? Convert.ToDecimal("0.00") : Convert.ToDecimal(txt_estimate.Text);
                    ProjectDB.Status = cmb_status.Text==""?"Initial":cmb_status.Text;
                    ProjectDB.EntryDate = DateTime.Now;
                    ProjectDB.LastUpdated = DateTime.Now;
                    ProjectDB.Active = ch_active.Checked;

                    if (ProjectDB.Insert_Project() >= 1)
                    {
                        MessageBox.Show("Project Inserted Successfully..!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Error Inserting Project...!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);                        
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void txt_estimate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && (txt_estimate.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cmb_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnCust.Focus();
            }
        }

        private void CUSTOMER_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txt_address.Focus();
            }
        }

        private void txt_location_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbState.Focus();
            }
        }

        private void cmbState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txt_estimate.Focus();
            }
        }

        private void txt_estimate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmb_status.Focus();
            }
        }

        private void cmb_status_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }

        private void txt_address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txt_location.Focus();
            }
        }

        private void dgv_project_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgv_project.SelectedRows.Count > 0)
            {
                txt_pid.Text = dgv_project.SelectedRows[0].Cells["Id"].Value.ToString();
                txt_name.Text = dgv_project.SelectedRows[0].Cells["PROJECT NAME"].Value.ToString();
                CUSTOMER_CODE.Text = dgv_project.SelectedRows[0].Cells["CUST CODE"].Value.ToString();
                CUSTOMER_NAME.Text = dgv_project.SelectedRows[0].Cells["CUSTOMER NAME"].Value.ToString();
                cmbState.SelectedValue = dgv_project.SelectedRows[0].Cells["STATE"].Value;
                cmb_status.Text = dgv_project.SelectedRows[0].Cells["Status"].Value.ToString();
                txt_estimate.Text = dgv_project.SelectedRows[0].Cells["Estimated Budget"].Value.ToString();
                ch_active.Checked= Convert.ToBoolean(dgv_project.SelectedRows[0].Cells["ACTIVE"].Value)==true?true:false;
                txt_location.Text = dgv_project.SelectedRows[0].Cells["Location"].Value.ToString();
                txt_address.Text = dgv_project.SelectedRows[0].Cells["SiteAddress"].Value.ToString();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are Sure to Delete this Project..? \n It is Not Reversible..!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                if (ProjectDB.UsedProjects())
                {
                    MessageBox.Show("Cant Delete this project. Some Transaction Exist..!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ProjectDB.Id=Convert.ToInt32 (txt_pid.Text);
                    if (ProjectDB.DeleteProject())
                    {
                        MessageBox.Show("Project Deleted Successfully.", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Error Delete this project.!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
