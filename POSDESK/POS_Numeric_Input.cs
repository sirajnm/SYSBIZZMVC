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

namespace Sys_Sols_Inventory.POSDESK
{
    public partial class POS_Numeric_Input : Form
    {

        public string numericvalue;
        
        public POS_Numeric_Input()
        {
            InitializeComponent();
            textBox1.Text = numericvalue;
            textBox1.SelectAll();
            Button[] numericbutton = new Button[12];
            List<string> buttonstring = new List<string>(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "Clear", "0", "." });
            int i = 0;
            foreach (string button in buttonstring)
            {
                int x = i;
                numericbutton[x] = new Button();
                numericbutton[x].Text = button;
                numericbutton[x].Name = button;
                numericbutton[x].Width = flowLayoutPanel1.Width / 3 - 10;
                numericbutton[x].Height = flowLayoutPanel1.Height / 4 - 5;
                numericbutton[x].FlatStyle = FlatStyle.Flat;
                numericbutton[x].FlatAppearance.BorderColor = Color.LightGray;
                numericbutton[x].Font = new Font("Segoe UI Light", 13f);
                numericbutton[x].BackColor = Color.White;
                numericbutton[x].ForeColor = Color.Black;
                numericbutton[x].Click += (sender1, Keys) => NumericButtonPressed(numericbutton[x].Name);
                flowLayoutPanel1.Controls.Add(numericbutton[x]);
                i += 1;
            }

        }

        private void NumericButtonPressed(string name)
        {
            if (name == "Clear")
            {
                textBox1.Text = "";
                return;
            }
            if (name == ".")
            {
                if (textBox1.Text.Contains(".")) return;
            }
            textBox1.Text = textBox1.Text + name;
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            numericvalue = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                okbutton.PerformClick();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                cancelbutton.PerformClick();
                return true;
            }

            return false;
        }

        private void POS_Numeric_Input_Load(object sender, EventArgs e)
        {
            textBox1.Text = numericvalue;
        }
    }
}
