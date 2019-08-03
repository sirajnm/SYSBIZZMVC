using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;
using Sys_Sols_Inventory.Class;
using System.Reflection;
using System.Drawing.Printing;

namespace Sys_Sols_Inventory.POSDESK
{
    public partial class POSForm : Form
    {
        Class.CompanySetup ComSet = new Class.CompanySetup();
        Class.Printing Printing = new Class.Printing();
        Class.PaymentDetails PaymDet = new Class.PaymentDetails();
        Class.DateSettings dset = new Class.DateSettings();
        Class.Login login = new Class.Login();
        Class.Login supervisorlogin = new Class.Login();
        ShiftMasterDB shift = new ShiftMasterDB();
        private Boolean _islogedIn = false;
        private float totalquantity, totalamounttopay, paidamount, balanceamount;
        List<POS_DeliveryBoy> deliveryboys = POS_Repositery.GetDeliveryBoys();
        TransactionHeaderTEMP lasttransaction;
        String lasttransactionreceiptno;
        String Selectedtransactionreceiptno;
        String printabletransctionreceiptno;
        TransactionHeaderTEMP selectedtransaction;
        TransactionHeaderTEMP printabletransaction;
        Button[] numericbutton; 

        #region Global Properties

        public Single TotalAmounttoPay
        {
            get { return totalamounttopay; }
            set {
                totalamounttopay = value;
                balanceamount = totalamounttopay - PaidAmount;
            }
        }

        public Single PaidAmount
        {
            get { return paidamount; }
            set
            {
                paidamount = value;
                balanceamount = totalamounttopay - paidamount;
            }
        }

        public Boolean IsLogedIn
        {
            get
            {
                return _islogedIn;
            }
            set
            {
                _islogedIn = value;

                if (_islogedIn)
                {
                    this.toolStripStatusLabel4.Text = login.Username;
                    TransStatus = "NewTransaction";
                    if (!StartTabControl.TabPages.Contains(Main))
                    {
                        StartTabControl.TabPages.Add(Main);
                    }

                    if (StartTabControl.TabPages.Contains(Start))
                    {
                        StartTabControl.TabPages.Remove(Start);
                    }

                }
                if (!_islogedIn)
                {
                    if (StartTabControl.TabPages.Contains(Main))
                    {
                        StartTabControl.TabPages.Remove(Main);
                    }

                    if (!StartTabControl.TabPages.Contains(Start))
                    {
                        StartTabControl.TabPages.Add(Start);
                    }
                }
            }


        }
        private string _transstatus = "NewTransaction";
        private string _transsubstatus;
        public string TransStatus
        {
            get { return _transstatus; }
            set
            {
                _transstatus = value;

                toolStripStatus.Text = _transstatus;

                foreach (TabPage tb in MainTabControl.TabPages)
                {
                    MainTabControl.TabPages.Remove(tb);
                }

                if (_transstatus == "NewTransaction")
                {
                    //POSGridView.DataSource = transtemp.TransactionDetails;
                    totalquantity = 0;
                    TotalAmounttoPay = 0;
                    PaidAmount = 0;
                    TotalQuantity_TextBox.Text = "0";
                    AmountforPay_TextBox.Text = "0";
                    BalanceAmount_TextBox.Text = "0";
                    transdetailstemplist = new BindingList<TransactionDetailTEMP>();
                    payments = new BindingList<TransactionPayment>();
                    toolStripStatusLabel4.Text = shift.CashierID;
                    toolStripStatusLabel6.Text = shift.ShiftStartDate.ToShortDateString();
                    UpdateTotalArea();

                    CreatePOSGrid();
                    if (!MainTabControl.TabPages.Contains(NewTransaction))
                    {
                        MainTabControl.TabPages.Add(NewTransaction);
                    }
                }


                if (_transstatus == "Sales")
                {
                    POSGridView.DataSource = transtemp.TransactionDetails;
                    CreatePOSGrid();
                    if (!MainTabControl.TabPages.Contains(SalesTab))
                    {
                        MainTabControl.TabPages.Add(SalesTab);
                    }
                    toolStripStatus.Text = "Sales";
                    toolStripStatusLabel2.Text = transtemp.TransactionNo;
                }
                if (_transstatus == "Return")
                {

                    POSGridView.DataSource = transtemp.TransactionDetails;
                    CreatePOSGrid();
                    if (!MainTabControl.TabPages.Contains(SalesTab))
                    {
                        MainTabControl.TabPages.Add(SalesTab);
                    }

                    toolStripStatus.Text = "Return";
                    //if (!MainTabControl.TabPages.Contains(Return))
                    //{
                    //    MainTabControl.TabPages.Add(Return);
                    //}
                }
                if (_transstatus == "Retreive")
                {
                    if (!MainTabControl.Contains(Transactionstab))
                    {
                        MainTabControl.TabPages.Add(Transactionstab);
                    }
                }

                if (_transstatus == "PayOnDelivery")
                {
                    if (!MainTabControl.Contains(PayOnDelivery))
                    {
                        MainTabControl.TabPages.Add(PayOnDelivery);
                    }
                }

                if (_transstatus == "Delivery")
                {
                    if (!MainTabControl.Contains(ReceivePayments))
                    {
                        MainTabControl.TabPages.Add(ReceivePayments);
                    }
                }

                SearchBox.Focus();

            }

        }
        public string TransSubStatus
        {
            get
            {
                return _transsubstatus;
            }
            set
            {
                _transsubstatus = value;
                toolStripSubStatus.Text = _transsubstatus;
                if (value == "Payment")
                {
                    foreach (TabPage tb in MainTabControl.TabPages)
                    {
                        MainTabControl.TabPages.Remove(tb);
                    }
                    if (!MainTabControl.TabPages.Contains(Payment))
                    {
                        MainTabControl.TabPages.Add(Payment);
                    }
                    this.toolStripSubStatus.Text = "Payment";
                }
                

                if (value == "")
                {
                    TransStatus = TransStatus;
                }

            }
        }
        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        private DateTime TransDate;
        TransactionHeaderTEMP transtemp = new TransactionHeaderTEMP();
        List<POS_MenuLine> MenuLines = POS_MenuLineRepo.POS_MenuLines();

        BindingList<TransactionDetailTEMP> transdetailstemplist = new BindingList<TransactionDetailTEMP>();
        BindingList<TransactionPayment> payments = new BindingList<TransactionPayment>();

        #endregion


        public POSForm()
        {
            InitializeComponent();
            CreatePOSGrid();
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int w = screen.Width;
            int h = screen.Height - 25;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);
            //MessageBox.Show("This Screen Resolution " + w.ToString() + ", " + h.ToString());
            StartTabControl.Width = w;
            //MainTabControl.Width = w;
            //MessageBox.Show("MainTab " + MainTabControl.Width + " , POSLayout " + POSLayout.Width + ", Menu Layout " + MenuLayout.Width);
            //MessageBox.Show("MainTab Height " + MainTabControl.Height + " Menu Height " + MenuLayout.Height + ", Transpanel height " + NewTransactionPanel.Height);
            this.SuspendLayout();
            numericbutton  = new Button[] { numeric_0, numeric_1, numeric_2, numeric_3, numeric_4, numeric_5, numeric_6, numeric_7, numeric_8, numeric_9, numeric_dot, numeric_asterick,  numeric_bs, numeric_plus, numeric_minus, numeric_enter };
            int bi = 0;
            foreach(Button bt in numericbutton)
            {
                int bx = bi;
                numericbutton[bx].Click += (sender1, args) => numericbuttonclickevent(bx);
                
                bi += 1;
            }

            MenuLayout.Width = 300;
            MenuLayout.Height = h - 20;
            MainTabControl.Height = h - 20;
            NewTransactionPanel.Height = h - 20;
            SalesButtonLayoutPanel.Height = h - 20;
            paymentbuttonpanel.Height = h - 20;
            PayOnDeliveryPanel.Height = h - 20;
            ReceivePaymentPanel.Height = h - 20;
            
            POSLayout.Width = StartTabControl.Width - MenuLayout.Width - 20;
            POSLayout.Height = h - 20;

            NewTransactionPanel.BackColor = Color.Black;
            SalesButtonLayoutPanel.BackColor = Color.Black;
            paymentbuttonpanel.BackColor = Color.Black;
            //MenuLayout.Location = new Point(POSLayout.Width + 5, 5);
            flowLayoutPanel1.Height = h - 120;
            POSGridView.Width = POSLayout.Width - POSSubLayout2.Width - 25;
            POSGridView.Height = flowLayoutPanel1.Height / 2;
            flowLayoutPanel1.Width = POSLayout.Width - POSSubLayout2.Width - 25;
            flowLayoutPanel2.Width = POSLayout.Width - POSSubLayout2.Width - 25;

            flowLayoutPanel3.Width = POSLayout.Width - POSSubLayout2.Width - 25;
            
            button1.Width = flowLayoutPanel3.Width - 5;
            button1.BackColor = Color.DarkGreen;
            button1.ForeColor = Color.White;
            button2.Width = flowLayoutPanel3.Width - 5;
            button2.BackColor = Color.RosyBrown;

            flowLayoutPanel2.Height = flowLayoutPanel1.Height / 2 -20;
            flowLayoutPanel3.Height = 140;
               flowLayoutPanel4.Width = POSLayout.Width - POSSubLayout2.Width - 25;
               flowLayoutPanel4.Height = flowLayoutPanel2.Height -170; 

            QuickPanel.Height = 150;
            QuickPanel.Width = POSSubLayout2.Width - 5;

            this.ResumeLayout();
            //  MessageBox.Show("MainTab " + MainTabControl.Width +  " , POSLayout " + POSLayout.Width+", Menu Layout " + MenuLayout.Width);
            // MessageBox.Show("MainTab Height "+ MainTabControl.Height + " Menu Height " + MenuLayout.Height + ", Transpanel height " + NewTransactionPanel.Height);
            List<POS_PaymentType> paymenttypes = POS_Repositery.GetAllPaymentTypes();
            


            int paymentcount = MenuLines.Where(m => m.MenuHeader == "Payment").Count();
            Button[] paymentbutton = new Button[paymentcount ];
            Button[] mainbutton = new Button[MenuLines.Where(m => m.MenuHeader == "MainMenu").Count() ];
            Button[] salesbutton = new Button[MenuLines.Where(m => m.MenuHeader == "Sales").Count() ];

