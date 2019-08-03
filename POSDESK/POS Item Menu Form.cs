using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.POSDESK
{
    public partial class POS_Item_Menu_Form : Form
    {
        List<POS_ItemMenu> itemMenus = new List<POS_ItemMenu>();
        
        public string Barcode
        {
            get; set;
        }
        BindingList<POS_GeneratedMenu> generatedmenus = new BindingList<POS_GeneratedMenu>();
        public POS_Item_Menu_Form()
        {
            InitializeComponent();
            
            
        }

        private void POS_Item_Menu_Form_Load(object sender, EventArgs e)
        {
            generatedmenus = new BindingList<POS_GeneratedMenu>(POS_Repositery.GenerateItemMenus().Where (a => a.Available == true ).ToList());
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = generatedmenus;
            dataGridView1.Font = new Font("Segoe UI Light", 12);
            dataGridView1.Columns.Add("descr", "Description");
            dataGridView1.Columns.Add("category", "Category");
            dataGridView1.Columns.Add("uom", "UOM");
            dataGridView1.Columns.Add("qty", "Quantity");
            dataGridView1.Columns.Add("barcode", "Barcode");
            dataGridView1.Columns[0].DataPropertyName = "DESC_ENG";
            dataGridView1.Columns[0].Width = 300;
            dataGridView1.Columns[1].DataPropertyName = "CATEGORY";
            dataGridView1.Columns[2].DataPropertyName = "UNIT_CODE";
            dataGridView1.Columns[3].DataPropertyName = "PACKET_SIZE";
            dataGridView1.Columns[4].DataPropertyName = "BARCODE";
            dataGridView1.Columns[4].Visible = false;

        }

        private void  ItemMenuFunction(string name)
        {
            this.Barcode = name;
            this.DialogResult = DialogResult.OK;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Barcode = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Barcode = "";
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void metroTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            dataGridView1.DataSource = generatedmenus.Where(a => a.DESC_ENG.Contains(metroTextBox1.Text));
        }
    }

    
}
