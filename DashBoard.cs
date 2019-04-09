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
using System.Runtime.InteropServices;
using System.Threading;

namespace Sys_Sols_Inventory
{
    public partial class DashBoard : Form
    {
        String decimalFormat = "0.0";
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        Class.Transactions Tr = new Class.Transactions();
        Accounts.Suppliers_for_Collection sc = new Accounts.Suppliers_for_Collection();
        private const int _maxNumberOfBlinks = 1000;
        double Liability = 0, Asset = 0;
        private int _blinkCount = 0;
        private const int _blinkFrequency = 500;
        clsDashBoard dash = new clsDashBoard();
        Class.Privilage priv = new Class.Privilage();
        int flag = 0;
        // Initial mdi = new Initial();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        //thread //thread;

        public DashBoard()
        {
            InitializeComponent();

        }

        protected void groupbox()
        {
            BeginInvoke(new Action(() =>
            {
                GPStockAlerts.Visible = true;
                GPStockAlerts.Enabled = true;
                mdi.pnldaybook.Visible = true;
                mdi.pnlcashbook.Visible = true;
                mdi.pnlledger.Visible = true;
                gpDuecheques.Visible = true;
                gpDuecheques.Enabled = true;
                gpDueCreditors.Visible = true;
                gpDueDebitors.Visible = true;
                mdi.pnlreceiptvoucher.Visible = true;
                mdi.pnlpurchase.Visible = true;
                mdi.pnlitemmaster.Visible = true;
            }));
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
        //        return cp;
        //    }
        //}

        public void type()
        {
            try
            {
                dash.UserId = Login.memid;
                DataTable dt = dash.getUserType();
                if(dt.Rows.Count>0)
                lbltype.BeginInvoke(new Action(() => lbltype.Text = dt.Rows[0][0].ToString()));
            }
            catch (Exception)
            {
                ////thread.Abort();
                ////thread.Start();
                //MessageBox.Show("Loading in Progress Please Wait..!!","Sysbizz v1.5",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        PictureBox pictureBoxlogo = new PictureBox();
        protected void logo()
        {
            BeginInvoke(new Action(() =>
            {
                //PictureBox pictureBox1 = new PictureBox();
                //  pictureBox1.Location = new System.Drawing.Point(50,50);
                pictureBoxlogo.Padding = new Padding(200, 250, 200, 250);
                pictureBoxlogo.Name = " pictureBoxlogo";
                pictureBoxlogo.Size = new System.Drawing.Size(790, 200);
                // pictureBox1.BackColor = ColorTranslator.FromHtml("#EDEDED");
                // pictureBox1.BackgroundImage = Properties.Resources.sysbizz_logo;
                pictureBoxlogo.BackgroundImage = Properties.Resources.login_logo;
                pictureBoxlogo.BackgroundImageLayout = ImageLayout.Stretch;
                //   pictureBox1.Location = new Point(50, 50);
                pictureBoxlogo.Anchor = AnchorStyles.Left;
                pictureBoxlogo.Visible = false;
                flowLayoutPanel1.Controls.Add(pictureBoxlogo);

            }));
        }

        public void netMargin()
        {
            double net = Convert.ToDouble(LB_STOCK.Text) + Convert.ToDouble(lb_CashInHand.Text) + Convert.ToDouble(lb_recievbles.Text) - Convert.ToDouble(lb_payables.Text);
            textBox1.BeginInvoke(new Action(() => textBox1.Text = net.ToString(decimalFormat)));

        }

        public void user()
        {
            try
            {
                DataTable user = dash.getUser();
                if (user.Rows.Count > 0)
                {
                    txt_user.BeginInvoke(new Action(() => txt_user.Text = "User:" + user.Rows[0][0].ToString()));
                    txt_logintime.BeginInvoke(new Action(() => txt_logintime.Text = "Login Time:" + user.Rows[0][1].ToString()));
                }
            }

            catch (Exception e)
            {
                ////thread.Abort();
                ////thread.Start();
                //MessageBox.Show("Loading in Progress Please Wait..!!", "Sysbizz v1.5", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DashBoard_Load(object sender, EventArgs e)


        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(DbFunctions.conn.ConnectionString);
            lbl_company.Text = builder.InitialCatalog;

            if (lbl_company.Text == "")
            {
                string comp = builder.AttachDBFilename;
                comp = comp.Substring(comp.LastIndexOf(@"\"));
                comp = comp.Replace(@"\", "");
                lbl_company.Text ="Company :"+ comp.Replace(".mdf", "");
            }
            string productType = CompanyCreation.CompanyCreation.checkPackageType() == "1" ? "Demo" : CompanyCreation.CompanyCreation.checkPackageType() == "2" ? "Premium" : "";
            if (productType == "Demo")
            {
                CompanyCreation.CompanyCreation.checkExpiry("Demo");
                lbl_productsts.Text = "Product Not Activated " + CompanyCreation.CompanyCreation.dayremain + " Days Left.";
            }
            else
            {
                lbl_productsts.Text = "";
            }

            #region

            //  user();
            //  decimalFormat = Common.getDecimalFormat();
            //  logo();
            //  type();
            //  groupbox();
            //  BindMinimumStockItems();
            //  BINDCHEQUEDUERECIPTS();
            //  BINDCHEQUEDUEPAYMENTS();
            //  GETSUPPLIERS();
            //  GetFinancialYear();
            //  AmountDue();
            //  TOTALSTOCK();
            //  cashinHand();
            //  payables();
            //  Recievables();
            //  AmountDueByCreditors();
            //  netMargin();
            //  panel3.Visible = false; // purchase
            //  panel5.Visible = false;
            //  panel2.Visible = false; // sales
            //  panel4.Visible = false;
            //  panel10.Visible = false; //stock
            //  panel18.Visible = false;//receipt
            //  panel9.Visible = false;//payment
            //  panel8.Visible = false; // back up
            //  panel7.Visible = false;// settings
            //  panel17.Visible = false;// Accounts
            //  LB_STOCK.TextAlign = ContentAlignment.MiddleRight;
            //  lb_CashInHand.TextAlign = ContentAlignment.MiddleRight;
            //  lb_recievbles.TextAlign = ContentAlignment.MiddleRight;
            //  lb_payables.TextAlign = ContentAlignment.MiddleRight;
            //  //mdi.pnlitemmaster.Visible = false; // item master
            ////  toolStripStatusLabel1.Width = statusStrip1.Location.Y;
            //  timer2.Interval = _blinkFrequency; //optional: set in design code
            //  timer2.Start();
            /*int id = Convert.ToInt16(Login.memid);
            lblemp_id.Text = Convert.ToInt16(id).ToString();
            int flag;
            flag = priv.getpriv_id(lblemp_id.Text);
            if (flag > 0)
            {
                List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
                foreach (ToolStripMenuItem tsmi in mdi.menuMain.Items)
                {
                    if (tsmi.Enabled == true)
                    {
                        foreach (object item in tsmi.DropDownItems)
                        {
                            if (((ToolStripDropDownItem)item).Enabled == true)
                            {

                                if (tsmi.ToString() == "Items")
                                {
                                    if (((ToolStripDropDownItem)item).Text == "Item Master")
                                    {
                                        // mdi.pnlitemmaster.Visible = true;
                                        pictureBoxlogo.Hide();

                                    }
                                }
                                if (tsmi.ToString() == "Purchase")
                                {
                                    if (((ToolStripDropDownItem)item).Text == "Purchases")
                                    {

                                        panel3.Visible = true;
                                        picturepurchaseCash.Enabled = true;
                                        pictureBoxlogo.Hide();
                                        // mdi.pnlpurchase.Visible = true;

                                    }

                                    if (item.ToString() == "Purchase Report")
                                    {

                                        panel5.Visible = true;
                                        pictureSalesCredit.Enabled = true;
                                        pictureBoxlogo.Hide();
                                    }



                                }
                                if (tsmi.ToString() == "Sales")
                                {
                                    if (((ToolStripDropDownItem)item).Text == "POS Desk")
                                    {

                                        //  panel1.Visible = true;
                                        panel2.Visible = true;
                                        pictureBox2.Enabled = true;
                                        pictureBoxlogo.Hide();
                                    }

                                    if (((ToolStripDropDownItem)item).Text == "Sales Report")
                                    {

                                        //  panel1.Visible = true;
                                        panel4.Visible = true;
                                        pictureSalesReport.Enabled = true;
                                        pictureBoxlogo.Hide();
                                    }

                                }
                                if (tsmi.ToString() == "Stock")
                                {
                                    if (((ToolStripDropDownItem)item).Text == "Stock Report")
                                    {
                                        // pictureStockReport.Visible = true;

                                        //  GPStockAlerts.Visible = true;
                                        panel10.Visible = true;
                                        pictureStockReport.Enabled = true;
                                        pictureBoxlogo.Hide();
                                    }

                                }
                                if (tsmi.ToString() == "Vouchers")
                                {
                                    if (((ToolStripDropDownItem)item).Text == "Receipt Voucher")
                                    {

                                        panel18.Visible = true;
                                        pictureBox.Enabled = true;
                                        pictureBoxlogo.Hide();
                                        //  gpDuecheques.Visible = true;
                                        //   gpDuecheques.Enabled = true;
                                        //  mdi.pnlreceiptvoucher.Visible = true;
                                    }
                                    if (((ToolStripDropDownItem)item).Text == "Payment Voucher")
                                    {

                                        panel9.Visible = true;
                                        picturepaymentvoucher.Enabled = true;
                                        pictureBoxlogo.Hide();
                                        //   gpDuecheques.Visible = true;
                                        //   gpDuecheques.Enabled = true;
                                        //  gpDueCreditors.Visible = true;
                                        //  gpDueDebitors.Visible = true;
                                        // mdi.pnlpaymentvoucher.Visible = true;


                                    }
                                }
                                if (tsmi.ToString() == "Backup")
                                {
                                    if (((ToolStripDropDownItem)item).Text == "Data Backup")
                                    {

                                        panel8.Visible = true;

                                        pictureBoxBackUp.Enabled = true;
                                        pictureBoxlogo.Hide();
                                    }
                                }
                                if (tsmi.ToString() == "Settings")
                                {
                                    if (((ToolStripDropDownItem)item).Text == "General Settings")
                                    {

                                        panel7.Visible = true;
                                        pictureBoxSettings.Enabled = true;
                                        pictureBoxlogo.Hide();
                                    }
                                }
                                if (tsmi.ToString() == "Accounts")
                                {
                                    if (((ToolStripDropDownItem)item).Text == "Day Book")
                                    {

                                        panel17.Visible = true;
                                        pictureBoxDayBook.Enabled = true;
                                        pictureBoxlogo.Hide();
                                        //  mdi.pnldaybook.Visible = true;
                                        //  mdi.pnlcashbook.Visible = true;
                                        //  mdi.pnlledger.Visible = true;




                                    }
                                }
                                if (tsmi.ToString() == "Reports")
                                {
                                    if (((ToolStripDropDownItem)item).Text == "Stock Limits")
                                    {

                                        pictureBoxlogo.Hide();
                                        // GPStockAlerts.Enabled = true;
                                    }

                                    if (item.ToString() == "Purchase Report")
                                    {

                                        panel5.Visible = true;
                                        pictureSalesCredit.Enabled = true;
                                        pictureBoxlogo.Hide();
                                    }

                                    if (((ToolStripDropDownItem)item).Text == "Sales Report")
                                    {

                                        panel4.Visible = true;
                                        pictureSalesReport.Enabled = true;
                                        pictureBoxlogo.Hide();
                                    }

                                    if (((ToolStripDropDownItem)item).Text == "Stock Report")
                                    {


                                        panel10.Visible = true;
                                        pictureStockReport.Enabled = true;
                                        pictureBoxlogo.Hide();
                                    }
                                }


                            }
                        }
                    }
                }
            }
            */
            #endregion
            
            //////thread = new ////thread(new ////threadStart(OnLoadProcess));
            //////thread.IsBackground = true;
            //if (!////thread.IsAlive)
            //{
            //    ////thread.Start();
            //}

            OnLoadProcess();
            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
            //  //timer interval
            //  t.Interval = 1000;  //in milliseconds

            //  t.Tick += new EventHandler(this.t_Tick);

            //  //start timer when form loads
            //  t.Start();  //this will use t_Tick() method
        }

        private IEnumerable<ToolStripMenuItem> GetItems(ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem dropDownItem in item.DropDownItems)
            {
                if (dropDownItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem subItem in GetItems(dropDownItem))
                        yield return subItem;
                }
                yield return dropDownItem;
            }
        }

        public void BindMinimumStockItems()
        {
            try
            {
                lblMinimustock.BeginInvoke(new Action(() => lblMinimustock.Text = dash.getMinimumStock()));
                lblbelowstock.BeginInvoke(new Action(() => lblbelowstock.Text = dash.getBelowStock()));
            }
            catch (Exception)
            {
                ////thread.Abort();
                //thread.Start();
                //MessageBox.Show("Loading in Progress Please Wait..!!", "Sysbizz v1.5", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void timer2_Tick_1(object sender, EventArgs e)
        {
            if (Convert.ToDouble(lblMinimustock.Text) > 0)
            {
                this.lblMinimustock.Visible = !this.lblMinimustock.Visible;
            }
            if (Convert.ToDouble(lblbelowstock.Text) > 0)
            {
                this.lblbelowstock.Visible = !this.lblbelowstock.Visible;
            }

            if (Convert.ToInt32(LBLCHECDUERECIPTS.Text) > 0)
            {
                this.LBLCHECDUERECIPTS.Visible = !this.LBLCHECDUERECIPTS.Visible;
            }

            if (Convert.ToInt32(LBLCHEQUEDUEPAYMENTS.Text) > 0)
            {
                this.LBLCHEQUEDUEPAYMENTS.Visible = !this.LBLCHEQUEDUEPAYMENTS.Visible;
            }

            _blinkCount++;

            if (_blinkCount == _maxNumberOfBlinks * 2)
            {
                //  timer2.Stop();
                lblMinimustock.Visible = true;
            }
        }


        public void BINDCHEQUEDUERECIPTS()
        {
            try
            {
                string totalPendingReceiptsCheques = dash.bindChequeDueReceipt();
                LBLCHECDUERECIPTS.BeginInvoke(new Action(() => LBLCHECDUERECIPTS.Text = totalPendingReceiptsCheques));

            }
            catch (Exception ee)
            {
                MessageBox.Show("Loading in Progress Please Wait..!!", "Sysbizz v1.5", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void GETSUPPLIERS()
        {
            try
            {

                DataTable getsupp = dash.getSuppliers();
                lblsuppliers.BeginInvoke(new Action(() => lblsuppliers.Text = getsupp.Rows.Count.ToString()));
            }
            catch (Exception)
            {
                //thread.Abort();
                //thread.Start();
               // MessageBox.Show("Loading in Progress Please Wait..!!", "Sysbizz v1.5", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void BINDCHEQUEDUEPAYMENTS()
        {
            try
            {
                string totalPendingPaymentCheques = dash.bindChequeDuePayments();
                LBLCHEQUEDUEPAYMENTS.BeginInvoke(new Action(() => LBLCHEQUEDUEPAYMENTS.Text = totalPendingPaymentCheques));
            }
            catch (Exception)
            {
                //thread.Abort();
                //thread.Start();
                //MessageBox.Show("Loading in Progress Please Wait..!!", "Sysbizz v1.5", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        DateTime Datefrom, Dateto;
        public void GetFinancialYear()
        {
            try
            {
                Class.CompanySetup cset = new Class.CompanySetup();
                DataTable dt = new DataTable();
                cset.Status = true;
                dt = cset.GetFinancialYear();

                Datefrom = Convert.ToDateTime(dt.Rows[0][1]);

                Dateto = Convert.ToDateTime(dt.Rows[0][2]);
            }
            catch
            {
            }
        }

        public void cashinHand()
        {
            DataTable dt = new DataTable();
            Class.CompanySetup ComSet = new Class.CompanySetup();
            ComSet.Status = true;
            dt = ComSet.GetFinancialYear();

            DateTime startDate = Convert.ToDateTime(dt.Rows[0][1]);
            dash.AccId = 21;
            dash.AccName = "CASH ACCOUNT";
            dash.StartDate = startDate;
            dash.EndDate = DateTime.Today;
            DataTable r = dash.cashInHand();
            double value = 0;
            for (int i = 0; i < r.Rows.Count; i++)
            {
                try
                {
                    value += Convert.ToDouble(r.Rows[i]["Total Balance"]);
                }
                catch
                {

                }
            }
            //  conn.Close();
            //DbFunctions.CloseConnection();
            lb_CashInHand.BeginInvoke(new Action(() => lb_CashInHand.Text = value.ToString(decimalFormat)));
        }

        public void payables()
        {
            try
            {
                dash.GroupID = 64;
                DataTable pay = dash.getPayables();
                //double value = 0;
                //for (int i = 0; i < pay.Rows.Count; i++)
                //{
                //    // if (Convert.ToDouble(r["Total Balance"])>0)
                //    value += Convert.ToDouble(pay.Rows[i]["Total Balance"]);
                //}
                //DbFunctions.CloseConnection();
                if(pay.Rows.Count>0)
                    if (pay.Rows[0]["Balance"].ToString() != "")
                    lb_payables.BeginInvoke(new Action(() => lb_payables.Text = Convert.ToDecimal(pay.Rows[0]["Balance"] == "" ? "0" : pay.Rows[0]["Balance"]).ToString(decimalFormat)));
            }
            catch
            {
                MessageBox.Show("Loading in Progress Please Wait..!!", "Sysbizz v1.5", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Recievables()
        {
            try
            {
                DataTable recieve = dash.getReceiveble();
                double value = 0;
                //for (int i = 0; i < r.Rows.Count; i++)
                //{

                //    value += Convert.ToDouble(r.Rows[i]["Total Balance"]);

                //}
                //DbFunctions.CloseConnection();
                if (recieve.Rows.Count > 0)
                {
                    if (recieve.Rows[0]["Balance"].ToString()!="")
                    lb_recievbles.BeginInvoke(new Action(() => lb_recievbles.Text = Convert.ToDecimal(recieve.Rows[0]["Balance"] == "" ? "0" : recieve.Rows[0]["Balance"]).ToString(decimalFormat)));
                }
            }
            catch
            {
                MessageBox.Show("Loading in Progress Please Wait..!!", "Sysbizz v1.5", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void TOTALSTOCK()
        {
            double value = 0;
            value = dash.totalStock();
            LB_STOCK.BeginInvoke(new Action(() => LB_STOCK.Text = value.ToString(decimalFormat)));
        }

        public void AmountDue()
        {
            double tot = 0;
            int count = 0;
            DataTable amountdue = dash.amountDueByCustomer();
            try
            {
                if (amountdue.Rows.Count > 0)
                {
                    //lblAmountDue.Text = Math.Abs(Convert.ToDouble(tmp.Rows[0]["totalAmount"])).ToString(decimalFormat);
                    //lb_CustomerDueCount.Text = tmp.Rows[0]["DueCust"].ToString();
                    lblAmountDue.BeginInvoke(new Action(() => lblAmountDue.Text = Math.Abs(Convert.ToDouble(amountdue.Rows[0]["totalAmount"])).ToString(decimalFormat)));
                    lb_CustomerDueCount.BeginInvoke(new Action(() => lb_CustomerDueCount.Text = amountdue.Rows[0]["DueCust"].ToString()));
                }
            }
            catch (Exception ee)
            {
                //thread.Abort();
                //thread.Start();
                //MessageBox.Show("Loading in Progress Please Wait..!!", "Sysbizz v1.5", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        public void AmountDueByCreditors()
        {
            double tot = 0;
            int count = 0;
            DataTable tmp = dash.amountDueToSuppliers();
            try
            {
                lblAmountDueByCreditors.BeginInvoke(new Action(() => lblAmountDueByCreditors.Text = Math.Abs(Convert.ToDouble(tmp.Rows[0]["totalAmount"])).ToString(decimalFormat)));
                lblsuppliers.BeginInvoke(new Action(() => lblsuppliers.Text = tmp.Rows[0]["DueCust"].ToString()));

            }
            catch (Exception ee)
            {
                //thread.Abort();
                //thread.Start();
                //MessageBox.Show("Loading in Progress Please Wait..!!", "Sysbizz v1.5", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BackColor = ColorTranslator.FromHtml("#EDEDED");
            pictureBox2.Cursor = Cursors.Hand;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.White;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (General.IsEnabled(Settings.POSTerminal))
            {
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);
                New_Sales cn = new New_Sales("SAL.CSS", "Cash");
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "POS Desk";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
                mdi.onlyhide();
            }
            else
            {
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);
                SalesQ cn = new SalesQ("SAL.CSS", "Cash");
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "POS Desk";
                //kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
                mdi.onlyhide();

            }

        }

        private void picturepurchaseCash_MouseHover(object sender, EventArgs e)
        {
            picturepurchaseCash.BackColor = ColorTranslator.FromHtml("#EDEDED");
            picturepurchaseCash.Cursor = Cursors.Hand;
        }

        private void picturepurchaseCash_MouseLeave(object sender, EventArgs e)
        {
            picturepurchaseCash.BackColor = Color.White;
        }

        private void picturepurchaseCash_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "Purchase Cash")
                {
                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    mdi.onlyhide();
                    break;

                }
            }
            if (flag == 0)
            {
                PurchaseMaster cn = new PurchaseMaster("PUR.CSS", "Cash");
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);

                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Purchases";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
                mdi.onlyhide();
            }
            flag = 0;
        }

        private void pictureStockReport_MouseHover(object sender, EventArgs e)
        {
            pictureStockReport.BackColor = ColorTranslator.FromHtml("#EDEDED");
            pictureStockReport.Cursor = Cursors.Hand;
        }

        private void pictureStockReport_MouseLeave(object sender, EventArgs e)
        {
            pictureStockReport.BackColor = Color.White;
        }

        private void pictureStockReport_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "Stock Report")
                {
                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    mdi.onlyhide();
                    break;

                }
            }
            if (flag == 0)
            {
                Current_Stock cn = new Current_Stock();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);

                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Stock Report";
                //kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
                mdi.onlyhide();
            }
            flag = 0;
        }

        private void pictureSalesReport_MouseHover(object sender, EventArgs e)
        {
            pictureSalesReport.BackColor = ColorTranslator.FromHtml("#EDEDED");
            pictureSalesReport.Cursor = Cursors.Hand;
        }

        private void pictureSalesReport_MouseLeave(object sender, EventArgs e)
        {
            pictureSalesReport.BackColor = Color.White;
        }

        private void pictureSalesReport_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "Sales Report")
                {
                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    mdi.onlyhide();
                    break;

                }
            }
            if (flag == 0)
            {
                reports.Sales_Report_On_HDR cn = new reports.Sales_Report_On_HDR();
                //  reports.SalesReportFinal cn = new reports.SalesReportFinal();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);

                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Sales Report";
                //  kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
                mdi.onlyhide();
            }
            flag = 0;
        }

        private void pictureSalesCredit_MouseHover(object sender, EventArgs e)
        {
            pictureSalesCredit.BackColor = ColorTranslator.FromHtml("#EDEDED");
            pictureSalesCredit.Cursor = Cursors.Hand;
        }

        private void pictureSalesCredit_MouseLeave(object sender, EventArgs e)
        {
            pictureSalesCredit.BackColor = Color.White;
        }

        private void pictureSalesCredit_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "Purchase Report")
                {

                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    mdi.onlyhide();
                    break;

                }
            }
            if (flag == 0)
            {
                Purchase_RPT_HDR cn = new Purchase_RPT_HDR();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);

                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Sales Credit";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
                mdi.onlyhide();
            }
            flag = 0;
        }

        private void pictureBox_MouseHover(object sender, EventArgs e)
        {
            pictureBox.BackColor = ColorTranslator.FromHtml("#EDEDED");
            pictureBox.Cursor = Cursors.Hand;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            pictureBox.BackColor = Color.White;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "Receipt Voucher")
                {
                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    break;

                }
            }
            if (flag == 0)
            {
                PaymentVoucher2 cn = new PaymentVoucher2(1);
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);

                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);


                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Receipt Voucher";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
            }
            flag = 0;
        }

        private void picturepaymentvoucher_MouseHover(object sender, EventArgs e)
        {
            picturepaymentvoucher.BackColor = ColorTranslator.FromHtml("#EDEDED");
            picturepaymentvoucher.Cursor = Cursors.Hand;
        }

        private void picturepaymentvoucher_MouseLeave(object sender, EventArgs e)
        {
            picturepaymentvoucher.BackColor = Color.White;
        }

        private void picturepaymentvoucher_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "Payment Voucher")
                {
                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    break;

                }
            }
            if (flag == 0)
            {
                PaymentVoucher2 cn = new PaymentVoucher2(0);
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);

                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);


                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Payment Voucher";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
            }
            flag = 0;
        }

        private void pictureBoxDayBook_MouseHover(object sender, EventArgs e)
        {
            pictureBoxDayBook.BackColor = ColorTranslator.FromHtml("#EDEDED");
            pictureBoxDayBook.Cursor = Cursors.Hand;
        }

        private void pictureBoxDayBook_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxDayBook.BackColor = Color.White;
        }

        private void pictureBoxDayBook_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "Ledger Report")
                {
                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    mdi.onlyhide();
                    break;

                }
            }
            if (flag == 0)
            {
                Accounts.LedgerReport cn = new Accounts.LedgerReport();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);


                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Ledger Report";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
                mdi.onlyhide();
                cn.Show();
            }
            flag = 0;
        }

        private void pictureBoxBackUp_MouseHover(object sender, EventArgs e)
        {
            pictureBoxBackUp.BackColor = ColorTranslator.FromHtml("#EDEDED");
            pictureBoxBackUp.Cursor = Cursors.Hand;
        }

        private void pictureBoxBackUp_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxBackUp.BackColor = Color.White;
        }

        private void pictureBoxBackUp_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "Data Backup")
                {
                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    break;

                }
            }
            if (flag == 0)
            {
                Backups cn = new Backups();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);

                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Data Backup";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
            }
            flag = 0;
        }

        private void pictureBoxSettings_MouseHover(object sender, EventArgs e)
        {
            pictureBoxSettings.BackColor = ColorTranslator.FromHtml("#EDEDED");
            pictureBoxSettings.Cursor = Cursors.Hand;
        }

        private void pictureBoxSettings_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxSettings.BackColor = Color.White;
        }

        private void pictureBoxSettings_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "General Settings")
                {
                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    mdi.onlyhide();
                    break;
                }
            }
            if (flag == 0)
            {
                GeneralSettings cn = new GeneralSettings();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);

                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "General Settings";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
                mdi.onlyhide();
            }
            flag = 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            reports.Stock_Limits cn = new reports.Stock_Limits(0);
            //   FrmStockWindow cn = new FrmStockWindow(1);

            ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
            mdi.maindocpanel.Pages.Add(kp);

            cn.Show();
            cn.TopLevel = false;
            //  splitContainer1.Panel2.Controls.Add(ad);
            kp.Controls.Add(cn);


            cn.Dock = DockStyle.Fill;
            kp.Text = cn.Text;
            kp.Name = "Payment Voucher";
            // kp.Focus();
            cn.FormBorderStyle = FormBorderStyle.None;

            mdi.maindocpanel.SelectedPage = kp;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            reports.Stock_Limits cn = new reports.Stock_Limits(1);
            // FrmStockWindow cn = new FrmStockWindow(2);

            ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
            mdi.maindocpanel.Pages.Add(kp);

            cn.Show();
            cn.TopLevel = false;
            //  splitContainer1.Panel2.Controls.Add(ad);
            kp.Controls.Add(cn);


            cn.Dock = DockStyle.Fill;
            kp.Text = cn.Text;
            kp.Name = "Payment Voucher";
            // kp.Focus();
            cn.FormBorderStyle = FormBorderStyle.None;

            mdi.maindocpanel.SelectedPage = kp;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Accounts.Check_Dues Chd = new Accounts.Check_Dues(0);
            Chd.ShowDialog();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Accounts.Check_Dues Chd = new Accounts.Check_Dues(1);
            Chd.ShowDialog();
        }

        private void lblsuppliers_Click(object sender, EventArgs e)
        {
            Accounts.Suppliers_for_Collection Spc = new Accounts.Suppliers_for_Collection();
            Spc.ShowDialog();
        }

        private void lblAmountDue_Click(object sender, EventArgs e)
        {
            Due_Customers dc = new Due_Customers();
            dc.ShowDialog();
        }

        private void LBLCHEQUEDUEPAYMENTS_Click(object sender, EventArgs e)
        {
            Accounts.Check_Dues Chd = new Accounts.Check_Dues(1);
            Chd.ShowDialog();
        }

        private void LBLCHECDUERECIPTS_Click(object sender, EventArgs e)
        {
            Accounts.Check_Dues Chd = new Accounts.Check_Dues(0);
            Chd.ShowDialog();
        }

        private void lblAmountDueByCreditors_Click(object sender, EventArgs e)
        {
            Accounts.Suppliers_for_Collection Spc = new Accounts.Suppliers_for_Collection();
            Spc.ShowDialog();
        }

        private void panel20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (lblMinimustock.Text != "" && Convert.ToDouble(lblMinimustock.Text) > 0)
            {
                this.lblMinimustock.Visible = !this.lblMinimustock.Visible;
            }
            if (lblbelowstock.Text != "" && Convert.ToDouble(lblbelowstock.Text) > 0)
            {
                this.lblbelowstock.Visible = !this.lblbelowstock.Visible;
            }

            if (LBLCHECDUERECIPTS.Text != "" && Convert.ToInt32(LBLCHECDUERECIPTS.Text) > 0)
            {
                this.LBLCHECDUERECIPTS.Visible = !this.LBLCHECDUERECIPTS.Visible;
            }

            if (LBLCHEQUEDUEPAYMENTS.Text != "" && Convert.ToInt32(LBLCHEQUEDUEPAYMENTS.Text) > 0)
            {
                this.LBLCHEQUEDUEPAYMENTS.Visible = !this.LBLCHEQUEDUEPAYMENTS.Visible;
            }

            _blinkCount++;

            if (_blinkCount == _maxNumberOfBlinks * 2)
            {

                //  timer2.Stop();

                lblMinimustock.Visible = true;


            }
        }

        private void lb_CashInHand_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "Ledger Report")
                {
                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    mdi.onlyhide();
                    break;

                }
            }
            if (flag == 0)
            {
                Accounts.LedgerReport cn = new Accounts.LedgerReport();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);


                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Ledger Report";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
                mdi.onlyhide();
                cn.Show();
            }
            flag = 0;
        }

        private void LB_STOCK_Click(object sender, EventArgs e)
        {
            reports.StockDetails rpt = new reports.StockDetails();
            rpt.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
             foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "Ledger Report")
                {
                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    mdi.onlyhide();
                    break;

                }
            }
            if (flag == 0)
            {
                Accounts.LedgerReport cn = new Accounts.LedgerReport();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);


                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Ledger Report";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
                mdi.onlyhide();
                cn.Show();
            }
            flag = 0;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Visible = false;
        }

        private void DashBoard_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
        }

        private void lb_CustomerDueCount_Click(object sender, EventArgs e)
        {
            Due_Customers dc = new Due_Customers();
            dc.ShowDialog();
        }

        private void lb_recievbles_Click(object sender, EventArgs e)
        {
            //flag=0;
            //string a = "hiding";
            reports.Recievables_and_Payables obj = new reports.Recievables_and_Payables();
            obj.Show();
            obj.Hiding_Payables();
        }

        private void lb_payables_Click(object sender, EventArgs e)
        {
            reports.Recievables_and_Payables obj = new reports.Recievables_and_Payables();
            obj.Show();
            obj.Hiding_Recievables();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //reports.StockDetails rpt = new reports.StockDetails();
            //rpt.Show();
            Current_Stock cn = new Current_Stock();
            cn.ShowDialog();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //reports.FrmReceivables obj = new reports.FrmReceivables();
            rpt.FrmTransactionsReport obj = new rpt.FrmTransactionsReport(63);
            obj.Show();
            //obj.Hiding_Payables();
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //reports.FrmPayables obj = new reports.FrmPayables();
            rpt.FrmTransactionsReport obj = new rpt.FrmTransactionsReport(64);
            obj.Show();
            obj.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void t_Tick(object sender, EventArgs e)
        {
            //get current time
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;

            //time
            string time = "";

            //padding leading zero
            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }
            Convert.ToDateTime(time);
            //update label

            toolStripStatusLabel1.Text = Convert.ToDateTime(time).ToString();
        }

        private void groupBox1_Leave(object sender, EventArgs e)
        {
            // textBox1.Visible = false;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            textBox1.Visible = false;
        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            textBox1.Visible = true;
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            CompanyCreation.frmCreateNewCompany f = new CompanyCreation.frmCreateNewCompany();
            f.Show();

            //Payment_Voucher_Multiple f = new Payment_Voucher_Multiple();
            //f.Show();

        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        public void OnLoadProcess()
        {
            try
            {
                user();
                //prgrsbar.BeginInvoke(new Action(() => prgrsbar.Value = 5));
                progress.BeginInvoke(new Action(() => progress.Text = "" + 5 + "%"));                
                //System.//threading.//thread.Sleep(200);
                decimalFormat = Common.getDecimalFormat();
                logo();




                //System.//threading.//thread.Sleep(200);
                //prgrsbar.BeginInvoke(new Action(() => prgrsbar.Value = 10));
                progress.BeginInvoke(new Action(() => progress.Text = "" + 10 + "%"));
                type();
                groupbox();
                //System.//threading.//thread.Sleep(350);
                //prgrsbar.BeginInvoke(new Action(() => prgrsbar.Value = 20));
                progress.BeginInvoke(new Action(() => progress.Text = "" + 20 + "%"));
                BindMinimumStockItems();
                //System.//threading.//thread.Sleep(350);
                BINDCHEQUEDUERECIPTS();
                //prgrsbar.BeginInvoke(new Action(() => prgrsbar.Value = 30));
                progress.BeginInvoke(new Action(() => progress.Text = "" + 30 + "%"));
                BINDCHEQUEDUEPAYMENTS();
                //System.//threading.//thread.Sleep(300);
                GETSUPPLIERS();
                //prgrsbar.BeginInvoke(new Action(() => prgrsbar.Value = 40));
                progress.BeginInvoke(new Action(() => progress.Text = "" + 40 + "%"));
                GetFinancialYear();
                AmountDue();
                //System.//threading.//thread.Sleep(300);
                //prgrsbar.BeginInvoke(new Action(() => prgrsbar.Value = 50));
                progress.BeginInvoke(new Action(() => progress.Text = "" + 50 + "%"));
                TOTALSTOCK();
                cashinHand();
                //System.//threading.//thread.Sleep(200);
                //prgrsbar.BeginInvoke(new Action(() => prgrsbar.Value = 60));
                progress.BeginInvoke(new Action(() => progress.Text = "" + 60+ "%"));
                payables();
                Recievables();
                //System.//threading.//thread.Sleep(250);
                //prgrsbar.BeginInvoke(new Action(() => prgrsbar.Value = 70));
                progress.BeginInvoke(new Action(() => progress.Text = "" + 70 + "%"));
                AmountDueByCreditors();
                //prgrsbar.BeginInvoke(new Action(() => prgrsbar.Value = 80));
                progress.BeginInvoke(new Action(() => progress.Text = "" + 80 + "%"));
                netMargin();
                //System.//threading.//thread.Sleep(250);
                progress.BeginInvoke(new Action(() => progress.Text = "" + 90 + "%"));
                progress.Text = "" + 90 + "%"; ;
                //panel3.BeginInvoke(new Action(() => panel3.Visible = false));// purchase
                //panel5.BeginInvoke(new Action(() => panel5.Visible = false));
                //panel2.BeginInvoke(new Action(() => panel2.Visible = false));// sales
                //panel4.BeginInvoke(new Action(() => panel4.Visible = false));
                //panel10.BeginInvoke(new Action(() => panel10.Visible = false));
                //panel3.BeginInvoke(new Action(() => panel3.Visible = false));
                //panel18.BeginInvoke(new Action(() => panel18.Visible = false));//receipt
                //panel9.BeginInvoke(new Action(() => panel9.Visible = false));//payment
                //panel8.BeginInvoke(new Action(() => panel8.Visible = false));// back up
                //panel7.BeginInvoke(new Action(() => panel7.Visible = false));// settings
                //panel7.BeginInvoke(new Action(() => panel17.Visible = false));// Accounts
                //System.//threading.//thread.Sleep(200);
                //BeginInvoke(new Action(() =>
                //{
                    pictureBoxlogo.Hide();
                    LB_STOCK.TextAlign = ContentAlignment.MiddleRight;
                    lb_CashInHand.TextAlign = ContentAlignment.MiddleRight;
                    lb_recievbles.TextAlign = ContentAlignment.MiddleRight;
                    lb_payables.TextAlign = ContentAlignment.MiddleRight;
                    //mdi.pnlitemmaster.Visible = false; // item master
                    //  toolStripStatusLabel1.Width = statusStrip1.Location.Y;

                    int id = Convert.ToInt16(Login.memid);
                    lblemp_id.Text = Convert.ToInt16(id).ToString();
                    int flag;
                    flag = priv.getpriv_id(lblemp_id.Text);
                    if (flag > 0)
                    {
                        List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
                        foreach (ToolStripMenuItem tsmi in mdi.menuMain.Items)
                        {
                            if (tsmi.Enabled == true)
                            {
                                foreach (object item in tsmi.DropDownItems)
                                {
                                    if (((ToolStripDropDownItem)item).Enabled == true)
                                    {

                                        if (tsmi.ToString() == "Items")
                                        {
                                            if (((ToolStripDropDownItem)item).Text == "Item Master")
                                            {
                                                // mdi.pnlitemmaster.Visible = true;
                                                pictureBoxlogo.Hide();

                                            }
                                        }
                                        if (tsmi.ToString() == "Purchase")
                                        {
                                            if (((ToolStripDropDownItem)item).Text == "Purchases")
                                            {

                                                panel3.Visible = true;
                                                picturepurchaseCash.Enabled = true;
                                                pictureBoxlogo.Hide();
                                                // mdi.pnlpurchase.Visible = true;

                                            }

                                            if (item.ToString() == "Purchase Report")
                                            {

                                                panel5.Visible = true;
                                                pictureSalesCredit.Enabled = true;
                                                pictureBoxlogo.Hide();
                                            }



                                        }
                                        if (tsmi.ToString() == "Sales")
                                        {
                                            if (((ToolStripDropDownItem)item).Text == "POS Desk")
                                            {

                                                //  panel1.Visible = true;
                                                panel2.Visible = true;
                                                pictureBox2.Enabled = true;
                                                pictureBoxlogo.Hide();
                                            }

                                            if (((ToolStripDropDownItem)item).Text == "Sales Report")
                                            {

                                                //  panel1.Visible = true;
                                                panel4.Visible = true;
                                                pictureSalesReport.Enabled = true;
                                                pictureBoxlogo.Hide();
                                            }

                                        }
                                        if (tsmi.ToString() == "Stock")
                                        {
                                            if (((ToolStripDropDownItem)item).Text == "Stock Report")
                                            {
                                                // pictureStockReport.Visible = true;

                                                //  GPStockAlerts.Visible = true;
                                                panel10.Visible = true;
                                                pictureStockReport.Enabled = true;
                                                pictureBoxlogo.Hide();
                                            }

                                        }
                                        if (tsmi.ToString() == "Vouchers")
                                        {
                                            if (((ToolStripDropDownItem)item).Text == "Receipt Voucher")
                                            {

                                                panel18.Visible = true;
                                                pictureBox.Enabled = true;
                                                pictureBoxlogo.Hide();
                                                //  gpDuecheques.Visible = true;
                                                //   gpDuecheques.Enabled = true;
                                                //  mdi.pnlreceiptvoucher.Visible = true;
                                            }
                                            if (((ToolStripDropDownItem)item).Text == "Payment Voucher")
                                            {

                                                panel9.Visible = true;
                                                picturepaymentvoucher.Enabled = true;
                                                pictureBoxlogo.Hide();
                                                //   gpDuecheques.Visible = true;
                                                //   gpDuecheques.Enabled = true;
                                                //  gpDueCreditors.Visible = true;
                                                //  gpDueDebitors.Visible = true;
                                                // mdi.pnlpaymentvoucher.Visible = true;


                                            }
                                        }
                                        if (tsmi.ToString() == "Backup")
                                        {
                                            if (((ToolStripDropDownItem)item).Text == "Data Backup")
                                            {

                                                panel8.Visible = true;

                                                pictureBoxBackUp.Enabled = true;
                                                pictureBoxlogo.Hide();
                                            }
                                        }
                                        if (tsmi.ToString() == "Settings")
                                        {
                                            if (((ToolStripDropDownItem)item).Text == "General Settings")
                                            {

                                                panel7.Visible = true;
                                                pictureBoxSettings.Enabled = true;
                                                pictureBoxlogo.Hide();
                                            }
                                        }
                                        if (tsmi.ToString() == "Accounts")
                                        {
                                            if (((ToolStripDropDownItem)item).Text == "Day Book")
                                            {

                                                panel17.Visible = true;
                                                pictureBoxDayBook.Enabled = true;
                                                pictureBoxlogo.Hide();
                                                //  mdi.pnldaybook.Visible = true;
                                                //  mdi.pnlcashbook.Visible = true;
                                                //  mdi.pnlledger.Visible = true;
                                            }
                                        }
                                        if (tsmi.ToString() == "Reports")
                                        {
                                            if (((ToolStripDropDownItem)item).Text == "Stock Limits")
                                            {

                                                pictureBoxlogo.Hide();
                                                // GPStockAlerts.Enabled = true;
                                            }

                                            if (item.ToString() == "Purchase Report")
                                            {

                                                panel5.Visible = true;
                                                pictureSalesCredit.Enabled = true;
                                                pictureBoxlogo.Hide();
                                            }

                                            if (((ToolStripDropDownItem)item).Text == "Sales Report")
                                            {

                                                panel4.Visible = true;
                                                pictureSalesReport.Enabled = true;
                                                pictureBoxlogo.Hide();
                                            }

                                            if (((ToolStripDropDownItem)item).Text == "Stock Report")
                                            {


                                                panel10.Visible = true;
                                                pictureStockReport.Enabled = true;
                                                pictureBoxlogo.Hide();
                                            }
                                        }


                                    }
                                }
                            }
                        }
                    }
                    //prgrsbar.BeginInvoke(new Action(() => prgrsbar.Value = 100));
                    progress.BeginInvoke(new Action(() => progress.Text = "" + 100 + "%"));
                    //prgrsbar.Visible = false;
                    progress.Visible = false;
                //}));
            }
            catch { }

            //this will use t_Tick() method
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            timer2.Interval = _blinkFrequency; //optional: set in design code
            timer2.Start();

            //timer interval
            t.Interval = 1000;  //in milliseconds

            t.Tick += new EventHandler(this.t_Tick);

            //start timer when form loads
            t.Start();
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //ModifyProgressBarColor.SetState(prgrsbar, 2);
            ////prgrsbar.Value = e.ProgressPercentage;
            //lbl_percent.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void lbl_productsts_MouseHover(object sender, EventArgs e)
        {
            Font font = new Font("Verdana", 10,FontStyle.Bold);
            lbl_productsts.Font = font;
            lbl_productsts.ForeColor = Color.Blue;
        }

        private void lbl_productsts_MouseLeave(object sender, EventArgs e)
        {
            Font font = new Font("Verdana", 10, FontStyle.Bold);
            lbl_productsts.Font = font;
            lbl_productsts.ForeColor = Color.LightGray;
        }

        private void lbl_productsts_Click(object sender, EventArgs e)
        {
            Activation.Activation activation = new Activation.Activation();
            activation.ShowDialog();
        }

        private void panel27_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in mdi.maindocpanel.Pages)
            {
                if (kr.Name == "Touch POS")
                {
                    mdi.maindocpanel.SelectedPage = kr;
                    flag = 1;
                    break;

                }
            }
            if (flag == 0)
            {
                POSDESK.POSForm cn = new POSDESK.POSForm();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);

                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);


                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Touch POS";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;

                mdi.maindocpanel.SelectedPage = kp;
            }
            flag = 0;
        }

        private void DashBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (//thread.IsAlive)
            //{
            //    //thread.Abort();
            //}            
            //            bgWorker.CancelAsync();
            
        }

        private void DashBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (prgrsbar.Visible)
            //{
            //    e.Cancel = true;
            //}
        }

    }
}


public static class ModifyProgressBarColor
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    public static void SetState(this ProgressBar pBar, int state)
    {
        SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
    }
}