            int i = 0;
            int paymentmenucount = MenuLines.Where(m => m.MenuHeader == "Payment").Count();
            foreach (POS_MenuLine menuline in MenuLines.Where(m => m.MenuHeader == "Payment").OrderBy( m => m.MenuSortOrder))
            {
                int x = i;
                paymentbutton[x] = new Button();
                paymentbutton[x].Text = menuline.MenuDescription;
                paymentbutton[x].Name = menuline.MenuCommand;
                paymentbutton[x].Width = paymentbuttonpanel.Width - 5;
                paymentbutton[x].Height = paymentbuttonpanel.Height / paymentmenucount - paymentmenucount;
                paymentbutton[x].BackColor = Color.FromName(menuline.MenuColor);
                paymentbutton[x].FlatStyle = FlatStyle.Flat;
                paymentbutton[x].FlatAppearance.BorderColor = Color.FromName(menuline.MenuColor);
                paymentbutton[x].ForeColor = Color.FromArgb(Color.FromName(menuline.MenuColor).ToArgb() ^ 0xffffff);
                paymentbutton[x].Font = new Font("Segoe UI Light", 13f);
                paymentbutton[x].Click += (sender1, args) => PaymentFunction(paymentbutton[x].Name, menuline.MenuParameter);
                paymentbuttonpanel.Controls.Add(paymentbutton[x]);
                i += 1;
            }
            i = 0;
            int mainmenucount = MenuLines.Where(m => m.MenuHeader == "MainMenu").Count();
            int mainbuttonheight = NewTransactionPanel.Height / mainmenucount - mainmenucount;
            foreach (POS_MenuLine menuline in MenuLines.Where(m => m.MenuHeader == "MainMenu").OrderBy(m => m.MenuSortOrder))
            {
                int x = i;
                mainbutton[x] = new Button();

                

                
                
                mainbutton[x].Name = menuline.MenuCommand;
                mainbutton[x].Width = NewTransactionPanel.Width - 5;
                mainbutton[x].Height = mainbuttonheight;
                mainbutton[x].Text = menuline.MenuDescription;
                mainbutton[x].BackColor = Color.FromName(menuline.MenuColor);
                mainbutton[x].FlatStyle = FlatStyle.Flat;
                mainbutton[x].FlatAppearance.BorderColor = Color.FromName(menuline.MenuColor);
                mainbutton[x].ForeColor = Color.FromArgb(Color.FromName(menuline.MenuColor).ToArgb() ^ 0xffffff);
                mainbutton[x].Font = new Font("Segoe UI Light", 13f);
                mainbutton[x].Click += (sender1, args) => MainFunction(mainbutton[x].Name);
                NewTransactionPanel.Controls.Add(mainbutton[x]);
                i = i + 1;

            }
            

            int salesmenucount = MenuLines.Where(m => m.MenuHeader == "Sales").Count();
            int salesbuttonheight = SalesButtonLayoutPanel.Height / salesmenucount - salesmenucount;
            i = 0;
            foreach (POS_MenuLine menuline in MenuLines.Where(m => m.MenuHeader == "Sales").OrderBy(m => m.MenuSortOrder))
            {
                int x = i;
                salesbutton[x] = new Button();
                
                salesbutton[x].Text = menuline.MenuDescription ;
                
                salesbutton[x].Name = menuline.MenuCommand;
                salesbutton[x].Width = SalesButtonLayoutPanel.Width - 5;
                salesbutton[x].Height = salesbuttonheight;
                salesbutton[x].BackColor = Color.FromName(menuline.MenuColor);
                salesbutton[x].FlatStyle = FlatStyle.Flat;
                salesbutton[x].FlatAppearance.BorderColor = Color.FromName(menuline.MenuColor);
                salesbutton[x].ForeColor = Color.FromArgb(Color.FromName(menuline.MenuColor).ToArgb() ^ 0xffffff);
                salesbutton[x].Font = new Font("Segoe UI Light", 13f);
                salesbutton[x].Click += (sender1, args) => SalesFunction(salesbutton[x].Name);
                SalesButtonLayoutPanel.Controls.Add(salesbutton[x]);
                i = i + 1;

            }
            int quickmenucount = MenuLines.Where(m => m.MenuHeader == "QuickMenu").Count();
            int quickbuttonheight = QuickPanel.Height / quickmenucount - 10;
            Button[] quickbutton = new Button[quickmenucount];
            i = 0;
            foreach (POS_MenuLine menuline in MenuLines.Where(m => m.MenuHeader == "QuickMenu").OrderBy(m => m.MenuSortOrder))
            {
                int x = i;
                quickbutton[x] = new Button();
                quickbutton[x].Text = menuline.MenuDescription;
                quickbutton[x].Name = menuline.MenuCommand;
                quickbutton[x].Width = QuickPanel.Width - 5;
                quickbutton[x].Height = quickbuttonheight;
                quickbutton[x].BackColor = Color.FromName(menuline.MenuColor);
                quickbutton[x].FlatStyle = FlatStyle.Flat;
                quickbutton[x].FlatAppearance.BorderColor = Color.FromName(menuline.MenuColor);
                quickbutton[x].ForeColor = Color.FromArgb(Color.FromName(menuline.MenuColor).ToArgb() ^ 0xffffff);
                quickbutton[x].Font = new Font("Segoe UI Light", 13f);
                quickbutton[x].Click += (sender1, args) => QuickFunction(quickbutton[x].Name);
                QuickPanel.Controls.Add(quickbutton[x]);
                i += 1;
            }

            Button[] retreivebutton = new Button[6];
            int retreivebuttonheight = TransactionLayoutPanel.Height / 6 - 5;
            i = 0;
            foreach (POS_MenuLine menuline in MenuLines.Where(m => m.MenuHeader == "RetreiveSuspended").OrderBy(m => m.MenuSortOrder))
            {
                int x = i;
                retreivebutton[x] = new Button();
                retreivebutton[x].Text = menuline.MenuDescription;
                retreivebutton[x].Name = menuline.MenuCommand;
                retreivebutton[x].Width = TransactionLayoutPanel.Width;
                retreivebutton[x].Height = retreivebuttonheight;
                retreivebutton[x].BackColor = Color.FromName(menuline.MenuColor);
                retreivebutton[x].FlatStyle = FlatStyle.Flat;
                retreivebutton[x].FlatAppearance.BorderColor = Color.FromName(menuline.MenuColor);
                retreivebutton[x].ForeColor = Color.FromArgb(Color.Brown.ToArgb() ^ 0xffffff);
                retreivebutton[x].Font = new Font("Segoe UI Light", 13f);
                retreivebutton[x].Click += (sender1, args) => RetreiveFunction(retreivebutton[x].Name);
                TransactionLayoutPanel.Controls.Add(retreivebutton[x]);
                i += 1;
            }

            Button[] payondeliverybutton = new Button[6];
            
            i = 0;
            List<POS_DeliveryBoy> Deliveryboys = POS_Repositery.GetDeliveryBoys();
            int payondeliverybuttonheight = PayOnDeliveryPanel.Height / (Deliveryboys.Count+1) - 5;
            foreach (POS_DeliveryBoy deliveryboy in Deliveryboys)
            {
                int x = i;
                payondeliverybutton[x] = new Button();
                payondeliverybutton[x].Text = deliveryboy.DeliveryBoyName;
                payondeliverybutton[x].Name = deliveryboy.DeliveryBoyID;
                payondeliverybutton[x].Width = PayOnDeliveryPanel.Width;
                payondeliverybutton[x].Height = payondeliverybuttonheight;
                payondeliverybutton[x].BackColor = Color.FromName("Purple"); 
                payondeliverybutton[x].FlatStyle = FlatStyle.Flat;
                payondeliverybutton[x].FlatAppearance.BorderColor = Color.FromName("Purple");  
                payondeliverybutton[x].ForeColor = Color.FromArgb(Color.FromName("Purple").ToArgb() ^ 0xffffff);
                payondeliverybutton[x].Font = new Font("Segoe UI Light", 13f);
                payondeliverybutton[x].Click += (sender1, args) => DeliveryFunction(payondeliverybutton[x].Name);
                PayOnDeliveryPanel.Controls.Add(payondeliverybutton[x]);
                i += 1;

                if( i == Deliveryboys.Count )
                {
                    int y = i;
                    payondeliverybutton[y] = new Button();
                    payondeliverybutton[y].Text = "Back";
                    payondeliverybutton[y].Name = "Back";
                    payondeliverybutton[y].Width = PayOnDeliveryPanel.Width;
                    payondeliverybutton[y].Height = payondeliverybuttonheight;
                    payondeliverybutton[y].BackColor = Color.FromName("Yellow");
                    payondeliverybutton[y].FlatStyle = FlatStyle.Flat;
                    payondeliverybutton[y].FlatAppearance.BorderColor = Color.FromName("Yellow");
                    payondeliverybutton[y].ForeColor = Color.FromArgb(Color.FromName("Yellow").ToArgb() ^ 0xffffff);
                    payondeliverybutton[y].Font = new Font("Segoe UI Light", 13f);
                    payondeliverybutton[y].Click += (sender1, args) => DeliveryFunction(payondeliverybutton[y].Name);
                    PayOnDeliveryPanel.Controls.Add(payondeliverybutton[y]);

                }

            }
            i = 0;
            Button[] receivepaymentbutton = new Button[6];
            int receipaymentbuttonheight = ReceivePaymentPanel.Height / MenuLines.Where(a => a.MenuHeader == "Receive Payments").Count() - 5;
            foreach (POS_MenuLine menuline in MenuLines.Where(m => m.MenuHeader == "Receive Payments").OrderBy(m => m.MenuSortOrder))
            {
                int x = i;
                receivepaymentbutton[x] = new Button();
                receivepaymentbutton[x].Text = menuline.MenuDescription;
                receivepaymentbutton[x].Name = menuline.MenuCommand;
                receivepaymentbutton[x].Height = receipaymentbuttonheight;
                receivepaymentbutton[x].Width = ReceivePaymentPanel.Width;
                receivepaymentbutton[x].BackColor = Color.FromName( menuline.MenuColor);
                receivepaymentbutton[x].FlatStyle = FlatStyle.Flat;
                receivepaymentbutton[x].FlatAppearance.BorderColor = Color.FromName(menuline.MenuColor);
                receivepaymentbutton[x].ForeColor = Color.FromArgb(Color.FromName(menuline.MenuColor).ToArgb() ^ 0xffffff);
                receivepaymentbutton[x].Font = new Font("Segoe UI Light", 13f);
                receivepaymentbutton[x].Click += (sender1, args) => ReceivePaymentFunction(receivepaymentbutton[x].Name);
                ReceivePaymentPanel.Controls.Add(receivepaymentbutton[x]);
                i += 1;


            }


        }

        private void numericbuttonclickevent(int val)
        {
            if (val <= 9)
            {
                SearchBox.Text += val.ToString();
                return;
            }

            if(val == 10)
            {
                if (!SearchBox.Text.Contains("."))
                {
                    SearchBox.Text += ".";
                    return;
                }
            }
            if (val == 11)
            {
                if (TransStatus == "Sales" || TransStatus == "Return") TransSubStatus = "ChangeQty";
            }

            if (val == 12)
            {
                if(SearchBox.Text.Length > 0)
                SearchBox.Text = SearchBox.Text.Substring(0, SearchBox.Text.Length - 1);

            }

            if (val == 13)
            {
                float qty = (float) POSGridView.CurrentRow.Cells["quantity"].Value;
                if (qty > 0)
                {
                    TransSubStatus = "ChangeQty";
                    SearchBox.Text = (qty + 1).ToString();
                    SendKeys.Send("{ENTER}");

                }
            }

            if (val == 14)
            {
                float qty = (float)POSGridView.CurrentRow.Cells["quantity"].Value;
                if (qty > 1)
                {
                    TransSubStatus = "ChangeQty";
                    SearchBox.Text = (qty - 1).ToString();
                    SendKeys.Send("{ENTER}");
                    return;

                }
                if (qty == 1)
                {
                    SalesFunction("VoidLine");

                }

            }

            if(val == 15)
            {
                SendKeys.Send("{ENTER}");
            }
            

        }

