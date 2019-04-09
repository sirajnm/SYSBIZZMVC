using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.reports
{
    public partial class Item_Price_List : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private BindingSource source = new BindingSource();
        Class.Stock_Report stkrpt = new Class.Stock_Report();
        DataTable dt = new DataTable();
        public Item_Price_List()
        {
            InitializeComponent();
        }

        string type, cate, group, tm,UOM,Desc_Name,saltype;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridItem.Visible = false;
          //  if (txtCode.Text == "")
           // {
           // string conection = Properties.Settings.Default.ConnectionStrings.ToString();
           // Sp_itempricelistTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(conection);
            Sp_itempricelistTableAdapter.Connection = Model.DbFunctions.GetConnection();

                if (TYPE.Text != "" || DrpCategory.Text != "" || Group.Text != "" || Trademark.Text != "" || DrpUOM.Text != "" || DESC_ENG.Text != ""||Drpsaltype.Text!="")
                {
                    type = Convert.ToString(TYPE.SelectedValue);
                    cate = Convert.ToString(DrpCategory.SelectedValue);
                    group = Convert.ToString(Group.SelectedValue);
                    tm = Convert.ToString(Trademark.SelectedValue);
                    UOM = Convert.ToString(DrpUOM.SelectedValue);
                    Desc_Name = DESC_ENG.Text;
                    saltype =Convert.ToString(Drpsaltype.SelectedValue);
                    this.Sp_itempricelistTableAdapter.Fillitempricelist(this.PRICELISTDATASET.Sp_itempricelist, type, cate, group, tm, UOM, Desc_Name,saltype);
                    this.reportViewer1.RefreshReport();
              //  }
              //  else
              //  {
                   // MessageBox.Show("Please select a filter option");
               // }
            }
                else if (DESC_ENG.Text == "")
                {
                    type = "";
                    cate = "";
                    group = "";
                    tm = "";
                    UOM = "";
                    Desc_Name = "";
                    saltype  ="";
                    try
                    {
                        this.Sp_itempricelistTableAdapter.Fillitempricelist(this.PRICELISTDATASET.Sp_itempricelist, type, cate, group, tm, UOM, Desc_Name, saltype);
                        this.reportViewer1.RefreshReport();
                    }
                    catch
                    {
                    }
                }
                else
                {
                    this.Sp_itempricelistTableAdapter.FillByItemCode(this.PRICELISTDATASET.Sp_itempricelist, txtCode.Text);
                    this.reportViewer1.RefreshReport();
                }
        }

        private void Item_Price_List_Load(object sender, EventArgs e)
        {

            BindType();
            BindGroup();
            BindCategory();
            BindTradeMark();
            BindUOM();
            bindPriceType();
            bindgridview();
        }

        public void bindPriceType()
        {
            try
            {
                dt = stkrpt.BindPriceType();



                Drpsaltype.ValueMember = "CODE";
                Drpsaltype.DisplayMember = "DESC_ENG";


                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                Drpsaltype.DataSource = dt;
            }
            catch
            {
            }
        }

        public void bindgridview()
        {
            try
            {
                //cmd.Connection = conn;
                //cmd.CommandText = "SELECT        INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name] FROM   INV_ITEM_DIRECTORY ";
                //cmd.CommandType = CommandType.Text;
                //DataTable dt = new DataTable();
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);
                source.DataSource = stkrpt.bindItemName();

                dataGridItem.DataSource = source;

                dataGridItem.RowHeadersVisible = false;
                dataGridItem.Columns[0].Width = 200;
           
                dataGridItem.ClearSelection();

            }
            catch
            {
            }


        }





        public void BindUOM()
        {
            try
            {
                dt = stkrpt.BindUOM();



                DrpUOM.ValueMember = "CODE";
                DrpUOM.DisplayMember = "DESC_ENG";


                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                DrpUOM.DataSource = dt;
            }
            catch
            {
            }
        }
        public void BindType()
        {
            try
            {
                dt = stkrpt.BindType();



                TYPE.ValueMember = "CODE";
                TYPE.DisplayMember = "DESC_ENG";


                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                TYPE.DataSource = dt;
            }
            catch
            {
            }
        }
        public void BindCategory()
        {

            try
            {
                dt = stkrpt.BindCategory();

                DrpCategory.ValueMember = "CODE";
                DrpCategory.DisplayMember = "DESC_ENG";

                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                DrpCategory.DataSource = dt;

            }
            catch
            {
            }
        }
        public void BindGroup()
        {

            try
            {
                dt = stkrpt.BindGroup();

                Group.ValueMember = "CODE";
                Group.DisplayMember = "DESC_ENG";

                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                Group.DataSource = dt;
            }
            catch
            {
            }
        }
        public void BindTradeMark()
        {
            try
            {
                dt = stkrpt.BindTrademark();

                Trademark.ValueMember = "CODE";
                Trademark.DisplayMember = "DESC_ENG";

                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                Trademark.DataSource = dt;
            }
            catch
            {
            }
        }

        private void kryptonLabel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
        }

        private void DESC_ENG_TextChanged(object sender, EventArgs e)
        {
            if (DESC_ENG.Text != "")
            {
                dataGridItem.Visible = true;
            }
            else
                dataGridItem.Visible = false;
        }

        private void kryptonLabel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DrpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
        }

        private void Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
        }

        private void TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
        }

        private void kryptonLabel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonLabel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    DESC_ENG.Text = dataGridItem.CurrentRow.Cells[0].Value.ToString();
                    dataGridItem.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void DESC_ENG_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Down)
            {
                if (dataGridItem.Visible == true)
                {
                    dataGridItem.Focus();
                    dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[0];
                }
                
            }
            else if (e.KeyData == Keys.Escape)
            {
                dataGridItem.Visible = false;
                DESC_ENG.Text = "";
            }
        }

        private void Trademark_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
        }

        private void Drpsaltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
        }

        public void SavePDF(ReportViewer viewer, string savePath)
        {
            try
            {
                //  byte[] Bytes = viewer.LocalReport.Render(format:"WORDOPENXML", deviceInfo: "");
                byte[] Bytes = viewer.LocalReport.Render(format: "EXCELOPENXML", deviceInfo: "");
                // byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);

                }
            }
            catch { }

            try
            {
                if (DialogResult.Yes == MessageBox.Show("Make sure you have configued outlook for sending email", "OutLook Config", MessageBoxButtons.YesNo))
                {
                    Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
                    Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                    // body, bcc etc...

                    oMailItem.Attachments.Add((object)savePath, Microsoft.Office.Interop.Outlook.OlAttachmentType.olEmbeddeditem, 1, (object)"Attachment");
                    oMailItem.Display(true);
                }
            }
            catch
            {
                MessageBox.Show("You may not have Outlook Installed Please check");
            }
        }
        private void Send_Mail_Click(object sender, EventArgs e)
        {

            SavePDF(reportViewer1, Path.GetTempPath() + "PriceList_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");
            //    SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".docx");
            //  SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff")+".pdf");
       
        }

        private void Sp_itempricelistBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

    }
}
