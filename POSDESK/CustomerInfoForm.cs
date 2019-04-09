using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using Sys_Sols_Inventory.Model;


namespace Sys_Sols_Inventory.POSDESK
{
    public partial class CustomerInfoForm : MetroForm
    {
        private bool _creditcustomer;
        public string CustomerMobileNo
        {
            get; set;
        }
        public string CustomerName
        {
            get; set;
        }

        public string CustomerAddress1
        {
            get; set;
        }

        public string CustomerAddress2
        {
            get; set;
        }

        public bool IsCreditCustomer
        {
            get { return _creditcustomer; }
            set {
                _creditcustomer = value;

                if (value)
                { CustomerListComboBox.Enabled = true; }
                else 
                 { CustomerListComboBox.Enabled = false; }
                }

        }

        public string CustomerLedger
        {
            get; set;
        }
        List<clsCustomer> CustomerList = POS_Repositery.GetAllCustomers();

        public CustomerInfoForm()
        {
            InitializeComponent();
            IsCreditCustomer = false;

         //  iscreditcustomertoggle.DataBindings.Add("Checked",this,IsCreditCustomer.ToString());
            
            CustomerListComboBox.DataSource = CustomerList;
            CustomerListComboBox.DisplayMember = "DescEng";
            CustomerListComboBox.ValueMember = "LedgerId";


        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            this.CustomerMobileNo = CustomerMobileTextBox.Text;
            this.CustomerName = CustomerNameTextBox.Text;
            this.CustomerAddress1 = Address1TextBox.Text;
            this.CustomerAddress2 = Address2TextBox.Text;
            this.IsCreditCustomer = iscreditcustomertoggle.CheckState == CheckState.Checked ? true : false;
            this.CustomerLedger = CustomerListComboBox.SelectedValue.ToString();
                
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void iscreditcustomertoggle_CheckStateChanged(object sender, EventArgs e)
        {
            IsCreditCustomer = iscreditcustomertoggle.Checked ? true : false;
        }

        private void CustomerListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsCustomer customer = CustomerList.Where(a => a.LedgerId == CustomerListComboBox.SelectedValue.ToString()).FirstOrDefault();
            if (customer != null)
            {
                this.CustomerMobileTextBox.Text = customer.Mobile;
                this.CustomerNameTextBox.Text = customer.DescEng;
                this.Address1TextBox.Text = customer.AddressA;
                this.Address2TextBox.Text = customer.AddressB;
                CustomerLedger = customer.LedgerId;

            }
        }
    }
}