        private void ReceivePaymentFunction(string name)
        {
            if (name == "Back")
            {
                TransStatus = "NewTransaction";
                return;
            }
            if (name == "ReceivePayment")
            {
                transtemp = POS_Repositery.ReceivePayments(shift.ShiftNo, shift.Branch, shift.TillID, POSGridView.CurrentRow.Cells[0].Value.ToString());

                if (transtemp != null)
                {
                    TransactionDetailTEMP trns = new TransactionDetailTEMP();
                    List<TransactionDetailTEMP> tlist = POS_Repositery.TransactionDetailsTemp(shift.ShiftNo, shift.Branch, shift.TillID, POSGridView.CurrentRow.Cells[0].Value.ToString());
                    TransStatus = transtemp.TransactionType;
                    transtemp.TransactionDetails = tlist;
                    transtemp.Update();
                    transdetailstemplist = new BindingList<TransactionDetailTEMP>(tlist);
                    //transtemp.TransactionDetails = POS_Repositery.TransactionDetailsTemp(shift.ShiftNo, shift.Branch, shift.TillID, POSGridView.CurrentRow.Cells[0].Value.ToString());
                    //transdetailstemplist = new BindingList<TransactionDetailTEMP>(transtemp.TransactionDetails);
                    POSGridView.DataSource = transdetailstemplist;
                    UpdateTotalArea();

                }



            }


        }

        private void DeliveryFunction(string name)
        {
            if (name == "Back")
            {
                TransStatus = "NewTransaction";
                return;
            }

            
            POS_DeliveryBoy deliveryboy = deliveryboys.Where(d => d.DeliveryBoyID == name).FirstOrDefault();

           List<TransactionHeaderTEMP> payondeliverytransactions = POS_Repositery.PayonDeliveryTransactions();
            if(POSGridView.CurrentRow.Cells[9].Value.ToString() != "PayOnDelivery")
            {
                MessageLabel.Text = "The Current Transaction is not for Delivery";
                System.Media.SystemSounds.Beep.Play();
                return;
            }

            transtemp = payondeliverytransactions.Where(a => a.TransactionNo == POSGridView.CurrentRow.Cells[0].Value.ToString()).FirstOrDefault();
                
            if (transtemp == null)
            {
                MessageBox.Show("transaction null");
                return;
            }

            if (deliveryboy.AddTransaction(transtemp))
            {
                transtemp.TransactionStatus = "Delivery";
                transtemp.Update();
                MainFunction("Delivery");
            }




        }

        private void RetreiveFunction(string name)
        {
           if(name == "Retreive")
            {

               // MessageBox.Show(POSGridView.CurrentRow.Cells[0].Value.ToString());
                transtemp =  POS_Repositery.RetreiveTransaction(shift.ShiftNo, shift.Branch, shift.TillID, POSGridView.CurrentRow.Cells[0].Value.ToString());

                if  (transtemp != null)
                {
                    TransactionDetailTEMP trns = new TransactionDetailTEMP();
                    List<TransactionDetailTEMP> tlist = POS_Repositery.TransactionDetailsTemp(shift.ShiftNo, shift.Branch, shift.TillID, POSGridView.CurrentRow.Cells[0].Value.ToString());
                    TransStatus = transtemp.TransactionType;
                    transtemp.TransactionDetails = tlist;
                    transtemp.Update();
                    transdetailstemplist = new BindingList<TransactionDetailTEMP>(tlist);
                    //transtemp.TransactionDetails = POS_Repositery.TransactionDetailsTemp(shift.ShiftNo, shift.Branch, shift.TillID, POSGridView.CurrentRow.Cells[0].Value.ToString());
                    //transdetailstemplist = new BindingList<TransactionDetailTEMP>(transtemp.TransactionDetails);

                    POSGridView.DataSource = transdetailstemplist;
                    CreatePOSGrid();
                    UpdateTotalArea();




                }
                return;
            }
           if(name == "Back")
            {
                TransStatus = "NewTransaction";
                return;
            }
        }

