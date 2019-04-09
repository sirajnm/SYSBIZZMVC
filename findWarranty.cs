using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public partial class findWarranty : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        clsCommon clsCommon = new clsCommon();

        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        public findWarranty()
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }

        private void findWarranty_Load(object sender, EventArgs e)
        {
            ActiveControl = txtSerialNo;
        }

        //public void BindSales()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        conn.Open();
        //        cmd.CommandText = "SELECT INV_SALES_HDR.DOC_NO, INV_SALES_HDR.DOC_TYPE as 'Doc Type',INV_SALES_HDR.DOC_DATE_GRE as Date, REC_CUSTOMER.DESC_ENG as Customer, INV_SALES_HDR.TAX_TOTAL as 'Tax Total', INV_SALES_HDR.DISCOUNT, INV_SALES_HDR.NET_AMOUNT as 'Net Amount',INV_SALES_DTL.ITEMCODE FROM            INV_SALES_DTL LEFT OUTER JOIN INV_SALES_HDR ON INV_SALES_DTL.DOC_NO = INV_SALES_HDR.DOC_NO LEFT OUTER JOIN REC_CUSTOMER ON INV_SALES_HDR.CUSTOMER_CODE = REC_CUSTOMER.CODE WHERE        (INV_SALES_DTL.SERIALNO = '" + txtSerialNo.Text + "')";
        //        adapter.Fill(dt);
        //        drgsales.DataSource = dt;
        //        conn.Open();
        //    }
        //    catch
        //    {
        //        conn.Close();
        //    }
        //}

        //public void BindPurchaseDetails()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        conn.Open();
        //        cmd.CommandText = "SELECT INV_PURCHASE_HDR.DOC_NO, INV_PURCHASE_HDR.DOC_TYPE as 'Doc Type', PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, INV_PURCHASE_HDR.DOC_DATE_HIJ as 'Hij Date', INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Total', INV_PURCHASE_HDR.GROSS as 'Gross Total', INV_PURCHASE_DTL.SERIALNO,INV_PURCHASE_DTL.ITEMCODE FROM            INV_PURCHASE_DTL LEFT OUTER JOIN  INV_PURCHASE_HDR ON INV_PURCHASE_DTL.DOC_NO = INV_PURCHASE_HDR.DOC_NO LEFT OUTER JOIN  PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE        (INV_PURCHASE_DTL.SERIALNO = '"+txtSerialNo.Text+"')";
        //        adapter.Fill(dt);
        //        drgPurchase.DataSource = dt;
        //        conn.Open();
        //    }
        //    catch
        //    {
        //        conn.Close();
        //    }
        //}

        bool HASSWARRANTY = false;

        public void GetItemWarranty()
        {
            try
            {
                string itemcode = drgPurchase.Rows[0].Cells["ITEMCODE"].Value.ToString();
               // conn.Open();
               // cmd.CommandText = "SELECT PERIOD,PERIODTYPE,HASWARRENTY FROM INV_ITEM_DIRECTORY WHERE CODE='" + itemcode + "'";
                SqlDataReader RD;
               // RD = cmd.ExecuteReader();
                RD = clsCommon.GetItemWarranty(itemcode);
                while (RD.Read())
                {
                    Wvalue.Text = RD[0].ToString();
                    WType.Text = RD[1].ToString();
                    HASSWARRANTY = Convert.ToBoolean(RD[2]);
                }
                DbFunctions.CloseConnection();
            }
            catch
            {
            }
        }


        public void CalculateWarrantyendson()
        {
            try
            {
                if (WType.Text == "Year")
                {
                  lblwarretyends.Text= (Convert.ToDateTime(lblSaledon.Text).AddYears(Convert.ToInt32(Wvalue.Text))).ToShortDateString();
                }

            }
            catch
            {
            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtSerialNo.Text != "")
                {
                    //BindPurchaseDetails();
                    //BindSales();
                    drgPurchase.DataSource = clsCommon.BindPurchase_Warranty(txtSerialNo.Text);
                    drgsales.DataSource= clsCommon.BindSales_Warranty(txtSerialNo.Text);

                    if(drgsales.Rows.Count>0)
                    {
                        lblSaledon.Text = drgsales.Rows[0].Cells["Date"].Value.ToString();
                    }

                    if (HASSWARRANTY)
                    {
                        GetItemWarranty();
                        CalculateWarrantyendson();
                    }


                }
                else
                {
                    MessageBox.Show("Please Enter Serial No");
                }
            }
            catch
            {
            }




        }

        private void txtSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (Keys.Enter | Keys.Tab))
            {
                btnFind.Focus();
            }
        }

        private void drgPurchase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string docno = drgPurchase.CurrentRow.Cells["DOC_NO"].Value.ToString();
                PurchaseMaster pm = new PurchaseMaster(docno);

                if (lg.Theme == "1")
                {
                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                    mdi.maindocpanel.Pages.Add(kp);

                    pm.Show();
                    pm.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(pm);
                    pm.Dock = DockStyle.Fill;
                    kp.Text = pm.Text;
                    kp.Name = "Purchase of "+ docno;
                    // kp.Focus();
                    pm.FormBorderStyle = FormBorderStyle.None;

                    mdi.maindocpanel.SelectedPage = kp;
                    mdi.onlyhide();
                }
                else
                {
                    pm.ShowDialog();
                }
                
            }
            catch
            {
            }
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
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

        private void drgsales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string docno = drgsales.CurrentRow.Cells["DOC_NO"].Value.ToString();
                SalesQ Sal = new SalesQ(docno);

                if (lg.Theme == "1")
                {
                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                    mdi.maindocpanel.Pages.Add(kp);

                    Sal.Show();
                    Sal.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(Sal);
                    Sal.Dock = DockStyle.Fill;
                    kp.Text = Sal.Text;
                    kp.Name = "Purchase of " + docno;
                    // kp.Focus();
                    Sal.FormBorderStyle = FormBorderStyle.None;

                    mdi.maindocpanel.SelectedPage = kp;
                    mdi.onlyhide();
                }
                else
                {
                    Sal.ShowDialog();
                }

            }
            catch
            {
            }
        }
    }
}
