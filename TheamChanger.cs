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
    public partial class TheamChanger : Form
    {
        public TheamChanger()
        {
            InitializeComponent();
        }
        Class.Login login = new Class.Login();
        Login log = (Login)Application.OpenForms["Login"];
        private void pictureBoxStandard_Click(object sender, EventArgs e)
        {
         if(  MessageBox.Show("Are you sure ? \n ", "Change Theme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
             //   string str = log.Theme;
                string Empid = log.EmpId;
                login.Empid1 =Convert.ToInt16( Empid);
                login.Theam = 1;
                login.UpdateTheam();
                MessageBox.Show("Updated. You must Restart application to affect changes");
                this.Hide();

         }
        }

        private void pictureBoxClassic_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ? \n ", "Change Theme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                //   string str = log.Theme;
                string Empid = log.EmpId;
                login.Empid1 = Convert.ToInt16(Empid);
                login.Theam = 0;
                login.UpdateTheam();
                MessageBox.Show("Updated! You must Restart application to affect changes");
                this.Hide();

            }
        }
    }
}