        private void QuickFunction(string quickfunction)
        {
            if(quickfunction == "PriceCheck")
            {
               
                TransSubStatus = "PriceCheck";

            }
            if(quickfunction == "PrintLastTransaction")
            {
                PrintFunction("Last"); 
            }
            if(quickfunction == "CustomerInfo")
            {
                using (var frm = new CustomerInfoForm())
                {

                    var result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        POS_Customer customer = new POS_Customer();
                        customer.CustomerMobileNo = frm.CustomerMobileNo;
                        customer.CustomerName = frm.CustomerName;
                        customer.CustomerAddress1 = frm.CustomerAddress1;
                        customer.CustomerAddress2 = frm.CustomerAddress2;
                        customer.IsCreditCustomer = frm.IsCreditCustomer;
                        customer.CustomerLedger = frm.CustomerLedger;
                        transtemp.UpdateCustomerInfo(customer);

                    }

                }


            }
    
        }

        private void MainFunction(String mainfunction)
        {
            if (mainfunction == "StartSales")
            {
                if (TransStatus == "NewTransaction")
                {
                    transtemp = new TransactionHeaderTEMP(shift, "Sales");
                    TransStatus = "Sales";
                    SearchBox.Focus();
                }
                else
                {
                    MessageLabel.Text = "Close Current Transaction to initiate new transactions ! ";
                }

            }

            if (mainfunction == "StartReturn")
            {
                if (TransStatus == "NewTransaction")
                {
                    transtemp = new TransactionHeaderTEMP(shift, "Return");
                    TransStatus = "Return";
                    SearchBox.Focus();
                }
                else
                {
                    MessageLabel.Text = "Close Current Transaction to initiate new transactions ! ";
                }
            }
            if (mainfunction == "LogOff")
            {
                IsLogedIn = false;
            }

            if (mainfunction == "RetreiveSuspended")
            {
                BindingList<TransactionHeaderTEMP> suspendedtransactions = new BindingList<TransactionHeaderTEMP>(POS_Repositery.SuspendedTransactions());
                
                this.SuspendLayout();
                POSGridView.AutoGenerateColumns = false;
                POSGridView.DataSource = suspendedtransactions;
                POSGridView.Columns.Clear();
                
                POSGridView.Columns.Add("transno", "TransactionNo");
                POSGridView.Columns.Add("transdate", "Date");
                POSGridView.Columns.Add("transtime", "Time");
                POSGridView.Columns.Add("Totqty", "Quantity");
                POSGridView.Columns.Add("amountpay", "Amount");

                
                POSGridView.Columns[0].DataPropertyName = "TransactionNo";
                POSGridView.Columns[1].DataPropertyName = "TransactionDate";
                POSGridView.Columns[2].DataPropertyName = "TransactionTime";
                POSGridView.Columns[3].DataPropertyName = "TotalQty";
                POSGridView.Columns[4].DataPropertyName = "AmountForPay";
                POSGridView.Refresh();
                this.ResumeLayout();
                this.Refresh();


                
                TransStatus = "Retreive";

            }

            if(mainfunction == "Retreive_CashOnDelivery")
            {
                BindingList<TransactionHeaderTEMP> suspendedtransactions = new BindingList<TransactionHeaderTEMP>(POS_Repositery.DeliveryTransactions());

                this.SuspendLayout();
                POSGridView.AutoGenerateColumns = false;
                POSGridView.DataSource = suspendedtransactions;
                POSGridView.Columns.Clear();




                POSGridView.Columns.Add("transno", "TransactionNo");
                POSGridView.Columns.Add("transdate", "TransactionDate");
               
                POSGridView.Columns.Add("Totqty", "TotalQty");
                POSGridView.Columns.Add("amountpay", "Amount");
                POSGridView.Columns.Add("mobileno", "MobileNo");
                POSGridView.Columns.Add("customername", "Customer Name");
                POSGridView.Columns.Add("address1", "Address 1");
                POSGridView.Columns.Add("address2", "Address 2");
                POSGridView.Columns.Add("status", "Status");
                DataGridViewCellStyle amountcellStyle = new DataGridViewCellStyle();
                amountcellStyle.Format = "N2";
                

                POSGridView.Columns[0].DataPropertyName = "TransactionNo";
                POSGridView.Columns[1].DataPropertyName = "TransactionDate";
                
                POSGridView.Columns[2].DataPropertyName = "TotalQty";
                POSGridView.Columns[3].DataPropertyName = "AmountForPay";
                
                POSGridView.Columns[4].DataPropertyName = "CustomerMobileNo";
                POSGridView.Columns[5].DataPropertyName = "CustomerName";
                POSGridView.Columns[6].DataPropertyName = "CustomerAddress1";
                POSGridView.Columns[7].DataPropertyName = "CustomerAddress2";
                POSGridView.Columns[8].DataPropertyName = "TransactionStatus";
                POSGridView.Columns[8].Visible = false;
                POSGridView.Refresh();
                this.ResumeLayout();
                this.Refresh();

                TransStatus = "Delivery";
            }

            if (mainfunction == "Delivery")
            {
                BindingList<TransactionHeaderTEMP> suspendedtransactions = new BindingList<TransactionHeaderTEMP>(POS_Repositery.PayonDeliveryTransactions());
                
                this.SuspendLayout();
                POSGridView.AutoGenerateColumns = false;
                POSGridView.DataSource = suspendedtransactions;
                POSGridView.Columns.Clear();
                

              

                POSGridView.Columns.Add("transno", "TransactionNo");
                POSGridView.Columns.Add("transdate", "TransactionDate");
                POSGridView.Columns.Add("transtime", "TransactionTime");
                POSGridView.Columns.Add("Totqty", "TotalQty");
                POSGridView.Columns.Add("amountpay", "Amount");
                POSGridView.Columns.Add("mobileno", "MobileNo");
                POSGridView.Columns.Add("customername", "Customer Name");
                POSGridView.Columns.Add("address1", "Address 1");
                POSGridView.Columns.Add("address2", "Address 2");
                POSGridView.Columns.Add("status", "Status");
                DataGridViewCellStyle amountcellStyle = new DataGridViewCellStyle();
                amountcellStyle.Format = "N2";
                DataGridViewCellStyle timecell = new DataGridViewCellStyle();
                timecell.Format = "HH:MM:SS";
                
                POSGridView.Columns[0].DataPropertyName = "TransactionNo";
                POSGridView.Columns[1].DataPropertyName = "TransactionDate";
                POSGridView.Columns[2].DataPropertyName = "TransactionTime";
                POSGridView.Columns[2].DefaultCellStyle = timecell;
                POSGridView.Columns[3].DataPropertyName = "TotalQty";
                POSGridView.Columns[4].DataPropertyName = "AmountForPay";
                POSGridView.Columns[4].DefaultCellStyle = amountcellStyle;
                POSGridView.Columns[5].DataPropertyName = "CustomerMobileNo";
                POSGridView.Columns[6].DataPropertyName = "CustomerName";
                POSGridView.Columns[7].DataPropertyName = "CustomerAddress1";
                POSGridView.Columns[8].DataPropertyName = "CustomerAddress2";
                POSGridView.Columns[9].DataPropertyName = "TransactionStatus";
                POSGridView.Columns[9].Visible = false;
                POSGridView.Refresh();
                this.ResumeLayout();
                this.Refresh();

                TransStatus = "PayOnDelivery";

            }

            if(mainfunction == "Transactions")
            {
                BindingList<TransactionHeaderTEMP> transactions = new BindingList<TransactionHeaderTEMP>(POS_Repositery.GetAllTransactions(shift.ShiftNo));

                this.SuspendLayout();
                POSGridView.AutoGenerateColumns = false;
                POSGridView.DataSource = transactions;
                POSGridView.Columns.Clear();

                POSGridView.Columns.Add("transno", "TransactionNo");
                POSGridView.Columns.Add("transdate", "TransactionDate");
                POSGridView.Columns.Add("transtime", "TransactionTime");
                POSGridView.Columns.Add("Totqty", "TotalQty");
                POSGridView.Columns.Add("amountpay", "AmounttoPay");

                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "Print Copy";
                DataGridViewCellStyle cstyle = new DataGridViewCellStyle();
                
                btn.Text = "Print";

                btn.Name = "printcopy";
                POSGridView.CellButtonClick += (sender1, keys) => PrintThisTransaction(shift.Branch, POSGridView.CurrentRow.Cells[0].Value.ToString());
                    

                POSGridView.Columns.Add(btn);
                

                POSGridView.Columns[0].DataPropertyName = "TransactionNo";
                POSGridView.Columns[1].DataPropertyName = "TransactionDate";
                POSGridView.Columns[2].DataPropertyName = "TransactionTime";
                POSGridView.Columns[3].DataPropertyName = "TotalQty";
                POSGridView.Columns[4].DataPropertyName = "AmountForPay";
                
                POSGridView.Refresh();
                this.ResumeLayout();
                this.Refresh();
    


            }
            
            if (mainfunction == "EndOfShift")
            {
                if (shift.ShiftClose())
                    IsLogedIn = false;
                else MessageBox.Show("Shift Not Closed!!!");
            }
            if (mainfunction == "XReport")
            {
                PrintFunction("XReport");
            }

        }

        private void PrintThisTransaction(string branch, String transno)
        {
            selectedtransaction = POS_Repositery.GetATransaction(branch, transno);
            MessageBox.Show(selectedtransaction.TransactionNo);
            PrintFunction("Selected");

        }

        private void ItemMenuLoad()
        {
            this.SuspendLayout();
        /*   FlowLayoutPanel itemmenulayout = new FlowLayoutPanel();
            //   POSGridView.Height = POSGridView.Height / 2;
            itemmenulayout.Width = flowLayoutPanel2.Width;
            itemmenulayout.Height = flowLayoutPanel2.Height - 50;
          //  itemmenulayout.Location = new Point(POSGridView.Location.X, POSGridView.Location.Y + POSGridView.Height);
            
            itemmenulayout.FlowDirection = FlowDirection.LeftToRight;
            itemmenulayout.Name = "ItemMenuLayout";  */
            
            List<String> categories = POS_Repositery.POSMenuCategories();
            
            Button[] categorybuttons = new Button[categories.Count()];
            int i = 0;
            foreach(string category in categories)
            {
                int x = i;
                categorybuttons[x] = new Button();
                categorybuttons[x].Name = category;
                categorybuttons[x].Text = category;
                categorybuttons[x].Width = 170;
                categorybuttons[x].Height = 60;
                categorybuttons[x].BackColor = Color.DarkGreen;
                categorybuttons[x].ForeColor = Color.White;
                categorybuttons[x].FlatStyle = FlatStyle.Flat;
                categorybuttons[x].FlatAppearance.BorderColor = Color.DarkGreen;
                categorybuttons[x].Font = new Font("Segoe UI Light", 13f);
                categorybuttons[x].Click += (sender1, keys) => categorybuttonpressed(categorybuttons[x].Name);
                flowLayoutPanel3.Controls.Add(categorybuttons[x]);
                //if (x == categories.Count() - 1) flowLayoutPanel2.SetFlowBreak(categorybuttons[x], true);
                i += 1;
            }
            
            
            this.ResumeLayout();
            this.Refresh();


        }

        private void categorybuttonpressed(string name)
        {
            List<POS_ItemMenu> itemMenus = POS_Repositery.GetItemMenus().Where(a => a.ItemCategory == name).ToList();

            Button[] itembuttons = new Button[itemMenus.Count()];
            flowLayoutPanel4.Controls.Clear();
            int i = 0;
            foreach (POS_ItemMenu itemMenu in itemMenus)
            {
                int x = i;
                itembuttons[x] = new Button();
                itembuttons[x].Name = itemMenu.Barcode;
                itembuttons[x].Text = itemMenu.MenuDescription;
                itembuttons[x].Width = 200;
                itembuttons[x].Height = 60;
                itembuttons[x].BackColor = Color.RosyBrown;
                itembuttons[x].ForeColor = Color.White;
                itembuttons[x].FlatStyle = FlatStyle.Flat;
                itembuttons[x].FlatAppearance.BorderColor = Color.RosyBrown;
                itembuttons[x].Font = new Font("Segoe UI Light", 10f);
                itembuttons[x].Click += (sender1, keys) => ItemButtonPressed(itembuttons[x].Name);
                
                flowLayoutPanel4.Controls.Add(itembuttons[x]);
                this.Refresh();
                i += 1;
            }


        }

        private void ItemButtonPressed(string name)
        {
            SearchBox.Text = name;
            SendKeys.Send("{ENTER}");
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            IsLogedIn = false;

            POS_Setup setup = new POS_Setup();
            if (setup.IsSingleShift)
            {
                if (login.IsAuthorized(setup.CashierID, setup.CashierPassword))
                {

                    if (shift.GetOpenShift(setup.CashierID))
                    {
                        IsLogedIn = true;
                    }
                    else
                    {

                        if (supervisorlogin.IsAuthorized(setup.CashierID, setup.CashierPassword))
                        {
                            try
                            {
                                shift = new ShiftMasterDB(setup.CashierID, setup.DefaultTill, setup.LocalStoreNo, setup.CashierID);
                                if (shift != null) IsLogedIn = true;

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cashier is not a supervisor");

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid UserID or Password");
                }
                
            }


            //this.POSGridView.DataSource = transdetailstemplist;
            //LoginButton.PerformClick();
            if (IsLogedIn)
            {
                transtemp = POS_Repositery.GetOpenTransactions().Where(f => f.CashierID == shift.CashierID).FirstOrDefault();
                if (transtemp != null)
                { 
                TransStatus = transtemp.TransactionType;
                
                List<TransactionDetailTEMP> transdetail = POS_Repositery.TransactionDetailsTemp(shift.ShiftNo, shift.Branch, shift.TillID, transtemp.TransactionNo);
                transtemp.TransactionDetails = transdetail;
                transdetailstemplist = new BindingList<TransactionDetailTEMP>(transdetail);
                POSGridView.DataSource = transdetailstemplist;
                    CreatePOSGrid();
                    UpdateTotalArea();

                }

            }

            ItemMenuLoad();

        }

        private void ButtonStartSales_Click(object sender, EventArgs e)
        {

            if (TransStatus == "NewTransaction")
            {
                transtemp = new TransactionHeaderTEMP(shift, "Sales");
                TransStatus = "Sales";
                SearchBox.Focus();
            }
            else
            {
                MessageLabel.Text = "Close Current Transaction to initiate new transactions ! ";
            }

           



        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                SearchText();
                return true;
            }

            return false;
        }

        public void InsertTransaction()
        {
           
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            login.Username = PosUser.Text;
            login.Password = PosUserPassword.Text;
            //login.Username = "shan";
            //login.Password = "shanzafathima";


            if (login.IsAuthorized(login.Username, login.Password))
            {
                
                loginstatus.Text = "Login Successfull. Getting the Shift Status..";
                loginstatus.ForeColor = Color.Blue;
               // shift.GetOpenShift(login.Username); 
                if (shift.GetOpenShift(login.Username))
                {
                    IsLogedIn = true;
                }else
                {
                    loginstatus.Text = "Login Successfull. No open shift available. Contact Supervisor for new Shift!";
                    //shift = new ShiftMasterDB();
                }
            }
            else
            {
                IsLogedIn = false;
                loginstatus.Text = "Wrong Username or Password.";
                loginstatus.ForeColor = Color.Red;
            }


        }

        

        private void CreateShift_Click(object sender, EventArgs e)
        {
            supervisorlogin.Username = SupText.Text;
            supervisorlogin.Password = SupPwdText.Text;
            
            if (supervisorlogin.IsAuthorized(SupText.Text, SupPwdText.Text))
            {
                try
                {
                    shift = new ShiftMasterDB(SupText.Text, TillText.Text, BranchText.Text, PosUser.Text);
                    if (shift !=null) IsLogedIn = true;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                

            }
            else
            {
                MessageBox.Show("Invalid ID and Password!");
            }

            
        }

        private bool SearchText()
        {
            if (SearchBox.Text != "")
            {
              

                if (TransSubStatus == "Payment")
                {
                    PaymentFunction("Cash", SearchBox.Text);
                    return true; ;
                }
                if (TransSubStatus == "ChangeQty")
                {
                    ChangeQty();
                    TransSubStatus = "";
                    return true;
                }
                
                InvItemDirectoryUnits barcodes = new InvItemDirectoryUnits(SearchBox.Text);
                if (! barcodes.Equals(null) &&  barcodes.Barcode != "" && barcodes.ItemCode != "")
                {
                    if(TransSubStatus == "PriceCheck")
                    {
                        MessageLabel.Text = "Item Description & Price will be dispalyed Here";
                        SearchBox.Text = "";
                        TransSubStatus = "";
                        return true;
                    }
                    

                    if (TransStatus == "NewTransaction")
                    {
                        transtemp = new TransactionHeaderTEMP(shift, "Sales");
                        transdetailstemplist = new BindingList<TransactionDetailTEMP>();
                        TransStatus = "Sales";
                    }
                    


                    if (TransStatus == "Sales")
                    {
                        Sales(barcodes.Barcode);

                    }
                    if (TransStatus == "Return")
                    {
                        SalesReturn(barcodes.Barcode);
                    }


                    SearchBox.Text = "";
                    return true;

                }
                else
                {
                    MessageLabel.Text = "Item or Barcode does not exists";
                    MessageLabel.ForeColor = Color.Red;
                    return false;
                    
                }

                SearchBox.Focus();
                


            }

            return false;

        }

        

        private void textBox1_Leave(object sender, EventArgs e)
        {
            

        }

        public void SalesFunction(string menucommand)
        {

           


            if (menucommand == "ChangeQty")
            {
                ChangeQty();
            }
            if (menucommand == "GetItem")
            {
                POS_Item_Menu_Form itemform = new POS_Item_Menu_Form();
                using (var frm = new POS_Item_Menu_Form())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    var result = frm.ShowDialog();
                    if(result == DialogResult.OK)
                    {
                        Sales(frm.Barcode);
                    }
                }

            }
            if (menucommand == "VoidLine" && POSGridView.Rows.Count > 0)
            {
                int line_no = (int)POSGridView.CurrentRow.Cells["lineno"].Value;
                TransactionDetailTEMP trandetailtemp = transtemp.TransactionDetails.Where(d => d.LineNo == line_no).FirstOrDefault();
                    //transdetailstemplist.Where(d => d.TransactionNo == transtemp.TransactionNo && d.Branch == transtemp.Branch && d.LineNo == line_no).FirstOrDefault();
                trandetailtemp.Void();
                foreach(DataGridViewRow row in POSGridView.Rows)
                {
                    if (row.Cells["Void"].Value.ToString()== "True")
                    {
                        row.DefaultCellStyle.Font = new Font(POSGridView.Font, FontStyle.Strikeout);
                    }else
                    {
                        row.DefaultCellStyle.Font = new Font(POSGridView.Font, FontStyle.Regular);
                    }
                }
                transdetailstemplist = new BindingList<TransactionDetailTEMP>(transtemp.TransactionDetails);
                
                transtemp.TotalQty = transtemp.TransactionDetails.Where(d => d.VoidStatus == false).Sum(d => d.UOMQty);
                    
                transtemp.AmountForPay = transtemp.TransactionDetails.Where(d => d.VoidStatus == false).Sum(d => d.AmountForPay);
                transtemp.PaidAmount = TransactionPayment.PaidAmount(transtemp);
                UpdateTotalArea();

            }

            if (menucommand == "VoidTransactions")
            {
                DialogResult dr = MessageBox.Show("Do you want to Cancel Transaction", "Confirm", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    transtemp.VoidTransaction();
                    TransStatus = "NewTransaction";
                }
                    
                
            }

            if (menucommand == "Discount" && POSGridView.Rows.Count > 0)
            {
                if( SearchBox.Text == "")
                {
                    using (POS_Numeric_Input numbinput = new POS_Numeric_Input())
                    {
                        var result = numbinput.ShowDialog();
                        if(result == DialogResult.OK)
                        {
                            SearchBox.Text = numbinput.numericvalue.ToString();
                        }
                        else
                        {
                            return;
                        }
                    }

                }
                
                int line_no = (int)POSGridView.CurrentRow.Cells["lineno"].Value;
                TransactionDetailTEMP trandetailtemp = transtemp.TransactionDetails.Where(d => d.LineNo == line_no).FirstOrDefault();
                trandetailtemp.DiscountPercentage = Convert.ToSingle(SearchBox.Text);
                trandetailtemp.Update();
                transtemp.AmountForPay = transtemp.TransactionDetails.Where(d => d.VoidStatus == false).Sum(d => d.AmountForPay);
                transtemp.PaidAmount = TransactionPayment.PaidAmount(transtemp);
                transdetailstemplist = new BindingList<TransactionDetailTEMP>(transtemp.TransactionDetails);
                UpdateTotalArea();

                SearchBox.Text = "";

            }

            if (menucommand == "Payment")
            {
                if (TransStatus == "Sales" || TransStatus == "Return")
                {
                    TransSubStatus = "Payment";

                }
                else
                {
                    MessageLabel.Text = "Payment Cannot be Initiated.!!!";
                }
            }
            if (menucommand == "Suspend")
            {


                foreach (TransactionDetailTEMP trd in transtemp.TransactionDetails.Where(d => d.Updated == false))
                {

                    trd.Update();
                }
                if (!transtemp.Updated) transtemp.Update();
                if (transtemp.PaidAmount > 0)
                {
                    MessageLabel.Text = "Transaction with payment cannot be suspended.";
                    return;
                }else
                {

                    if (transtemp.TransactionStatus == "Delivery")
                    {
                        MessageLabel.Text = "PayOnDelivery Transaction.  Put on Delivery Mode";
                        transtemp = null;
                        transdetailstemplist = null;
                        POSGridView.DataSource = transdetailstemplist;
                        TransStatus = "NewTransaction";
                        return;
                    }


                    transtemp.TransactionStatus = "Suspened";
                    transtemp.Update();
                    transtemp = null;
                    transdetailstemplist = null;
                    POSGridView.DataSource = transdetailstemplist;
                    TransStatus = "NewTransaction";
                    return;
                }

            }

            
            if (menucommand == "PayOnDelivery")
            {
                foreach (TransactionDetailTEMP trd in transtemp.TransactionDetails.Where(d => d.Updated = false))
                {
                    trd.Update();
                }

                
                
                if (transtemp.PaidAmount > 0)
                {
                    MessageLabel.Text = "Transaction already paid.";
                    return;
                }
                else
                {
                    transtemp.Update();
                    if (transtemp.TransactionStatus == "Delivery")
                    {
                        MessageLabel.Text = "Transaction already on Delivery Mode";
                        transtemp = null;
                        transdetailstemplist = null;
                        POSGridView.DataSource = transdetailstemplist;
                        TransStatus = "NewTransaction";
                        return;
                    }

                    transtemp.TransactionStatus = "PayOnDelivery";
                    transtemp.Update();
                    using (var frm = new CustomerInfoForm())
                    {

                        var result = frm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            POS_Customer customer = new POS_Customer();
                            customer.CustomerLedger = frm.CustomerLedger;
                            customer.CustomerMobileNo = frm.CustomerMobileNo;
                            customer.CustomerName = frm.CustomerName;
                            customer.CustomerAddress1 = frm.CustomerAddress1;
                            customer.CustomerAddress2 = frm.CustomerAddress2;
                            transtemp.UpdateCustomerInfo(customer);

                        }

                    }


                    transtemp = null;
                    transdetailstemplist = null;
                    POSGridView.DataSource = transdetailstemplist;
                    TransStatus = "NewTransaction";
                    return;
                }
            }
        }

        public void Sales(string barcode)
        {

            



            TransactionDetailTEMP trandetailtemp = new TransactionDetailTEMP(transtemp, barcode);
            
            if (trandetailtemp.Updated)
            {
                transtemp.TransactionDetails.Add(trandetailtemp);
                
                MessageLabel.Text = trandetailtemp.ItemDescription + " @ " + trandetailtemp.PriceVAT.ToString() + " * " + trandetailtemp.Quantity.ToString();
               // transtemp.TransactionDetails.Add(trandetailtemp);
                transtemp.TotalQty = transtemp.TransactionDetails.Sum(q => q.Quantity);
                transtemp.AmountForPay = transtemp.TransactionDetails.Sum(a => a.AmountForPay);
                
                transdetailstemplist = new BindingList<TransactionDetailTEMP>(transtemp.TransactionDetails);
                UpdateTotalArea();
               
                this.POSGridView.DataSource = transdetailstemplist;
                this.POSGridView.Refresh();
                this.toolStripStatusLabel2.Text = transtemp.TransactionNo;
                this.Refresh();
            }
            else
            {
                MessageLabel.Text = "Error";
                System.Media.SystemSounds.Beep.Play();
            }

        }

        public void PaymentFunction(String paymentmethod, string paymentparameter)
        {
            if(paymentmethod == "Back")
            {
                TransSubStatus = "";
                return;
            }
            
            if(TransStatus == "NewTransaction")
            {
                MessageLabel.Text = "No Transaction for Payment";
                System.Media.SystemSounds.Beep.Play();
                return;

            }
            if(transtemp.AmountForPay == 0)
            {
                MessageLabel.Text = "Amount for Pay is Zero.";
                System.Media.SystemSounds.Beep.Play();
                return;


            }

            if (paymentmethod == "Post")
            {
                if( Math.Round( transtemp.BalanceAmount,0) == 0)
                {
                    if (transtemp.Post() == 1)
                    {
                        lasttransaction = transtemp;
                        lasttransactionreceiptno = transtemp.TransactionNo;
                        PrintFunction("Last");
                        transtemp = null;
                        transdetailstemplist = null;
                        POSGridView.DataSource = transdetailstemplist;
                       
                        TransStatus = "NewTransaction";
                        TransSubStatus = "";

                    }
                    else
                    {
                        MessageLabel.Text = "Payment Processing Error";
                        System.Media.SystemSounds.Exclamation.Play();
                    }
                    return;
                }
                else
                {
                    MessageLabel.Text = "Balance is not Zero, Cannot Complete.";
                    System.Media.SystemSounds.Exclamation.Play();
                }
                return;
            }
            if(paymentmethod == "VoidPayment")
            {
                transtemp.VoidPayments();
                UpdateTotalArea();
                return;
            }
            if (SearchBox.Text == "" || Convert.ToSingle(SearchBox.Text) == 0)
            {
                //MessageLabel.Text = "Enter Valid amount";
                using (var numericinput = new POS_Numeric_Input())
                {
                    numericinput.numericvalue = transtemp.BalanceAmount.ToString();
                    var result = numericinput.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        SearchBox.Text = numericinput.numericvalue;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            
                List<POS_PaymentType> paymenttypes = POS_Repositery.GetAllPaymentTypes();
                int paymentcount = paymenttypes.Count();
                POS_PaymentType paymentType = paymenttypes.Where(p => p.PaymentType == paymentmethod).FirstOrDefault();
                if (paymentType.PaymentType == "Account" && transtemp.CustomerAccount == null)
                {
                    QuickFunction("CustomerInfo");
                    return;
                }
                if (!paymentType.OverTenderAllowed && transtemp.BalanceAmount < Convert.ToSingle(SearchBox.Text))
                {
                    MessageBox.Show("Pay Exact, Balance Cashout not allowed for this Payment.");
                    return;
                }
                TransactionPayment payment = new TransactionPayment(transtemp, paymentType.PaymentType, paymentType.CurrencyCode, Convert.ToSingle(SearchBox.Text));
                payments.Add(payment);
                if (transtemp.TransactionPayments == null) transtemp.TransactionPayments = new List<TransactionPayment>();
                transtemp.TransactionPayments.Add(payment);
                transtemp.PaidAmount = TransactionPayment.PaidAmount(transtemp);
                transtemp.Update();
                UpdateTotalArea();
                SearchBox.Text = "";
                this.Refresh();
                if (transtemp.PaidAmount > transtemp.AmountForPay && TransStatus == "Sales" ) PayOut(transtemp.AmountForPay - transtemp.PaidAmount );
                if ( Math.Round( transtemp.PaidAmount - transtemp.AmountForPay) == 0) PaymentFunction("Post", "");
                return;
            

        }
        public void PayOut(Single payoutamount)
        {
            DialogResult result = MessageBox.Show(this, payoutamount.ToString("#,###.##"), "Payout", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                TransSubStatus = "Payment";
                SearchBox.Text = payoutamount.ToString();
                SendKeys.Send("{ENTER}");
            }

        }
        public void SalesReturn(string barcode)
        {
            TransactionDetailTEMP trandetailtemp = new TransactionDetailTEMP(transtemp, barcode);

            if (trandetailtemp.Updated)
            {
                transtemp.TransactionDetails.Add(trandetailtemp);

                MessageLabel.Text = trandetailtemp.ItemDescription + " @ " + trandetailtemp.PriceVAT.ToString() + " * " + trandetailtemp.Quantity.ToString();
                // transtemp.TransactionDetails.Add(trandetailtemp);
                transtemp.TotalQty = transtemp.TransactionDetails.Sum(q => q.Quantity);
                transtemp.AmountForPay = transtemp.TransactionDetails.Sum(a => a.AmountForPay);

                transdetailstemplist = new BindingList<TransactionDetailTEMP>(transtemp.TransactionDetails);
                UpdateTotalArea();

                this.POSGridView.DataSource = transdetailstemplist;
                this.POSGridView.Refresh();
                this.toolStripStatusLabel2.Text = transtemp.TransactionNo;
                this.Refresh();
            }
            else
            {
                MessageLabel.Text = "Error";
                System.Media.SystemSounds.Beep.Play();
            }

        }
        public void ChangeQty()
        {

            if (POSGridView.CurrentRow != null)
            {
                int line_no = (int)POSGridView.CurrentRow.Cells["lineno"].Value;
                if (Convert.ToBoolean(POSGridView.CurrentRow.Cells["void"].Value))
                {
                    MessageLabel.Text = "Suspended Line quantity cannot be changed";
                    System.Media.SystemSounds.Beep.Play();
                    return;
                }
                //MessageBox.Show(POSGridView.CurrentRow.Cells["lineno"].Value.ToString());
                TransactionDetailTEMP trandetailtemp = transtemp.TransactionDetails.Where(d => d.TransactionNo == transtemp.TransactionNo && d.Branch == transtemp.Branch && d.LineNo == line_no).FirstOrDefault();
                if (SearchBox.Text != "")
                {
                    if (TransStatus == "Sales")
                    trandetailtemp.ChangeQuantity(Math.Abs( Convert.ToInt16(SearchBox.Text)));
                    else
                    trandetailtemp.ChangeQuantity(-Math.Abs(Convert.ToInt16(SearchBox.Text)));
                    trandetailtemp.Update();
                    SearchBox.Text = "";
                    SearchBox.Focus();

                }
                else return;

                transtemp.TotalQty = transtemp.TransactionDetails.Where(q => !q.VoidStatus).Sum(q => q.Quantity);
                transtemp.AmountForPay = transtemp.TransactionDetails.Where(q => !q.VoidStatus).Sum(a => a.AmountForPay);
                transtemp.PaidAmount = TransactionPayment.PaidAmount(transtemp);
                UpdateTotalArea();

                

            }

            POSGridView.Refresh();



        }




        public void CreatePOSGrid()
        {
            POSGridView.AutoGenerateColumns = false;
            POSGridView.DataSource = transdetailstemplist;
            POSGridView.Font = new Font("Segoe UI Light", 12);
            POSGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            POSGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Light", 13, FontStyle.Bold);
            POSGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            POSGridView.RowsDefaultCellStyle.BackColor = Color.Azure;
            POSGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            POSGridView.AllowUserToResizeColumns = true;
            
            POSGridView.Columns.Clear();
            POSGridView.Columns.Add("barcode", "Barcode");
            POSGridView.Columns.Add("itemdesc", "Item");
            POSGridView.Columns.Add("price", "Price");
            POSGridView.Columns.Add("quantity", "Quantity");
            POSGridView.Columns.Add("amount", "Amount");
            POSGridView.Columns.Add("lineno", "LineNo");
            POSGridView.Columns.Add("void", "Voided");
            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
            cellstyle.Format = "N2";
            cellstyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            POSGridView.Columns[0].DataPropertyName = "Barcode";
            POSGridView.Columns[1].DataPropertyName = "ItemDescription";
            POSGridView.Columns[1].Width = 150;
            POSGridView.Columns[2].DataPropertyName = "PriceVAT";
            POSGridView.Columns[2].DefaultCellStyle = cellstyle;
            POSGridView.Columns[3].DataPropertyName = "Quantity";
            POSGridView.Columns[4].DataPropertyName = "AmountForPay";
            POSGridView.Columns[4].DefaultCellStyle = cellstyle;
            POSGridView.Columns[5].DataPropertyName = "LineNo";
            POSGridView.Columns[5].Visible = false;
            POSGridView.Columns[6].DataPropertyName = "VoidStatus";
            POSGridView.Columns[6].Visible = false;


            foreach (DataGridViewRow row in POSGridView.Rows)
            {
                if (row.Cells["Void"].Value.ToString() == "True")
                {
                    row.DefaultCellStyle.Font = new Font(POSGridView.Font, FontStyle.Strikeout);
                }
                else
                {
                    row.DefaultCellStyle.Font = new Font(POSGridView.Font, FontStyle.Regular);
                }
            }
        }

        
        public void UpdateTotalArea()
        {
            if (transtemp == null)
            {
                this.totalquantity = 0;
                this.TotalAmounttoPay = 0;
                this.paidamount = 0;
                this.balanceamount = 0;

            }
            else
            {
                this.totalquantity = transtemp.TotalQty;
                this.TotalAmounttoPay = transtemp.AmountForPay;
                this.paidamount = transtemp.PaidAmount;
                this.balanceamount = transtemp.BalanceAmount;

            }
            this.TotalQuantity_TextBox.Text = this.totalquantity.ToString("#,###.##");
            this.AmountforPay_TextBox.Text = this.TotalAmounttoPay.ToString("#,###.##");
            this.PaidAmountTextBox.Text = this.paidamount.ToString("#,###.##");
            this.BalanceAmount_TextBox.Text = this.balanceamount.ToString("#,###.##");
            this.Refresh();
        }

        private void POSGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            var sendgrid = (DataGridView)sender;

            if (sendgrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                selectedtransaction = POS_Repositery.GetATransaction(shift.Branch, sendgrid.CurrentRow.Cells[0].Value.ToString());
                PrintFunction("Selected");
              //  MessageBox.Show(sendgrid.CurrentRow.Cells[0].Value.ToString());
            }
        }

        public void PrintFunction(string whichTransaction)
        {
           
            // TransactionHeaderTEMP transheader = POS_Repositery.GetaTransaction(shift.ShiftNo, shift.Branch, shift.TillID, TransNo);
            PrintDocument pd = new PrintDocument();
            POS_Setup setup = new POS_Setup();
            pd.PrinterSettings.PrinterName = setup.POSPrinter;
            
            pd.DefaultPageSettings.PaperSize = new PaperSize("PosReceipt", Convert.ToInt16(setup.Pagewidth), Convert.ToInt16(setup.PageHeight));
           // pd.DefaultPageSettings.PaperSize.Height = Convert.ToInt16(setup.PageHeight);
           if (whichTransaction == "Last")
            {
                printabletransaction = POS_Repositery.GetATransaction(shift.Branch, lasttransactionreceiptno);
                pd.PrintPage += new PrintPageEventHandler(PrintLastReceipt);
                pd.Print();
                return;
            }
           if(whichTransaction == "Selected")
            {
                pd.PrintPage += new PrintPageEventHandler(PrintSelectedReceipt);
                pd.Print();

            }

           if(whichTransaction == "XReport")
            {
                pd.PrintPage += new PrintPageEventHandler(PrintXReport);
                pd.Print();

            }

        }

        public void PrintLastReceipt(object sender, PrintPageEventArgs ev)
        {
            //Get the Graphics object
            Graphics g = ev.Graphics;

            //Create a font Arial with size 16
            Font fontheading = new Font("Arial", 16, FontStyle.Bold);
            Font fontregular = new Font("Calibri", 12);
            Font fontcontent = new Font("Calibri", 10);
            Font Barcodefont = new Font("Free 3 of 9", 32);
            //Create a solid brush with black color
            SolidBrush solidbrush = new SolidBrush(Color.Black);
            SolidBrush lightdbrush = new SolidBrush(Color.Gray);
            Pen pen = new Pen(lightdbrush);
            StringFormat stringCenter = new StringFormat();
            stringCenter.Alignment = StringAlignment.Center;
            StringFormat stringboldcenter = new StringFormat();
            
            StringFormat  stringLeft = new StringFormat();
            stringLeft.Alignment = StringAlignment.Near;
            StringFormat stringRight = new StringFormat();
            stringRight.Alignment = StringAlignment.Far;
            POS_Setup setup = new POS_Setup();
            int line = 30;
            g.DrawString(setup.ReceiptHeader1, fontheading, solidbrush, new Rectangle(20, 30, 480, 100), stringCenter);
            g.DrawString(setup.ReceiptHeader2, fontregular, solidbrush, new Rectangle(20, 60, 480, 100), stringCenter);
            g.DrawString(setup.ReceiptHeader3, fontregular, solidbrush, new Rectangle(20, 85, 480, 100), stringCenter);
            
            g.DrawString("TAX Invoice", fontheading, solidbrush, new Rectangle(20, 125, 480, 40), stringCenter);
            g.DrawString("TRN# " + setup.TRNNumber, fontheading, solidbrush, new Rectangle(20, 165, 480, 40), stringCenter);


            g.DrawString("Transaction No :" + printabletransaction.TransactionNo  , fontregular, solidbrush, new Rectangle(20, 250, 250, 80), stringLeft);
            g.DrawString("Cashier        :" + printabletransaction.CashierID, fontregular, solidbrush, new Rectangle(20, 275, 250, 80), stringLeft);
            g.DrawString("Date      :" +printabletransaction.TransactionDate.ToShortDateString(), fontregular, solidbrush, new Rectangle(250, 250, 230, 80), stringRight);
            g.DrawString("Time      :" +printabletransaction.TransactionTime.ToShortTimeString(), fontregular, solidbrush, new Rectangle(250, 275, 230, 80), stringRight);
            line = 315;
            if (printabletransaction.IsCustomerAccount)
            {
                g.DrawString("Customer Account",  fontregular, solidbrush, new Rectangle(20, line, 250, 80), stringLeft);
                line += 25;
                g.DrawString(printabletransaction.CustomerAccount + " " + printabletransaction.CustomerName, fontregular, solidbrush, new Rectangle(20, line, 250, 80), stringLeft);
                line += 25;
                g.DrawString(printabletransaction.CustomerAddress1, fontregular, solidbrush, new Rectangle(20, line, 250, 80), stringLeft);
                line += 25;
                g.DrawString(printabletransaction.CustomerAddress2, fontregular, solidbrush, new Rectangle(20, line, 250, 80), stringLeft);
                line += 25;
                g.DrawString(printabletransaction.CustomerMobileNo, fontregular, solidbrush, new Rectangle(20, line, 250, 80), stringLeft);



            }
            line += 25;
            g.DrawString("Item Description", fontregular, solidbrush, new Rectangle(20, line, 260, 80), stringLeft);
            g.DrawString("Price", fontregular, solidbrush, new Rectangle(280, line, 70, 80), stringLeft);
            g.DrawString("Qty", fontregular, solidbrush, new Rectangle(350, line, 50, 80), stringLeft);
            g.DrawString("Amount", fontregular, solidbrush, new Rectangle(400, line, 80, 80), stringLeft);
            line += 25;
            g.DrawLine(pen, new Point(20, line), new Point(480, line));
            line += 25;
            foreach(TransactionDetailTEMP tdetail in printabletransaction.TransactionDetails)
            {
                g.DrawString(tdetail.ItemDescription, fontcontent, solidbrush, new Rectangle(20, line, 260, 25), stringLeft);
                g.DrawString(tdetail.PriceVAT.ToString("#,###.00"), fontcontent, solidbrush, new Rectangle(280, line, 70, 25), stringRight);
                g.DrawString(tdetail.UOMQty.ToString(), fontcontent, solidbrush, new Rectangle(350, line, 50, 25), stringRight);
                g.DrawString(tdetail.AmountForPay.ToString("#,###.00"), fontcontent, solidbrush, new Rectangle(400, line, 80, 25), stringRight);
                if(tdetail.DiscountAmount != 0)
                {
                    line += 20;
                    g.DrawString("Discount  " + tdetail.DiscountAmount.ToString("#,###.00"), fontcontent, solidbrush, new Rectangle(300, line, 180, 80), stringRight);
                    
                }
                line += 30;

            }
            

            g.DrawLine(pen, new Point(20, line), new Point(480, line));
            line += 20;
            g.DrawString("Total with Tax", fontregular, solidbrush, new Rectangle(200, line, 120, 80), stringLeft);
            
            g.DrawString(printabletransaction.AmountForPay.ToString("#,###.00"), fontregular, solidbrush, new Rectangle(340, line, 140, 80), stringRight);
            line += 20;
            g.DrawString("Total Quantity", fontregular, solidbrush, new Rectangle(200, line, 120, 80), stringLeft);
            
            g.DrawString(printabletransaction.TotalQty.ToString("#,###"), fontregular, solidbrush, new Rectangle(340, line, 140, 80), stringRight);
            line += 30;

            g.DrawLine(pen, new Point(20, line), new Point(480, line));
            line += 20;
            g.DrawString(" Invoice Summary ", fontregular, solidbrush, new Rectangle(20, line, 380, 25), stringCenter);
            line += 30;
            g.DrawString("Total Amount without VAT", fontregular, solidbrush, new Rectangle(150, line, 250, 80), stringLeft);
            g.DrawString(printabletransaction.NetAmount.ToString("#,###.00"), fontregular, solidbrush, new Rectangle(400, line, 80, 80), stringRight);
            
            line += 20;
            g.DrawString("Discount", fontregular, solidbrush, new Rectangle(150, line, 150, 80), stringLeft);
            g.DrawString(printabletransaction.DiscountAmount.ToString("#,###.00"), fontregular, solidbrush, new Rectangle(400, line, 80, 80), stringRight);

    
            line += 20;
            g.DrawString("VAT 5%", fontregular, solidbrush, new Rectangle(150, line, 150, 80), stringLeft);
            g.DrawString(printabletransaction.VATAmount.ToString("#,###.00"), fontregular, solidbrush, new Rectangle(400, line, 80, 80), stringRight);
            line += 20;
            foreach(TransactionPayment payment in printabletransaction.TransactionPayments)
            {
                if (payment.Amount > 0)
                {
                    if (payment.PaymentType == "FCY")
                    {
                        g.DrawString(payment.PaymentType + " " + payment.Currency + "@" + payment.ExchangeRate.ToString("#.000"), fontregular, solidbrush, new Rectangle(150, line, 150, 80), stringLeft);
                    }else
                    {
                        g.DrawString(payment.PaymentType , fontregular, solidbrush, new Rectangle(150, line, 150, 80), stringLeft);
                    }
                    
                }
                else
                {
                    g.DrawString("Change Due", fontregular, solidbrush, new Rectangle(150, line, 150, 80), stringLeft);
                }
                
                g.DrawString(payment.Amount.ToString("#,###.00"), fontregular, solidbrush, new Rectangle(400, line, 80, 80), stringRight);
                line += 20;

            }
            line += 20;
            g.DrawLine(pen, new Point(20, line), new Point(480, line));
            line += 5;
            g.DrawLine(pen, new Point(20, line), new Point(480, line));
           line += 20;

            g.DrawString("*" + selectedtransaction.TransactionNo + "*", Barcodefont, solidbrush, new Rectangle(20, line, 480, 100), stringCenter);
            line += 50;
            g.DrawString(setup.ReceiptFooter1, fontregular, solidbrush, new Rectangle(20, line, 480, 100), stringCenter);
            line += 25;
            g.DrawString(setup.ReceiptFooter2, fontregular, solidbrush, new Rectangle(20, line, 480, 100), stringCenter);
            line += 25;
            g.DrawString(setup.ReceiptFooter3, fontregular, solidbrush, new Rectangle(20, line, 480, 100), stringCenter);

        }
        
        public void PrintXReport(object sender, PrintPageEventArgs ev)
        {
            Graphics g = ev.Graphics;

            //Create a font Arial with size 16
            Font fontheading = new Font("Arial", 16);
            Font fontregular = new Font("Calibri", 12);
            Font fontcontent = new Font("Calibri", 10);
            //Create a solid brush with black color
            SolidBrush solidbrush = new SolidBrush(Color.Black);
            SolidBrush lightdbrush = new SolidBrush(Color.Gray);
            Pen pen = new Pen(lightdbrush);
            StringFormat stringCenter = new StringFormat();
            stringCenter.Alignment = StringAlignment.Center;
            StringFormat stringLeft = new StringFormat();
            stringLeft.Alignment = StringAlignment.Near;
            StringFormat stringRight = new StringFormat();
            stringRight.Alignment = StringAlignment.Far;
            POS_Setup setup = new POS_Setup();
            //Draw "Hello Printer!";
            // lineno = 30;
            g.DrawString(setup.ReceiptHeader1, fontheading, solidbrush, new Rectangle(20, 30, 480, 100), stringCenter);
            g.DrawString(setup.ReceiptHeader2, fontregular, solidbrush, new Rectangle(20, 60, 480, 100), stringCenter);
            g.DrawString(setup.ReceiptHeader3, fontregular, solidbrush, new Rectangle(20, 85, 480, 100), stringCenter);
            g.DrawString("X Report", fontheading, solidbrush, new Rectangle(20, 125, 480, 100), stringCenter);
            
            g.DrawString("Store No :" + shift.Branch, fontregular, solidbrush, new Rectangle(20, 200, 225, 80), stringLeft);
            g.DrawString("Cashier:        :" + shift.CashierID, fontregular, solidbrush, new Rectangle(20, 225, 250, 80), stringLeft);

            g.DrawString("Transaction Summary", fontheading, solidbrush, new Rectangle(20, 250, 480, 80), stringCenter);
            g.DrawLine(pen, new Point(20, 275), new Point(480, 275));
            int lineno = 325;
            Rectangle Column1 = new Rectangle(20, lineno, 250, 25);
            Rectangle Column2 = new Rectangle(270, lineno, 90, 25);
            Rectangle Column3 = new Rectangle(370, lineno, 110, 25);
            
            g.DrawString("Total Number of Transactions  :" , fontregular, solidbrush, Column1, stringLeft);
            g.DrawString(POS_Repositery.GetAllTransactions(shift.ShiftNo).Count().ToString("#,###"), fontregular, solidbrush, Column3, stringRight);
            lineno += 25;
            Column1.Y = lineno;
            Column3.Y = lineno;
            g.DrawString("Amount Before VAT :" , fontregular, solidbrush, Column1, stringLeft);
            g.DrawString(POS_Repositery.GetAllTransactions(shift.ShiftNo).Sum(a => a.AmountForPay).ToString("#,###.00"), fontregular, solidbrush, Column3, stringRight);
            lineno += 25;
            Column1.Y = lineno;
            Column3.Y = lineno;

            g.DrawString("Total Quantity :" , fontregular, solidbrush, Column1, stringLeft);
            g.DrawString(POS_Repositery.GetAllTransactions(shift.ShiftNo).Sum(a => a.TotalQty).ToString("#,###.00"), fontregular, solidbrush, Column3, stringRight);
            lineno += 25;
            Column1.Y = lineno;
            Column3.Y = lineno;

            g.DrawString("Discount :" , fontregular, solidbrush, Column1, stringLeft);
            g.DrawString(POS_Repositery.GetAllTransactions(shift.ShiftNo).Sum(a => a.DiscountAmount).ToString("#,###.00"), fontregular, solidbrush, Column3, stringRight);
            lineno += 25;
            Column1.Y = lineno;
            Column3.Y = lineno;

            g.DrawString("Amount without VAT:"  , fontregular, solidbrush, Column1, stringLeft);
            g.DrawString(POS_Repositery.GetAllTransactions(shift.ShiftNo).Sum(a => a.NetAmount).ToString("#,###.00"), fontregular, solidbrush, Column3, stringRight);
            lineno += 25;
            Column1.Y = lineno;
            Column3.Y = lineno;

            g.DrawString("VAT Amount :"  , fontregular, solidbrush, Column1, stringLeft);
            g.DrawString(POS_Repositery.GetAllTransactions(shift.ShiftNo).Sum(a => a.VATAmount).ToString("#,###.00"), fontregular, solidbrush, Column3, stringRight);

            lineno += 45;
            Column1.Y = lineno;
            Column3.Y = lineno;

            g.DrawString("Transactions for Delivery :" , fontregular, solidbrush, Column1, stringLeft);
            g.DrawString(POS_Repositery.PayonDeliveryTransactions().Count().ToString("#,###"), fontregular, solidbrush, Column3, stringRight);
            lineno += 25;
            Column1.Y = lineno;
            Column3.Y = lineno;

            g.DrawString("Amount :" , fontregular, solidbrush, Column1, stringLeft);
            g.DrawString(POS_Repositery.PayonDeliveryTransactions().Sum(a => a.AmountForPay).ToString("#,###.00"), fontregular, solidbrush, Column3, stringRight);

            lineno += 25;
            Column1.Y = lineno;
            Column3.Y = lineno;

            g.DrawString("Transactions Suspended :" , fontregular, solidbrush, Column1, stringLeft);
            g.DrawString(POS_Repositery.SuspendedTransactions().Count().ToString("#,###"), fontregular, solidbrush, Column3, stringRight);
            lineno += 25;
            Column1.Y = lineno;
            Column3.Y = lineno;

            g.DrawString("Amount :"  , fontregular, solidbrush, Column1, stringLeft);
            g.DrawString(POS_Repositery.SuspendedTransactions().Sum(a => a.AmountForPay).ToString("#,###.00"), fontregular, solidbrush, Column3, stringRight);
            lineno += 25;
            Column1.Y = lineno;
            Column3.Y = lineno;

            g.DrawString("Collection Summary", fontheading, solidbrush, new Rectangle(20, lineno, 480, 25), stringCenter);

            lineno += 25;
            g.DrawLine(pen, new Point(20, lineno), new Point(480, lineno));
            lineno += 25;
            
            var paym = POS_Repositery.PaymentLines(shift.ShiftNo, shift.Branch, shift.TillID).GroupBy(a => new { a.PaymentType, a.Currency, a.CurrencyRate }).Select(b => new { PaymentType = b.Key.PaymentType, Currency = b.Key.Currency, CurrencyRate=b.Key.CurrencyRate, AmountInCurrency = b.Sum(c => c.AmountinCurrency), Amount = b.Sum(c => c.Amount) });
            foreach (var p in paym.Where(a => a.PaymentType != "Account"))
            {
                Column1.Y = lineno;
                Column3.Y = lineno;

                if (p.PaymentType == "FCY")
                    g.DrawString(p.PaymentType + " " + p.Currency + " " + p.AmountInCurrency.ToString("#,###.00") + " @" + p.CurrencyRate, fontregular, solidbrush, Column1, stringLeft);
            else
                    g.DrawString(p.PaymentType , fontregular, solidbrush, Column1, stringLeft);
                g.DrawString( p.Amount.ToString("#,###.00"), fontregular, solidbrush, Column3, stringRight);
                lineno += 25;

            }
            ((IDisposable)paym).Dispose();
            var payment = POS_Repositery.PaymentLines(shift.ShiftNo, shift.Branch, shift.TillID).Where(a => a.PaymentType == "Account");
            if (payment.Count() > 0)
            {
                Column1.Y = lineno;
                Column3.Y = lineno;
                g.DrawString("On Customer Account", fontregular, solidbrush, Column1, stringLeft);
                lineno += 25;
            }
            foreach (var p in payment)
            {
                Column1.Y = lineno;
                Column3.Y = lineno;
                var custname = POS_Repositery.GetATransaction(shift.Branch, p.TransactionNo);
                g.DrawString(custname.CustomerAccount + " "+custname.CustomerName, fontregular, solidbrush, Column1, stringLeft);
                g.DrawString(p.Amount.ToString("#,###.00"), fontregular, solidbrush, Column3, stringRight);
                lineno += 25;
            }
            ((IDisposable)payment).Dispose();
        }

        public void PrintSelectedReceipt(object sender, PrintPageEventArgs ev)
        {
            //Get the Graphics object
            Graphics g = ev.Graphics;

            //Create a font Arial with size 16
            Font fontheading = new Font("Arial", 16, FontStyle.Bold);
            Font fontregular = new Font("Calibri", 12);
            Font fontcontent = new Font("Calibri", 10);
            Font Barcodefont = new Font("Free 3 of 9", 32);
            //Create a solid brush with black color
            SolidBrush solidbrush = new SolidBrush(Color.Black);
            SolidBrush lightdbrush = new SolidBrush(Color.Gray);
            Pen pen = new Pen(lightdbrush);
            StringFormat stringCenter = new StringFormat();
            stringCenter.Alignment = StringAlignment.Center;
            StringFormat stringboldcenter = new StringFormat();

            StringFormat stringLeft = new StringFormat();
            stringLeft.Alignment = StringAlignment.Near;
            StringFormat stringRight = new StringFormat();
            stringRight.Alignment = StringAlignment.Far;
            POS_Setup setup = new POS_Setup();
            //Draw "Hello Printer!";
            int line = 30;
            g.DrawString(setup.ReceiptHeader1, fontheading, solidbrush, new Rectangle(20, line, 480, 100), stringCenter);
            line += 30;
            g.DrawString(setup.ReceiptHeader2, fontregular, solidbrush, new Rectangle(20, line, 480, 100), stringCenter);
            line += 30;
            g.DrawString(setup.ReceiptHeader3, fontregular, solidbrush, new Rectangle(20, line, 480, 100), stringCenter);
            line += 40;
            g.DrawString("TAX Invoice", fontheading, solidbrush, new Rectangle(20, line, 480, 40), stringCenter);
            line += 40;
            g.DrawString("TRN# " + setup.TRNNumber, fontheading, solidbrush, new Rectangle(20, line, 480, 40), stringCenter);
            line += 80;

            g.DrawString("Transaction No :" + selectedtransaction.TransactionNo, fontregular, solidbrush, new Rectangle(20, line, 250, 80), stringLeft);
            g.DrawString("Date      :" + selectedtransaction.TransactionDate.ToShortDateString(), fontregular, solidbrush, new Rectangle(250, line, 230, 80), stringRight);
            line += 25;
            g.DrawString("Cashier        :" + selectedtransaction.CashierID, fontregular, solidbrush, new Rectangle(20, line, 250, 80), stringLeft);
            
            g.DrawString("Time      :" + selectedtransaction.TransactionTime.ToShortTimeString(), fontregular, solidbrush, new Rectangle(250, line, 230, 80), stringRight);
            line += 40;
            if (selectedtransaction.IsCustomerAccount)
            {
                g.DrawString("Customer Account", fontregular, solidbrush, new Rectangle(20, line, 260, 80), stringLeft);
                line += 25;
                g.DrawString(selectedtransaction.CustomerAccount + " "+selectedtransaction.CustomerName, fontregular, solidbrush, new Rectangle(20, line, 260, 80), stringLeft);
                line += 25;
                g.DrawString(selectedtransaction.CustomerAddress1 + ", " + selectedtransaction.CustomerAddress2, fontregular, solidbrush, new Rectangle(20, line, 260, 80), stringLeft);
                line += 40;
            }

            g.DrawString("Item Description", fontregular, solidbrush, new Rectangle(20, line, 260, 80), stringLeft);
            g.DrawString("Price", fontregular, solidbrush, new Rectangle(280, line, 70, 80), stringLeft);
            g.DrawString("Qty", fontregular, solidbrush, new Rectangle(350, line, 50, 80), stringLeft);
            g.DrawString("Amount", fontregular, solidbrush, new Rectangle(400, line, 80, 80), stringLeft);
            line += 25;
            g.DrawLine(pen, new Point(20, line), new Point(480, line));
            line += 25;
            

            foreach (TransactionDetailTEMP tdetail in selectedtransaction.TransactionDetails)
            {
                g.DrawString(tdetail.ItemDescription, fontcontent, solidbrush, new Rectangle(20, line, 260, 25), stringLeft);
                g.DrawString(tdetail.PriceVAT.ToString("#,###.00"), fontcontent, solidbrush, new Rectangle(280, line, 70, 25), stringRight);
                g.DrawString(tdetail.UOMQty.ToString(), fontcontent, solidbrush, new Rectangle(350, line, 50, 25), stringRight);
                g.DrawString(tdetail.AmountForPay.ToString("#,###.00"), fontcontent, solidbrush, new Rectangle(400, line, 80, 25), stringRight);
                if (tdetail.DiscountAmount != 0)
                {
                    line += 20;
                    g.DrawString("Discount  " + tdetail.DiscountAmount.ToString("#,###.00"), fontcontent, solidbrush, new Rectangle(300, line, 180, 80), stringRight);

                }
                line += 30;

            }


            g.DrawLine(pen, new Point(20, line), new Point(480, line));
            line += 20;
            g.DrawString("Total with Tax", fontregular, solidbrush, new Rectangle(200, line, 120, 80), stringLeft);

            g.DrawString(selectedtransaction.AmountForPay.ToString("#,###.00"), fontregular, solidbrush, new Rectangle(340, line, 140, 80), stringRight);
            line += 20;
            g.DrawString("Total Quantity", fontregular, solidbrush, new Rectangle(200, line, 120, 80), stringLeft);

            g.DrawString(selectedtransaction.TotalQty.ToString("#,###"), fontregular, solidbrush, new Rectangle(340, line, 140, 80), stringRight);
            line += 30;

            g.DrawLine(pen, new Point(20, line), new Point(480, line));
            line += 20;
            g.DrawString(" Invoice Summary ", fontregular, solidbrush, new Rectangle(20, line, 380, 25), stringCenter);
            line += 30;
            g.DrawString("Total Amount without VAT", fontregular, solidbrush, new Rectangle(150, line, 250, 80), stringLeft);
            g.DrawString(selectedtransaction.NetAmount.ToString("#,###.00"), fontregular, solidbrush, new Rectangle(400, line, 80, 80), stringRight);

            line += 20;
            g.DrawString("Discount", fontregular, solidbrush, new Rectangle(150, line, 150, 80), stringLeft);
            g.DrawString(selectedtransaction.DiscountAmount.ToString("#,###.00"), fontregular, solidbrush, new Rectangle(400, line, 80, 80), stringRight);


            line += 20;
            g.DrawString("VAT 5%", fontregular, solidbrush, new Rectangle(150, line, 150, 80), stringLeft);
            g.DrawString(selectedtransaction.VATAmount.ToString("#,###.00"), fontregular, solidbrush, new Rectangle(400, line, 80, 80), stringRight);
            line += 20;
            foreach (TransactionPayment payment in selectedtransaction.TransactionPayments.OrderBy(a => a.LineNo))
            {
                if (payment.Amount > 0)
                {
                    if (payment.PaymentType == "FCY")
                    {
                        g.DrawString(payment.PaymentType + " " + payment.Currency + "@" + payment.ExchangeRate.ToString("#.000"), fontregular, solidbrush, new Rectangle(150, line, 150, 80), stringLeft);
                    }
                    else
                    {
                        g.DrawString(payment.PaymentType, fontregular, solidbrush, new Rectangle(150, line, 150, 80), stringLeft);
                    }

                }
                else
                {
                    g.DrawString("Change Due", fontregular, solidbrush, new Rectangle(150, line, 150, 80), stringLeft);
                }

                g.DrawString(payment.Amount.ToString("#,###.00"), fontregular, solidbrush, new Rectangle(400, line, 80, 80), stringRight);
                line += 20;

            }
            line += 20;
            g.DrawLine(pen, new Point(20, line), new Point(480, line));
            line += 5;

            g.DrawLine(pen, new Point(20, line), new Point(480, line));
            line += 20;

            g.DrawString(setup.ReceiptFooter1, fontregular, solidbrush, new Rectangle(20, line, 480, 100), stringCenter);
            line += 25;
            g.DrawString(setup.ReceiptFooter2, fontregular, solidbrush, new Rectangle(20, line, 480, 100), stringCenter);
            line += 25;
            g.DrawString(setup.ReceiptFooter3, fontregular, solidbrush, new Rectangle(20, line, 480, 100), stringCenter);

            line += 40;
            g.DrawLine(pen, new Point(20, line), new Point(480, line));
            line += 20;

            
            g.DrawString("*" + selectedtransaction.TransactionNo + "*", Barcodefont, solidbrush, new Rectangle(20, line, 480, 100), stringCenter);
            line += 50;

        }




    }
}
