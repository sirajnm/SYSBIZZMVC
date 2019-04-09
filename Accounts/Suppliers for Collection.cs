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

namespace Sys_Sols_Inventory.Accounts
{
    public partial class Suppliers_for_Collection : Form
    {

       // private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
       // private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
       TbTransactionsDB trObj=new TbTransactionsDB ();
       PaySupplierDB paysupobj = new PaySupplierDB();

        public Suppliers_for_Collection()
        {
            InitializeComponent();
        }


        public void GETSUPPLIERS()
        {
            try
            {
                //cmd.Parameters.Clear();
                //cmd.Connection = conn;
                //cmd.CommandText = "GETSUPPLIERS";
                string cmd = "GETSUPPLIERS";
               // cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);
                dt = DbFunctions.GetDataTableProcedure(cmd);
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < (dt.Rows.Count); ++j)
                    {
                        string lid = dt.Rows[j]["LedgerId"].ToString();
                        DataTable tmp = new DataTable();
                        tmp = GetTransactions(lid);
                        dgSuppliers.Rows.Add(tmp.Rows[0][0], tmp.Rows[0][1], tmp.Rows[0][2], tmp.Rows[0][3], tmp.Rows[0][4], tmp.Rows[0][5], dt.Rows[0][0]);


                        //DataRow[] rows;
                        //rows = dt.Select("BALANCE='0.00' ");
                        //foreach (DataRow row in rows)
                        //    dt.Rows.Remove(row);

                    }

                }
                //dgSuppliers.DataSource = dt;

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }







         

        }
        public DataTable GetTransactions( string lid)
        {

            DataTable dt = new DataTable();
            try
            {
            //    if (conn.State == ConnectionState.Open)
            //    {
            //    }
            //    else
            //    {

            //        conn.Open();
            //    }
            //    cmd.Connection = conn;
            //    cmd.CommandType = CommandType.Text;
             //   cmd.CommandText = "SELECT tb_Transactions.ACCID,tb_Transactions.ACCNAME, SUM(tb_Transactions.CREDIT) AS CREDIT, SUM(tb_Transactions.DEBIT) AS DEBIT, tb_Ledgers.UNDER, SUM(tb_Transactions.CREDIT)- SUM(tb_Transactions.DEBIT) AS BALANCE FROM            tb_Transactions INNER JOIN tb_Ledgers ON tb_Transactions.ACCID = tb_Ledgers.LEDGERID GROUP BY  tb_Transactions.ACCID,tb_Transactions.ACCNAME, tb_Ledgers.UNDER HAVING (tb_Ledgers.UNDER = '13') and (tb_Transactions.ACCID= '" + lid + "')";

                trObj.AccId=lid;
                dt= trObj.getDetails();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(dt);
                return dt;


            }
            catch (Exception ex)
            {

            }
            finally
            {
               // conn.Close();

            }
            return dt;
        }
        public DataTable count_due_customers(DateTime date,int ledgerid)
        {
            DataTable dt = new DataTable();            
            trObj.Dated = date;
            trObj.AccId = ledgerid.ToString();
            dt = trObj.getdataCus();
            return dt;
        }

        public DataTable count_SUPPLIER(DateTime date, int ledgerid)
        {
            DataTable dt = new DataTable();
            trObj.Dated = date;
            trObj.AccId = ledgerid.ToString();
            dt = trObj.getDatabyIdAndDate();
            return dt;

        }
        private void Suppliers_for_Collection_Load(object sender, EventArgs e)
        {
            //GETSUPPLIERS();
            GET_SUPP_BALANCE();
        }
        public void GET_SUPP_BALANCE()
        {
            DataTable tmp = new DataTable();
            try
            {
                int indx = 0;
                //cmd.Parameters.Clear();
                //cmd.Connection = conn;
                //cmd.CommandText = "SELECT PAY_SUPPLIER.LedgerId,ISNULL(DEBIT_PERIOD_TYPE,0) AS DEBIT_PERIOD FROM PAY_SUPPLIER LEFT OUTER JOIN TB_LEDGERS ON TB_LEDGERS.LEDGERID=PAY_SUPPLIER.LEDGERID WHERE TB_LEDGERS.UNDER=13";
                //cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);
                dt = paysupobj.getDetailsByUnder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    int creditprd = 0 - Convert.ToInt32(dt.Rows[i]["DEBIT_PERIOD"]);
                    tmp.Clear();
                    tmp = count_SUPPLIER(DateTime.Now.AddDays(creditprd), Convert.ToInt32(dt.Rows[i]["LedgerId"]));
                    if (tmp.Rows.Count > 0)
                    {

                        try
                        {
                            if (Convert.ToDouble(tmp.Rows[0]["BALANCE"]) > 0)
                            {
                                dgSuppliers.Rows.Add();
                                dgSuppliers.Rows[indx].Cells["ACCID"].Value = tmp.Rows[0]["CODE"].ToString();
                                dgSuppliers.Rows[indx].Cells["ACCNAME"].Value = tmp.Rows[0]["DESC_ENG"].ToString();
                                dgSuppliers.Rows[indx].Cells["DEBIT"].Value = tmp.Rows[0]["total_DEBIT"].ToString();
                                dgSuppliers.Rows[indx].Cells["CREDIT"].Value = tmp.Rows[0]["total_CREDIT"].ToString();
                                dgSuppliers.Rows[indx].Cells["BALANCE"].Value = tmp.Rows[0]["BALANCE"].ToString();
                                dgSuppliers.Rows[indx].Cells["CONTACT"].Value = tmp.Rows[0]["MOBILE"].ToString();
                                dgSuppliers.Rows[indx].Cells["cAdd1"].Value = tmp.Rows[0]["ADDRESS_A"].ToString();
                                dgSuppliers.Rows[indx].Cells["cAdd2"].Value = tmp.Rows[0]["ADDRESS_B"].ToString();
                                dgSuppliers.Rows[indx].Cells["cEmail"].Value = tmp.Rows[0]["EMAIL"].ToString();
                                dgSuppliers.Rows[indx].Cells["cTele1"].Value = tmp.Rows[0]["TELE1"].ToString();
                                dgSuppliers.Rows[indx].Cells["cTele2"].Value = tmp.Rows[0]["TELE2"].ToString();
                                indx++;
                            }
                        }
                        catch
                        {

                        }



                    }
                   
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {


            if (keyData == (Keys.Escape))
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnDocNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgSuppliers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int Ledgerid = Convert.ToInt32(dgSuppliers.CurrentRow.Cells["ACCID"].Value.ToString());
                string SupId = dgSuppliers.CurrentRow.Cells["SUPPID"].Value.ToString();
                DueBills DB = new DueBills(Ledgerid, SupId);
                DB.ShowDialog();
                //string LedgerId = dgSuppliers.CurrentRow.Cells["ACCID"].Value.ToString();
                //Accounts.LedgerReport Lgrpt = new LedgerReport(LedgerId);
                //ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                //mdi.maindocpanel.Pages.Add(kp);

                //Lgrpt.Show();
                //Lgrpt.TopLevel = false;
                ////  splitContainer1.Panel2.Controls.Add(ad);
                //kp.Controls.Add(Lgrpt);
                //Lgrpt.Dock = DockStyle.Fill;
                //kp.Text = Lgrpt.Text;
                //kp.Name = "Ledger Report";
                //// kp.Focus();
                //Lgrpt.FormBorderStyle = FormBorderStyle.None;

                //mdi.maindocpanel.SelectedPage = kp;
                //mdi.onlyhide();
            }
            catch
            {
            }
        }

        private void dgSuppliers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    string LedgerId = dgSuppliers.CurrentRow.Cells["ACCID"].Value.ToString();
            //    try
            //    {

            //        cmd.Parameters.Clear();
            //        cmd.Connection = conn;
            //        cmd.CommandText = "SELECT PAY_SUPPLIER.DESC_ENG, PAY_SUPPLIER.ADDRESS_A, PAY_SUPPLIER.ADDRESS_B, PAY_SUPPLIER.TELE1, PAY_SUPPLIER.MOBILE, PAY_SUPPLIER.EMAIL, PAY_SUPPLIER.CITY_CODE, PAY_SUPPLIER.REG_CODE, PAY_SUPPLIER.COUNTRY_CODE, PAY_SUPPLIER.WEB, PAY_SUPPLIER.NOTES, PAY_SUPPLIER.DESC_ARB, PAY_SUPPLIER.TYPE, tb_Ledgers.LEDGERID, GEN_CITY.DESC_ENG AS CITY FROM tb_Ledgers LEFT OUTER JOIN PAY_SUPPLIER ON tb_Ledgers.LEDGERID = PAY_SUPPLIER.LedgerId LEFT OUTER JOIN GEN_CITY ON PAY_SUPPLIER.CITY_CODE = GEN_CITY.CODE WHERE  (tb_Ledgers.LEDGERID = @LEDGERID)";

            //        cmd.CommandType = CommandType.Text;
            //        DataTable dt = new DataTable();
            //        adapter.SelectCommand = cmd;
            //        adapter.SelectCommand.Parameters.AddWithValue("LEDGERID", LedgerId);

            //        adapter.Fill(dt);
            //        if (dt.Rows.Count > 0)
            //        {
            //            lblName.Text = dt.Rows[0]["DESC_ENG"].ToString();
            //            lbladd1.Text = dt.Rows[0]["ADDRESS_A"].ToString();
            //            lbladd2.Text = dt.Rows[0]["CITY"].ToString();
            //            lbltele.Text = dt.Rows[0]["TELE1"].ToString();
            //            lblmob.Text = dt.Rows[0]["MOBILE"].ToString();
            //            lblemail.Text = dt.Rows[0]["EMAIL"].ToString();

            //        }
            //    }
            //    catch (Exception ee)
            //    {
            //        MessageBox.Show(ee.Message);
            //    }

            //}
            //catch
            //{
            //}
            lblName.Text = dgSuppliers.CurrentRow.Cells["ACCNAME"].Value.ToString();
            lbladd1.Text = dgSuppliers.CurrentRow.Cells["cAdd1"].Value.ToString();
            lbladd2.Text = dgSuppliers.CurrentRow.Cells["cAdd2"].Value.ToString();
            if (dgSuppliers.CurrentRow.Cells["cTele2"].Value != "")
            {
                lbltele.Text = dgSuppliers.CurrentRow.Cells["cTele1"].Value.ToString() +","+ dgSuppliers.CurrentRow.Cells["cTele2"].Value.ToString(); 
            }
            else
          lbltele.Text = dgSuppliers.CurrentRow.Cells["cTele1"].Value.ToString() ;
            lblmob.Text = dgSuppliers.CurrentRow.Cells["CONTACT"].Value.ToString();
            lblemail.Text = dgSuppliers.CurrentRow.Cells["cEmail"].Value.ToString();
        }
    }
}
