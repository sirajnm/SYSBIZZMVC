using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;
using MetroFramework;
using MetroFramework.Forms;
using System.Drawing.Printing;

namespace Sys_Sols_Inventory.POSDESK
{
    public partial class POS_Setup_Form : MetroForm
    {

        POS_Setup POSSetup = new POS_Setup();
        
        BindingList<POS_ItemMenu> positemmenus = new BindingList<POS_ItemMenu>();
        BindingList<POS_GeneratedMenu> generatedMenus = new BindingList<POS_GeneratedMenu>();

        
        private void SaveButton_Click(object sender, EventArgs e)
        {
            POSSetup.LocalStoreNo = this.LocalStoreTextBox.Text;
            POSSetup.IsMultiTill = Convert.ToBoolean(this.multitilltogle.CheckState);
            POSSetup.IsSingleShift = Convert.ToBoolean(this.singleshifttoggle.CheckState);
            POSSetup.DefaultTill = defaultTillComboBox.SelectedValue.ToString();
            POSSetup.CashierID = cashieridtextbox.Text;
            POSSetup.CashierPassword = cashierpasswordtextbox.Text;
            POSSetup.POSPrinter = PosPrintersList.Text;
            POSSetup.ReceiptHeader1 = this.ReceiptHeader1Textbox.Text;
            POSSetup.ReceiptHeader2 = this.ReceiptHeader2Textbox.Text;
            POSSetup.ReceiptHeader3 = this.ReceiptHeader3Textbox.Text;
            POSSetup.ReceiptFooter1 = this.ReceiptFooter1Textbox.Text;
            POSSetup.ReceiptFooter2 = this.ReceiptFooter2Textbox.Text;
            POSSetup.ReceiptFooter3 = this.ReceiptFooter3Textbox.Text;
            POSSetup.Pagewidth = Convert.ToSingle(this.PageWidthTextBox.Text);
            POSSetup.PageHeight = Convert.ToSingle(this.PageHeightTextBox.Text);

            if (POSSetup.InsertUpdateMode == "Update")
            {
               if(POSSetup.Update() >= 1)
                {
                    MetroFramework.MetroMessageBox.Show(this, "Updated.", "POS Setup", MessageBoxButtons.OK);
                    
                }
            }
            if(POSSetup.InsertUpdateMode == "Insert")
            {
                if (POSSetup.Update() >= 1)
                {
                    MetroFramework.MetroMessageBox.Show(this, "Added.", "POS Setup", MessageBoxButtons.OK);
                    POSSetup.InsertUpdateMode = "Update";
                }



            }

        }

        private void UpdatetoItemMenu_Click(object sender, EventArgs e)
        {
           
            foreach (POS_GeneratedMenu generatedmenu in generatedMenus.Where(g => g.Available == true))
            {
                POS_ItemMenu itemMenu = new POS_ItemMenu();
                itemMenu.ItemNo = generatedmenu.CODE;
                itemMenu.MenuDescription = generatedmenu.DESC_ENG;
                itemMenu.UOM = generatedmenu.UNIT_CODE;
                itemMenu.Quantity = generatedmenu.PACK_SIZE;
                itemMenu.Barcode = generatedmenu.BARCODE;
                itemMenu.ItemCategory = generatedmenu.CATEGORY;
                itemMenu.MenuState = "Add";
                positemmenus.Add(itemMenu);
            }
         if (   POS_Repositery.UpdateItemMenus(positemmenus.ToList() )>= 1)
            {
                positemmenus = new BindingList<POS_ItemMenu>(POS_Repositery.GetItemMenus());
                generatedMenus = new BindingList<POS_GeneratedMenu>(POS_Repositery.GenerateItemMenus().Where(a => a.Available == false).ToList());
                generatedmenugrid.DataSource = generatedMenus;
                generatedmenugrid.Refresh();
                this.Refresh();
            }

        }

        public POS_Setup_Form()
        {
            InitializeComponent();
         //   this.Cursor = Cursors.Default;
            
            string tillquery = "Select * from POS_Tills";
            DataTable till = DbFunctions.GetDataTable(tillquery);
            foreach(DataRow dr in till.Rows)
            {
                POSSetup.Tills.Add(dr["TillID"].ToString());
            }
            this.defaultTillComboBox.DataSource = POSSetup.Tills;
            this.LocalStoreTextBox.Text = POSSetup.LocalStoreNo;
            this.singleshifttoggle.CheckState = (POSSetup.IsSingleShift ? CheckState.Checked : CheckState.Unchecked);
            this.multitilltogle.CheckState = (POSSetup.IsMultiTill ? CheckState.Checked : CheckState.Unchecked);
            this.cashieridtextbox.Text = POSSetup.CashierID;
            this.cashierpasswordtextbox.Text = POSSetup.CashierPassword;
            
            foreach (String printer in PrinterSettings.InstalledPrinters)
            {
                PosPrintersList.Items.Add(printer.ToString());
            }
            this.PosPrintersList.SelectedText = POSSetup.POSPrinter;
            this.PosPrintersList.Text = POSSetup.POSPrinter;
            this.ReceiptHeader1Textbox.Text = POSSetup.ReceiptHeader1;
            this.ReceiptHeader2Textbox.Text = POSSetup.ReceiptHeader2;
            this.ReceiptHeader3Textbox.Text = POSSetup.ReceiptHeader3;
            this.ReceiptFooter1Textbox.Text = POSSetup.ReceiptFooter1;
            this.ReceiptFooter2Textbox.Text = POSSetup.ReceiptFooter2;
            this.ReceiptFooter3Textbox.Text = POSSetup.ReceiptFooter3;
            this.PageWidthTextBox.Text = POSSetup.Pagewidth.ToString("#.##");
            this.PageHeightTextBox.Text = POSSetup.PageHeight.ToString("#.##");


            generatedMenus = new BindingList<POS_GeneratedMenu>( POS_Repositery.GenerateItemMenus().Where(a => a.Available == false).ToList());
            generatedmenugrid.AutoGenerateColumns = false;
            generatedmenugrid.DataSource = generatedMenus;
            generatedmenugrid.Font = new Font("Segoe UI Light", 12);
            generatedmenugrid.Columns.Add("desceng", "Description");
            generatedmenugrid.Columns.Add("category", "Category");
            generatedmenugrid.Columns.Add("uom", "UOM");
            generatedmenugrid.Columns.Add("qty", "Quantity");
            DataGridViewCheckBoxColumn availablecolumn = new DataGridViewCheckBoxColumn();
            availablecolumn.HeaderText = "Available";
            generatedmenugrid.Columns.Add(availablecolumn);
            generatedmenugrid.Columns[0].DataPropertyName = "DESC_ENG";
            generatedmenugrid.Columns[0].Width = 200;
            generatedmenugrid.Columns[1].DataPropertyName = "CATEGORY";
            generatedmenugrid.Columns[2].DataPropertyName = "UNIT_CODE";
            generatedmenugrid.Columns[3].DataPropertyName = "PACK_SIZE";
            generatedmenugrid.Columns[4].DataPropertyName = "Available";

            positemmenus = new BindingList<POS_ItemMenu>( POS_Repositery.GetItemMenus());
            itemmenugrid.AutoGenerateColumns = false;
            itemmenugrid.DataSource = positemmenus;
            itemmenugrid.Font = new Font("Segoe UI Light", 12);
            
            itemmenugrid.Columns.Add("itemcategory", "Category");
            itemmenugrid.Columns.Add("menudescription", "Menu Description");
            itemmenugrid.Columns.Add("uom", "UOM");
            itemmenugrid.Columns.Add("quantity", "Quantity");

            itemmenugrid.Columns[0].DataPropertyName = "ItemCategory";
            itemmenugrid.Columns[0].Width = 100;
            itemmenugrid.Columns[1].DataPropertyName = "MenuDescription";
            itemmenugrid.Columns[1].Width = 250;
            itemmenugrid.Columns[2].DataPropertyName = "UOM";
            
            itemmenugrid.Columns[3].DataPropertyName = "Quantity";



        }
    }
}
