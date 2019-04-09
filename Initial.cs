using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton;
using System.Diagnostics;
using MakarovDev.ExpandCollapsePanel;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Sys_Sols_Inventory
{
    
    public partial class Initial : Form
    {
        private bool POSTerminal = true;

        //[DllImport("user32.dll")]

        //[return: MarshalAs(UnmanagedType.Bool)]

        //  static extern bool SetForegroundWindow(IntPtr hWnd);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        public static string originalDate;
        public static string duplicateDate;
        public static bool flag = false;
        public static bool dbSet = false;
        Class.DatabaseSettings dsSettings = new Class.DatabaseSettings();
        Class.Transactions trans=new Class.Transactions();
        Class.CompanySetup Comp = new Class.CompanySetup();
        Class.EmpLog emplog = new Class.EmpLog();
        string s;
        int Flag = 0;
        string Empid;
        public string th;

        public Initial()
        {
            InitializeComponent();
        }

        public void GetCurrentDate()
        {
            int datecout;
            datecout = Comp.GetCurrentDate();
            if (datecout == 0)
            {
                Accounts.CurrentDate CD = new Accounts.CurrentDate();
                CD.ShowDialog();
            }
        }
           //public Initial(string id)
           //{
           //    Empid = id; 
           //    InitializeComponent();
           //}


        Login log =(Login)Application.OpenForms["Login"];
        Class.Activation Act = new Class.Activation();

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
           
            {
                if (keyData == (Keys.F2))
                {
                    AutoHidMenu();
                    return true;
                }

               
                if(keyData==(Keys.LWin))
                {
                  
                //   SetForegroundWindow(this.Handle);
                }
               
                if (keyData == (Keys.Alt | Keys.L))
                {
                    //ActivationCheck();
                    //emplog.Id = log.UpdateLog;
                    //emplog.UpdateEmplog();
                    //this.Hide();
                    //log.Show();
                    Login LG = new Login(1);
                    LG.ShowDialog();
 
                }
                if (keyData == (Keys.Alt | Keys.Control | Keys.S))
                {
                    string currentConn = Properties.Settings.Default.ConnectionStrings;

                    if (dsSettings.ConnOriginal() == currentConn && dsSettings.ConnDuplicate() != string.Empty && btnSwitch.BackColor == Color.MediumVioletRed)
                    {
                        dbSet = true;
                        txt_Multiaccount.Visible = true;
                        
                        this.ActiveControl = txt_Multiaccount;
                       
                    }
                    if (dsSettings.ConnDuplicate() == currentConn && dsSettings.ConnOriginal() != string.Empty && btnSwitch.BackColor == Color.MidnightBlue)
                    {
                        Properties.Settings.Default.ConnectionStrings = dsSettings.ConnOriginal();
                        Properties.Settings.Default.Save();
                        btnSwitch.BackColor = System.Drawing.Color.MediumVioletRed;
                        pictureBoxclosetab_Click(this.piccloseall,null);
                        picsales.Focus();
                    }
                    if (dsSettings.ConnDuplicate() == string.Empty && btnSwitch.BackColor == Color.MediumVioletRed)
                    {
                        dbSet = false;
                        txt_Multiaccount.Visible = true;
                        this.ActiveControl = txt_Multiaccount;

                    }
            
                }
                if (keyData == (Keys.F3))
                {

                    foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in maindocpanel.Pages)
                    {
                        if (kr.Name == "Home")
                        {
                            maindocpanel.SelectedPage = kr;
                            Flag = 1;
                            break;

                        }
                    }
                    if (Flag == 0)
                    {
                        ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                        maindocpanel.Pages.Add(kp);
                        DashBoard cn = new DashBoard();
                        cn.Show();
                        cn.TopLevel = false;
                        //  splitContainer1.Panel2.Controls.Add(ad);
                        kp.Controls.Add(cn);
                        cn.Dock = DockStyle.Fill;
                        kp.Text = "Home";
                        kp.Name = "Home";
                        maindocpanel.SelectedPage = kp;
                        


                    }
                    Flag = 0;
                    return true;
                }


                if (keyData == (Keys.Alt | Keys.C))
                {
                    AppLicense.Scripts sc = new AppLicense.Scripts();
                    sc.ShowDialog();
                    return true;
                }

                if (keyData == (Keys.Alt | Keys.Y))
                {
                    Accounts.YearEnd ye = new Accounts.YearEnd();
                    ye.Show();
                    return true;
                }

                if(keyData==(Keys.F8))
                {
                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                    maindocpanel.Pages.Add(kp);
                    SalesQ sls = new SalesQ("SAL.CSS", "Cash");
                    sls.Show();
                    sls.BackColor = Color.White;
                    sls.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);s
                    kp.Controls.Add(sls);
                    sls.Dock = DockStyle.Fill;
                    kp.Text = sls.Text;
                    kp.Name = "Sales Cash";
                    sls.FormBorderStyle = FormBorderStyle.None;
                    sls.BackColor = Color.White;
                    //kp.Focus();
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                if (keyData == (Keys.F9))
                {
                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                    maindocpanel.Pages.Add(kp);
                    PurchaseMaster m = new PurchaseMaster("PUR.CSS", "Cash");
                    m.Show();
                    m.BackColor = Color.White;
                    m.TopLevel = false;
                    kp.Controls.Add(m);
                    m.Dock = DockStyle.Fill;
                    kp.Text = m.Text;
                    kp.Name = "Purchase";
                    m.FormBorderStyle = FormBorderStyle.None;
                    //kp.Focus();
                    maindocpanel.SelectedPage = kp;
                    // m.Focus();
                    AutoHidMenu();
                }
                if(keyData==(Keys.F5))
                {
                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                    maindocpanel.Pages.Add(kp);
                    PaymentVoucher2 p = new PaymentVoucher2(0);
                    p.Show();
                    p.BackColor = Color.White;
                    p.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(p);
                    p.Dock = DockStyle.Fill;
                    kp.Text = p.Text;
                    kp.Name = "Payment Voucher";
                    p.FormBorderStyle = FormBorderStyle.None;
                    p.BackColor = Color.White;
                    //  kp.Focus();
                    maindocpanel.SelectedPage = kp;
                }
                if (keyData == (Keys.F6))
                {
                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                    maindocpanel.Pages.Add(kp);
                    PaymentVoucher2 p = new PaymentVoucher2(1);
                    p.Show();
                    p.BackColor = Color.White;
                    p.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(p);
                    p.Dock = DockStyle.Fill;
                    kp.Text = p.Text;
                    kp.Name = "Reciept Voucher";
                    p.FormBorderStyle = FormBorderStyle.None;
                    p.BackColor = Color.White;
                    // kp.Focus();
                    maindocpanel.SelectedPage = kp;
                }
                if (keyData == (Keys.Alt | Keys.A))
                {
                    AppLicense.Validate VL = new AppLicense.Validate();
                    VL.ShowDialog();
                  //  return true;
                }
                if(keyData==(Keys.F7))
                {
                    //ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                    //maindocpanel.Pages.Add(kp);
                    //SaleQt sqt = new SaleQt("SAL.CSS", "Cash");
                    //sqt.Show();
                    //sqt.BackColor = Color.White;
                    //sqt.TopLevel = false;
                    //kp.Controls.Add(sqt);
                    //sqt.Dock = DockStyle.Fill;
                    //kp.Text = sqt.Text;
                    //kp.Name = "Sales Quote";
                    //sqt.FormBorderStyle = FormBorderStyle.None;
                    //maindocpanel.SelectedPage = kp;
                }
                if (keyData == (Keys.F1))
                {
                   
                    foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
                    {
                        if (kr.Name == "Ledger Report")
                        {
                            this.maindocpanel.SelectedPage = kr;
                            Flag = 1;
                            this.onlyhide();
                            break;

                        }
                    }
                    if (Flag == 0)
                    {
                        Accounts.LedgerReport cn = new Accounts.LedgerReport();
                        ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                        this.maindocpanel.Pages.Add(kp);


                        cn.TopLevel = false;
                        //  splitContainer1.Panel2.Controls.Add(ad);
                        kp.Controls.Add(cn);
                        cn.Dock = DockStyle.Fill;
                        kp.Text = cn.Text;
                        kp.Name = "Ledger Report";
                        // kp.Focus();
                        cn.FormBorderStyle = FormBorderStyle.None;

                        this.maindocpanel.SelectedPage = kp;
                        this.onlyhide();
                        cn.Show();
                    }
                    Flag = 0;
                }
                 

                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        //public static void Disable()
        //{
        //    RegistryKey key = null;
        //    try
        //    {
        //        key = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Keyboard Layout", true);
        //        byte[] binary = new byte[] { 
        //            0x00, 
        //            0x00, 
        //            0x00, 
        //            0x00, 
        //            0x00, 
        //            0x00, 
        //            0x00, 
        //            0x00, 
        //            0x03, 
        //            0x00, 
        //            0x00, 
        //            0x00, 
        //            0x00, 
        //            0x00, 
        //            0x5B, 
        //            0xE0, 
        //            0x00, 
        //            0x00, 
        //            0x5C, 
        //            0xE0, 
        //            0x00, 
        //            0x00, 
        //            0x00, 
        //            0x00 
        //        };
        //        key.SetValue("Scancode Map", binary, RegistryValueKind.Binary);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.Assert(false, ex.ToString());
        //    }
        //    finally
        //    {
        //      //  key.Close();
        //    }
        //}
        
        int j=0;
      
        private void Initial_Load(object sender, EventArgs e)
        {
            GetCurrentDate();
            originalDate = GetDate();
            th = log.Theme;
            timer1.Start();
            timer1.Interval = 5 * 3600000;
            //timer1.Interval = 3 * 10000;
            DateTime now = DateTime.Now;
            s = now.DayOfWeek.ToString();
            btnSwitch.TabStop = false;
            POSTerminal = General.IsEnabled(Settings.POSTerminal);
            if (POSTerminal)
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                panel2.Visible = true;
            }

          //  splitContainer1.Panel1Collapsed = true;
            foreach (ToolStripMenuItem tsmi in menuMain.Items)
            {
                int i = 40;
                if(tsmi.Enabled==true)
                {
                    ExpandCollapsePanel exp = new ExpandCollapsePanel();
               
                    advancedFlowLayoutPanel1.Controls.Add(exp);
                    exp.Text = tsmi.Text;
                    exp.ForeColor = Color.White;
                    exp.IsExpanded = false;
               
                    exp.Name = "exp" + j;
                    j = j + 1;
                    exp.ExpandCollapse += exp_ExpandCollapse;
               
                
                    //getting size of collapse panel according to menuses count
                    foreach (object item in tsmi.DropDownItems)
                    {
                        if (item.ToString() == "Supplier Payment" || item.ToString() == "Customer Receipt" || item.ToString() == "Database Connection" || item.ToString() == "Receipt and Payment" || item.ToString() == "Project Report" || item.ToString() == "State" || item.ToString() == "Cashier Report" || item.ToString() == "Cheque Reports" || item.ToString() == "Customer Payment History" || item.ToString() == "Supplier Payment History")
                        {
                            ((ToolStripDropDownItem)item).Enabled = true;
                        }
                        if (((ToolStripDropDownItem)item).Enabled == true)
                        {
                            if (item.GetType() == typeof(ToolStripSeparator))
                            {
                                continue;
                            }
                            i = i + 24;
                        }
                    }
                    exp.ExpandedHeight = i;
                               
                //  exp.Font = new Font(exp.Font, FontStyle.Bold);
               
                    MakarovDev.ExpandCollapsePanel.AdvancedFlowLayoutPanel adcp = new MakarovDev.ExpandCollapsePanel.AdvancedFlowLayoutPanel();
            
             //   adcp.BackColor = Color.;
                    
                    adcp.Padding = new System.Windows.Forms.Padding(40);

                      //  adcp.BackColor = ColorTranslator.FromHtml("#EDEDED");
                    exp.Controls.Add(adcp);
             
               
                //  adcp.AutoSize = true;
                    foreach (object item in tsmi.DropDownItems)
                    {                        
                        if (((ToolStripDropDownItem)item).Enabled == true)
                        {
                            if (item.GetType() == typeof(ToolStripSeparator))
                            {
                                continue;
                            }
                            //    ComponentFactory.Krypton.Toolkit.KryptonLabel lb = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
                            Label lb = new Label();
                            lb.Text = ((ToolStripDropDownItem)item).Text;
                            lb.ForeColor = Color.White;
                            //lb.Font = new Font(lb.Font,FontStyle.Bold);
                            lb.Cursor = Cursors.Hand;

                            lb.Click += lb_Click;
                            lb.MouseEnter += lb_MouseEnter;
                            lb.MouseLeave += lb_MouseLeave;
                            adcp.Controls.Add(lb);
                        }
                    }
                    adcp.Height = 800;
                }
            }
                             
            ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
            maindocpanel.Pages.Add(kp);
            DashBoard cn = new DashBoard();
            cn.Show();
            cn.TopLevel = false;
            //  splitContainer1.Panel2.Controls.Add(ad);
            kp.Controls.Add(cn);
            cn.Dock = DockStyle.Fill;
            kp.Text = "Home";
            kp.Name = "Home";
            AutoHidMenu();
        }

        bool notwork = true;
        ExpandCollapsePanel TocloseExp=null;
        void exp_ExpandCollapse(object sender, MakarovDev.ExpandCollapsePanel.ExpandCollapseEventArgs e)
        {
            ExpandCollapsePanel Expdem = (ExpandCollapsePanel)sender;

            if (TocloseExp == Expdem)
            {
                TocloseExp = null;
            }
            if (TocloseExp == null)
            {

                TocloseExp = Expdem;
            }
            else
            {
                TocloseExp.IsExpanded = false;
                TocloseExp = Expdem;
            }
        }

        void lb_MouseLeave(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            lb.ForeColor = Color.White;
        }

        void lb_MouseEnter(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            lb.ForeColor = Color.WhiteSmoke;
           
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
        void lb_Click(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in maindocpanel.Pages)
            { 
                if(kr.Name==lb.Text)
                {
                    maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    break;

                }
            }

            if (Flag == 0)
            {

                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                maindocpanel.Pages.Add(kp);

                if (lb.Text == "Country")
                {
                    Country cn = new Country(genEnum.Country);
                    cn.BackColor = Color.White;
                    cn.Show();
                    cn.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(cn);
                    cn.Dock = DockStyle.Fill;
                    kp.Text = cn.Text;
                    kp.Name = lb.Text;
                    cn.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                if (lb.Text == "New POS")
                {
                    POSDESK.POSForm posform = new POSDESK.POSForm();
                    posform.Show();
                    posform.TopLevel = false;
                    kp.Controls.Add(posform);
                    posform.Dock = DockStyle.Fill;
                    kp.Text = posform.Text;
                    kp.Name = posform.Text;
                    posform.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }

                if (lb.Text == "Receipt and Payment")
                {
                    Receipt_Report cn = new Receipt_Report();
                    cn.BackColor = Color.White;
                    cn.Show();
                    cn.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(cn);
                    cn.Dock = DockStyle.Fill;
                    kp.Text = cn.Text;
                    kp.Name = lb.Text;
                    cn.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                if (lb.Text == "Cheque Reports")
                {
                    reports.Cheque_Report chqrpt = new reports.Cheque_Report();
                    chqrpt.BackColor = Color.White;
                    chqrpt.Show();
                    chqrpt.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(chqrpt);
                    chqrpt.Dock = DockStyle.Fill;
                    kp.Text = chqrpt.Text;
                    kp.Name = lb.Text;
                    chqrpt.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "City")
                {
                   Country cn = new Country(genEnum.City);
                   // reports.CustomerList cn = new reports.CustomerList();
                    cn.Show();
                    cn.BackColor = Color.White;
                    cn.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(cn);
                    cn.Dock = DockStyle.Fill;
                    kp.Text = cn.Text;
                    kp.Name = lb.Text;
                   // kp.Focus();
                    cn.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;

                }
                else if (lb.Text == "Branch")
                {
                    Country cn = new Country(genEnum.Branch);
                    // reports.CustomerList cn = new reports.CustomerList();
                    cn.Show();
                    cn.BackColor = Color.White;
                    cn.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(cn);
                    cn.Dock = DockStyle.Fill;
                    kp.Text = cn.Text;
                    kp.Name = lb.Text;
                    // kp.Focus();
                    cn.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;

                }
                else if (lb.Text == "Item Locations")
                {
                    Country cn = new Country(genEnum.Location);
                    // reports.CustomerList cn = new reports.CustomerList();
                    cn.Show();
                    cn.BackColor = Color.White;
                    cn.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(cn);
                    cn.Dock = DockStyle.Fill;
                    kp.Text = cn.Text;
                    kp.Name = lb.Text;
                    // kp.Focus();
                    cn.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;

                }
                else if (lb.Text == "Customer Payment History") 
                {
                    Statements.Invoice_Payment cn = new Statements.Invoice_Payment(0);
                    cn.Show();
                    cn.BackColor = Color.White;
                    cn.TopLevel = false;
                    kp.Controls.Add(cn);
                    cn.Dock = DockStyle.Fill;
                    kp.Text = cn.Text;
                    kp.Name = lb.Text;
                    cn.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;

                }
                else if (lb.Text == "Supplier Payment History") 
                {
                    Statements.Invoice_Payment cn = new Statements.Invoice_Payment(1);
                    cn.Show();
                    cn.BackColor = Color.White;
                    cn.TopLevel = false;
                    kp.Controls.Add(cn);
                    cn.Dock = DockStyle.Fill;
                    kp.Text = cn.Text;
                    kp.Name = lb.Text;
                    cn.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;

                }
                else if (lb.Text == "Employee Manage")
                {
                    Employee.AddEmployee ad = new Employee.AddEmployee(genEnum.State);
                    ad.Show();
                    ad.BackColor = Color.White;
                    ad.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(ad);
                    ad.Dock = DockStyle.Fill;
                    kp.Text = ad.Text;
                    kp.Name = lb.Text;
                    ad.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }

                else if (lb.Text == "Tax Type")
                {
                    Tax_Type TxP = new Tax_Type();
                    TxP.Show();
                    TxP.BackColor = Color.White;
                    TxP.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(TxP);
                    TxP.Dock = DockStyle.Fill;
                    kp.Text = TxP.Text;
                    kp.Name = lb.Text;
                   
                    TxP.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Units")
                {
                    Country c = new Country(genEnum.Units);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Rate Slabs")
                {
                    Country c = new Country(genEnum.RateSlab);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Pay Type")
                {
                    Country c = new Country(genEnum.PayType);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Stock Report")
                {
                    Current_Stock c = new Current_Stock();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;

                }
                else if (lb.Text == "Single Barcode")
                {
                    Generate_Barcode c = new Generate_Barcode();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Job Role Privilege")
                {
                    EmployeeJobRole c = new EmployeeJobRole();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = "Job Role";
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;

                }
                else if (lb.Text == "Currency")
                {
                    Currency c = new Currency();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Supplier")
                {
                    SupplierMaster sm = new SupplierMaster();
                    sm.Show();
                    sm.BackColor = Color.White;
                    sm.TopLevel = false;
                    kp.Controls.Add(sm);
                    sm.Dock = DockStyle.Fill;
                    kp.Text = sm.Text;
                    kp.Name = lb.Text;
                    sm.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Item Register")
                {
                    Search_Items sm = new Search_Items();
                    sm.Show();
                    sm.BackColor = Color.White;
                    sm.TopLevel = false;
                    kp.Controls.Add(sm);
                    sm.Dock = DockStyle.Fill;
                    kp.Text = sm.Text;
                    kp.Name = lb.Text;
                    sm.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Type")
                {
                    Country c = new Country(genEnum.Type);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Group")
                {
                    Country c = new Country(genEnum.Group);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;

                }
                else if (lb.Text == "Category")
                {
                    Country c = new Country(genEnum.Category);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Brand")
                {
                    Country c = new Country(genEnum.TradeMark);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Product Discount")
                {
                    NewDiscount c = new NewDiscount();
                  //  Country c = new Country(genEnum.DiscountType);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Discount Type")
                {
                    Discount_Types c = new Discount_Types(genEnum.DiscountType);
                    //  Country c = new Country(genEnum.DiscountType);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Price Type")
                {
                    Country c = new Country(genEnum.PriceType);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Supplier Type")
                {
                    Country c = new Country(genEnum.SupplierType);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Customer Type")
                {
                    CustomerType type = new CustomerType();
                    type.Show();
                    type.BackColor = Color.White;
                    type.TopLevel = false;
                    kp.Controls.Add(type);
                    type.Dock = DockStyle.Fill;
                    kp.Text = type.Text;
                    kp.Name = lb.Text;
                    type.FormBorderStyle = FormBorderStyle.None;
                   // kp.Focus();
                    maindocpanel.SelectedPage = kp;
                   // type.Focus();
                }
                else if (lb.Text == "Customer")
                {
                    CustomerMaster custMaster = new CustomerMaster();
                    custMaster.Show();
                    custMaster.BackColor = Color.White;
                    
                    custMaster.TopLevel = false;
                    kp.Controls.Add(custMaster);
                    custMaster.Dock = DockStyle.Fill;
                    kp.Text = custMaster.Text;
                    custMaster.FormBorderStyle = FormBorderStyle.None;
                    kp.Name = lb.Text;
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }

                else if (lb.Text == "State")
                {
                    State state = new State(genEnum.State);
                    state.Show();
                    state.BackColor = Color.White;

                    state.TopLevel = false;
                    kp.Controls.Add(state);
                    state.Dock = DockStyle.Fill;
                    kp.Text = state.Text;
                    state.FormBorderStyle = FormBorderStyle.None;
                    kp.Name = lb.Text;
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }

                else if (lb.Text == "Cashier Report")
                {
                    reports.Salesman_report smrpt = new reports.Salesman_report();
                    smrpt.Show();
                    smrpt.BackColor = Color.White;

                    smrpt.TopLevel = false;
                    kp.Controls.Add(smrpt);
                    smrpt.Dock = DockStyle.Fill;
                    kp.Text = smrpt.Text;
                    smrpt.FormBorderStyle = FormBorderStyle.None;
                    kp.Name = lb.Text;
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }

                else if (lb.Text == "Stock Out")
                {
                    StockTransaction st = new StockTransaction("INV.STK.OUT", "Out");
                    st.Show();
                    st.BackColor = Color.White;
                    st.TopLevel = false;
                    kp.Controls.Add(st);
                    st.Dock = DockStyle.Fill;
                    kp.Text = st.Text;
                    st.FormBorderStyle = FormBorderStyle.None;
                    kp.Name = lb.Text;
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Stock In")
                {
                    StockTransaction st = new StockTransaction("INV.STK.IN", "In");
                    st.Show();
                    st.BackColor = Color.White;
                    st.TopLevel = false;
                    kp.Controls.Add(st);
                    st.Dock = DockStyle.Fill;
                    kp.Text = st.Text;
                    st.FormBorderStyle = FormBorderStyle.None;
                    kp.Name = lb.Text;
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Multiple Barcodes")
                {
                    Barcode_Stock_Items st = new Barcode_Stock_Items();
                    st.Show();
                    st.BackColor = Color.White;
                    st.TopLevel = false;
                    kp.Controls.Add(st);
                    st.Dock = DockStyle.Fill;
                    kp.Text = st.Text;
                    st.FormBorderStyle = FormBorderStyle.None;
                    kp.Name = lb.Text;
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                   
                else if (lb.Text == "Opening Stock")
                {
                    OpenStockEntry s = new OpenStockEntry();
                    s.BackColor = Color.White;
                    s.Show();
                    s.TopLevel = false;
                    kp.Controls.Add(s);
                    s.Dock = DockStyle.Fill;
                    kp.Text = s.Text;
                    s.FormBorderStyle = FormBorderStyle.None;
                    kp.Name = lb.Text;
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }

                else if (lb.Text == "Item Master")
                {
                    ItemMasterView ItemV = new ItemMasterView();
                    ItemV.Show();
                    ItemV.BackColor = Color.White;
                    ItemV.TopLevel = false;
                    kp.Controls.Add(ItemV);
                    ItemV.Dock = DockStyle.Fill;
                    kp.Text = ItemV.Text;
                    kp.Name = lb.Text;
                    ItemV.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }



                else if (lb.Text == "Item Excel Upload")
                {
                    Item_Mater_Bulk_Upload ItemV = new Item_Mater_Bulk_Upload();
                    ItemV.Show();
                    ItemV.BackColor = Color.White;
                    ItemV.TopLevel = false;
                    kp.Controls.Add(ItemV);
                    ItemV.Dock = DockStyle.Fill;
                    kp.Text = ItemV.Text;
                    kp.Name = lb.Text;
                    ItemV.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Monthly Report")
                {
                    Accounts.Monthly_Report mrpt = new Accounts.Monthly_Report();
              
                    mrpt.Show();
                    mrpt.BackColor = Color.White;
                    mrpt.TopLevel = false;
                    kp.Controls.Add(mrpt);
                    mrpt.Dock = DockStyle.Fill;
                    kp.Text = mrpt.Text;
                    kp.Name = lb.Text;
                    mrpt.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Forex Purchase")
                {
                    reports.Currency_Report crpt = new reports.Currency_Report();

                    crpt.Show();
                    crpt.BackColor = Color.White;
                    crpt.TopLevel = false;
                    kp.Controls.Add(crpt);
                    crpt.Dock = DockStyle.Fill;
                    kp.Text = crpt.Text;
                    kp.Name = lb.Text;
                    crpt.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Sales Quote")
                {

                    SaleQt sqt = new SaleQt("SAL.CSS", "Cash");
                    sqt.Show();
                    sqt.BackColor = Color.White;
                    sqt.TopLevel = false;
                    kp.Controls.Add(sqt);
                    sqt.Dock = DockStyle.Fill;
                    kp.Text = sqt.Text;
                    kp.Name = lb.Text;
                    sqt.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Purchase Order")
                {
                    Purchase_Order sqt = new Purchase_Order();
                    sqt.Show();
                    sqt.BackColor = Color.White;
                    sqt.TopLevel = false;
                    kp.Controls.Add(sqt);
                    sqt.Dock = DockStyle.Fill;
                    kp.Text = sqt.Text;
                    kp.Name = lb.Text;
                    sqt.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Supplier Payment")
                {
                    PaymentVoucher pv = new PaymentVoucher(0);
                    pv.Show();
                    pv.BackColor = Color.White;
                    pv.TopLevel = false;
                    kp.Controls.Add(pv);
                    pv.Dock = DockStyle.Fill;
                    kp.Text = "Supplier Payment";
                    kp.Name = lb.Text;
                    pv.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }
              
                else if (lb.Text == "Payroll Voucher")
                {
                    Payrollvouch pr = new Payrollvouch();
                    pr.Show();
                    
                    pr.TopLevel = false;
                    kp.Controls.Add(pr);
                    pr.Dock = DockStyle.Fill;
                    kp.Text = pr.Text;
                    kp.Name = lb.Text;
                    pr.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }

                else if (lb.Text == "ReceivablesPayables")
                {
                    reports.Recievables_and_Payables par = new reports.Recievables_and_Payables();
                    par.Show();
                    par.BackColor = Color.White;
                    par.TopLevel = false;
                    kp.Controls.Add(par);
                    par.Dock = DockStyle.Fill;
                    kp.Text = par.Text;
                    kp.Name = lb.Text;
                    par.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                }

                else if (lb.Text == "Purchases")
                {
                    PurchaseMaster m = new PurchaseMaster("PUR.CSS", "Cash");
                    m.Show();
                    m.BackColor = Color.White;
                    m.TopLevel = false;
                    kp.Controls.Add(m);
                    m.Dock = DockStyle.Fill;
                    kp.Text = m.Text;
                    kp.Name = lb.Text;
                    m.FormBorderStyle = FormBorderStyle.None;
                    //kp.Focus();
                    maindocpanel.SelectedPage = kp;
                    // m.Focus();
                    AutoHidMenu();
                }
                else if (lb.Text == "Purchase Credit")
                {
                    PurchaseMaster m = new PurchaseMaster("PUR.CRD", "Credit");
                    m.Show();
                    m.BackColor = Color.White;
                    m.TopLevel = false;
                    kp.Controls.Add(m);
                    m.Dock = DockStyle.Fill;
                    kp.Text = m.Text;
                    kp.Name = lb.Text;
                    m.FormBorderStyle = FormBorderStyle.None;
                    //  kp.Focus();
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Local Purchase")
                {
                    PurchaseMaster m = new PurchaseMaster("LGR.CSS", "Local Purchase");
                    m.Show();
                    m.BackColor = Color.White;
                    m.TopLevel = false;
                    kp.Controls.Add(m);
                    m.Dock = DockStyle.Fill;
                    kp.Text = m.Text;
                    kp.Name = lb.Text;
                    m.FormBorderStyle = FormBorderStyle.None;
                    // kp.Focus();
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Purchase Return")
                {
                    Purchase_Return m = new Purchase_Return("LGR.PRT", "");
                    m.Show();
                    m.BackColor = Color.White;
                    m.TopLevel = false;
                    kp.Controls.Add(m);
                    m.Dock = DockStyle.Fill;
                    kp.Text = m.Text;
                    kp.Name = lb.Text;
                    m.FormBorderStyle = FormBorderStyle.None;
                    //kp.Focus();
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Sales Cash")
                {
                    SalesQ sls = new SalesQ("SAL.CSS", "Cash");
                    sls.Show();
                    sls.BackColor = Color.White;
                    sls.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);s
                    kp.Controls.Add(sls);
                    sls.Dock = DockStyle.Fill;
                    kp.Text = sls.Text;
                    kp.Name = lb.Text;
                    sls.FormBorderStyle = FormBorderStyle.None;
                    sls.BackColor = Color.White;
                    //kp.Focus();
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Sales Credit")
                {
                    SalesQ sls = new SalesQ("SAL.CRD", "Cash");
                    sls.Show();
                    sls.BackColor = Color.White;
                    sls.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(sls);
                    sls.Dock = DockStyle.Fill;
                    kp.Text = sls.Text;
                    kp.Name = lb.Text;
                    sls.FormBorderStyle = FormBorderStyle.None;
                    sls.BackColor = Color.White;
                    //kp.Focus();
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Sales Return")
                {
                    Sales_Return sls = new Sales_Return("SAL.CSR", "Cash");
                    sls.Show();
                    sls.BackColor = Color.White;
                    sls.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(sls);
                    sls.Dock = DockStyle.Fill;
                    kp.Text = sls.Text;
                    kp.Name = lb.Text;
                    sls.FormBorderStyle = FormBorderStyle.None;
                    sls.BackColor = Color.White;
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                    //sls.Focus();

                }
                else if (lb.Text == "Customer Receipt")
                {
                    PaymentVoucher pv = new PaymentVoucher(1);
                    pv.Show();
                    pv.BackColor = Color.White;
                    pv.TopLevel = false;
                    kp.Controls.Add(pv);
                    pv.Dock = DockStyle.Fill;
                    kp.Text = "Customer Receipt" ;
                    kp.Name = lb.Text;
                    pv.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                    //sls.Focus();

                }
                else if (lb.Text == "Payment Voucher")
                {
                    PaymentVoucher2 p = new PaymentVoucher2(0);
                    p.Show();
                    p.BackColor = Color.White;
                    p.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(p);
                    p.Dock = DockStyle.Fill;
                    kp.Text = p.Text;
                    kp.Name = lb.Text;
                    p.FormBorderStyle = FormBorderStyle.None;
                    p.BackColor = Color.White;
                    //  kp.Focus();
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Receipt Voucher")
                {
                    PaymentVoucher2 p = new PaymentVoucher2(1);
                    p.Show();
                    p.BackColor = Color.White;
                    p.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(p);
                    p.Dock = DockStyle.Fill;
                    kp.Text = p.Text;
                    kp.Name = lb.Text;
                    p.FormBorderStyle = FormBorderStyle.None;
                    p.BackColor = Color.White;
                    // kp.Focus();
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Item Movement")
                {
                    ItemMovement p = new ItemMovement();
                    p.Show();
                    p.BackColor = Color.White;
                    p.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(p);
                    p.Dock = DockStyle.Fill;
                    kp.Text = p.Text;
                    kp.Name = lb.Text;
                    p.FormBorderStyle = FormBorderStyle.None;
                    p.BackColor = Color.White;
                    //kp.Focus();
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Supplier Statement")
                {
                    SupplierStatement s = new SupplierStatement();
                    s.Show();
                    s.BackColor = Color.White;
                    s.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(s);
                    s.Dock = DockStyle.Fill;
                    kp.Text = s.Text;
                    kp.Name = lb.Text;
                    s.FormBorderStyle = FormBorderStyle.None;
                    s.BackColor = Color.White;
                    // kp.Focus();
                    maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Switch Theme")
                {
                    kp.Dispose();
                    TheamChanger s = new TheamChanger();
                    s.BackColor = Color.White;
                    s.ShowDialog();
                    // kp.Dispose();
                    //s.TopLevel = false;
                    ////  splitContainer1.Panel2.Controls.Add(ad);
                    //kp.Controls.Add(s);
                    //s.Dock = DockStyle.Fill;
                    //kp.Text = s.Text;
                    //kp.Name = lb.Text;
                    //s.FormBorderStyle = FormBorderStyle.None;
                    //s.BackColor = Color.White;
                    //kp.Focus();
                    //maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Customer Statement")
                {
                    CustomerStatement s = new CustomerStatement();
                    s.Show();
                    s.TopLevel = false;
                    s.BackColor = Color.White;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(s);
                    s.Dock = DockStyle.Fill;
                    kp.Text = s.Text;
                    kp.Name = lb.Text;
                    s.FormBorderStyle = FormBorderStyle.None;
                    s.BackColor = Color.White;
                    // kp.Focus();
                    maindocpanel.SelectedPage = kp;
                }
                //else if (lb.Text == "Add Job Role")
                //{
                //    Employee.JobRole jr = new Employee.JobRole();
                //    jr.Show();
                //    jr.BackColor = Color.White;
                //    jr.TopLevel = false;
                //    //  splitContainer1.Panel2.Controls.Add(ad);
                //    kp.Controls.Add(jr);
                //    jr.Dock = DockStyle.Fill;
                //    kp.Text = jr.Text;
                //    kp.Name = lb.Text;
                //    jr.FormBorderStyle = FormBorderStyle.None;
                //    jr.BackColor = Color.White;
                //    //kp.Focus();
                //    maindocpanel.SelectedPage = kp;
                //}
                else if (lb.Text == "General Settings")
                {
                    GeneralSettings c = new GeneralSettings();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    // AutoHidMenu();
                }
                else if (lb.Text == "Purchase Report")
                {

                    Purchase_RPT_HDR c = new Purchase_RPT_HDR();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Sales Report")
                {
                    reports.Sales_Report_On_HDR c = new reports.Sales_Report_On_HDR();
                    //  reports.SalesReportFinal c = new reports.SalesReportFinal();
                    //  reports.SalesProfitReport c = new reports.SalesProfitReport();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }


                else if (lb.Text == "Emp Privilege ")
                {
                    EmployeePrivilege c = new EmployeePrivilege();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Vendor Info")
                {
                    Vendo_Info c = new Vendo_Info();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    // AutoHidMenu();

                }
                else if (lb.Text == "Updates")
                {
                    Updates c = new Updates();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    //  AutoHidMenu();

                }
                else if (lb.Text == "Barcode Settings")
                {
                    Barcode_Settings c = new Barcode_Settings();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;


                }
                else if (lb.Text == "Multiple Barcode")
                {
                    Barcode_Stock_Items c = new Barcode_Stock_Items();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "POS Desk")
                {
                    if (POSTerminal)
                    {
                        New_Sales c = new New_Sales("SAL.CSS", "Cash");
                        c.Show();
                        c.BackColor = Color.White;
                        c.TopLevel = false;
                        kp.Controls.Add(c);
                        c.Dock = DockStyle.Fill;
                        kp.Text = c.Text;
                        kp.Name = lb.Text;
                        c.FormBorderStyle = FormBorderStyle.None;

                        maindocpanel.SelectedPage = kp;
                        AutoHidMenu();
                    }
                    else
                    {
                        SalesQ c = new SalesQ("SAL.CSS", "Cash");
                        c.Show();
                        c.BackColor = Color.White;
                        c.TopLevel = false;
                        kp.Controls.Add(c);
                        c.Dock = DockStyle.Fill;
                        kp.Text = c.Text;
                        kp.Name = lb.Text;
                        c.FormBorderStyle = FormBorderStyle.None;

                        maindocpanel.SelectedPage = kp;
                        AutoHidMenu();
                    }

                }
                else if (lb.Text == "Data Backup")
                {
                    Backups c = new Backups();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Help")
                {
                    kp.Dispose();
                    Help c = new Help();
                    //c.Show();
                    c.BackColor = Color.White;
                    c.ShowDialog();
                    //c.TopLevel = false;
                    //kp.Controls.Add(c);
                    //c.Dock = DockStyle.Fill;
                    //kp.Text = c.Text;
                    //kp.Name = lb.Text;
                    //c.FormBorderStyle = FormBorderStyle.None;

                    //maindocpanel.SelectedPage = kp;
                }
                else if (lb.Text == "Item Price List")
                {

                    reports.Item_Price_List c = new reports.Item_Price_List();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Sales Summary")
                {

                    reports.ProfitRpt c = new reports.ProfitRpt();
                   // reports.Pr c = new reports.Pr();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Fast Moving Items")
                {

                    reports.FastMovingItem c = new reports.FastMovingItem();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Sales Variation")
                {

                    reports.SalesProfitReport c = new reports.SalesProfitReport();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Stock Limits")
                {

                    reports.Stock_Limits c = new reports.Stock_Limits();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Stock Adjustment")
                {

                    StockAdjust c = new StockAdjust();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Stock Adjustment Rpt")
                {

                    reports.Stock_Adjust_Report c = new reports.Stock_Adjust_Report();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Account Group")
                {

                    Accounts.frmAccountGroup c = new Accounts.frmAccountGroup();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Ledgers")
                {

                    Accounts.FrmLedger c = new Accounts.FrmLedger();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Day Book")
                {

                    Accounts.Day_Book c = new Accounts.Day_Book(lb.Text);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Opening Balance Entry")
                {

                    Accounts.Opening_Balance_Entry c = new Accounts.Opening_Balance_Entry();
                    c.Show();
                    //      c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    // AutoHidMenu();
                }
                else if (lb.Text == "Cash Book")
                {


                    Accounts.Day_Book c = new Accounts.Day_Book(lb.Text);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }

                else if (lb.Text == "Due Bills")
                {


                    Accounts.DueBills c = new Accounts.DueBills();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Trail Balance")
                {


                    Accounts.Trail_Balance c = new Accounts.Trail_Balance();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Chart Of Accounts")
                {


                    Accounts.Chart_of_Accounts c = new Accounts.Chart_of_Accounts();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Profit & Loss Account")
                {


                    Accounts.ProfitAndLoss c = new Accounts.ProfitAndLoss();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Account Voucher")
                {

                    Accounting_Voucher c = new Accounting_Voucher(0);
                    c.Show();
                    //c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Trading  P& L")
                {


                    Accounts.Trading_PL c = new Accounts.Trading_PL();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Trading  P& L")
                {


                    Accounts.Trading_PL c = new Accounts.Trading_PL();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }


                else if (lb.Text == "Debit Note")
                {


                    Accounts.Debit_Note c = new Accounts.Debit_Note(0);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }


                else if (lb.Text == "Credit Note")
                {


                    Accounts.Debit_Note c = new Accounts.Debit_Note(1);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }


                else if (lb.Text == "Balance Sheet")
                {


                    Accounts.Balance_Sheet c = new Accounts.Balance_Sheet();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Modified Log")
                {


                    Employee.Modified_Log c = new Employee.Modified_Log();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Employee Statement")
                {


                    Employee.EmployeeSalesLog c = new Employee.EmployeeSalesLog();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Ledger Report")
                {


                    Accounts.LedgerReport c = new Accounts.LedgerReport();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Find Warranty")
                {


                    findWarranty c = new findWarranty();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Item Level Offer")
                {


                    Item_level_offer c = new Item_level_offer(genEnum.Item_level_offer);
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Database Connection")
                {

                    frmMsSqlInstaller c = new frmMsSqlInstaller();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();

                }
                else if (lb.Text == "Cheque Report")
                {

                    reports.Cheque_Report chqrpt = new reports.Cheque_Report();
                    chqrpt.BackColor = Color.White;
                    chqrpt.Show();
                    chqrpt.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(chqrpt);
                    chqrpt.Dock = DockStyle.Fill;
                    kp.Text = chqrpt.Text;
                    kp.Name = lb.Text;
                    chqrpt.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;

                }
                else if (lb.Text == "Invoice Settings")
                {
                    InovoicePrinterSettings c = new InovoicePrinterSettings();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Production Entry")
                {
                    Manufacture.FrmProduction c = new Manufacture.FrmProduction();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Add Project")
                {
                    Project.Project c = new Project.Project();
                    c.Show();
                    c.BackColor = Color.White;
                    c.TopLevel = false;
                    kp.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    kp.Text = c.Text;
                    kp.Name = lb.Text;
                    c.FormBorderStyle = FormBorderStyle.None;

                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }
                else if (lb.Text == "Project Report")
                {
                    Project_report p = new Project_report();
                    p.Show();
                    p.BackColor = Color.White;
                    p.TopLevel = false;
                    kp.Controls.Add(p);
                    p.Dock = DockStyle.Fill;
                    kp.Text = p.Text;
                    kp.Name = lb.Text;
                    p.FormBorderStyle = FormBorderStyle.None;
                    maindocpanel.SelectedPage = kp;
                    AutoHidMenu();
                }

            }
            Flag = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

     

        public void AutoHidMenu()
        {
            if (splitContainer1.Panel1Collapsed == true)
            {

                splitContainer1.Panel1Collapsed = false;
            }
            else
            {
                splitContainer1.Panel1Collapsed = true;
            }
        }

        public void onlyhide()
        {
            if (splitContainer1.Panel1Collapsed == false)
            {

                splitContainer1.Panel1Collapsed = true;
            }
        }
        private void ovalShape1_Click(object sender, EventArgs e)
        {
            AutoHidMenu();
        }

        private void countryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            OpenStockEntry op = new OpenStockEntry();
            op.Show();
            op.TopLevel = false;
            splitContainer1.Panel2.Controls.Add(op);
            op.Dock = DockStyle.Fill;
           // splitContainer1.Panel1.Hide();
        }

        private void addEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();

            Employee.AddEmployee AE = new Employee.AddEmployee(genEnum.State);
            AE.Show();
            AE.TopLevel = false;
            splitContainer1.Panel2.Controls.Add(AE);
            AE.Dock = DockStyle.Fill;
           // splitContainer1.Panel1.Hide();
        }

     

    

     

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AutoHidMenu();
        }
        private void BK()
        {
            string strconn = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\oman db\12 oct 2016\SYSBIZZ.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = strconn;

            try
            {
                // First get the db name
                string dbname;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                dbname = cmd.Connection.Database.ToString();
                //Query per backup
                string queryBK = "BACKUP DATABASE " + dbname + " TO DISK = 'D:\\software\\SYSBIZZ.bak' WITH INIT, SKIP, CHECKSUM";
                SqlCommand cmdBK = new SqlCommand(queryBK, conn);
                conn.Open();
                cmdBK.ExecuteNonQuery();
                MessageBox.Show("backup effettuato");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Logout", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //BK();
                //back();
                ActivationCheck();

                

                emplog.Id = log.UpdateLog;
                emplog.UpdateEmplog();
                this.Hide();
                log.Show();
              //  Login log = new Login();
              //  log.Show();
            }
           // Application.Exit();
          //  Process.Start("Sys-Sols Inventory.exe");
        }

        public void ActivationCheck()
        {
            DataTable dt = new DataTable();
            dt = Act.GetDicrypt();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "1")
                {
                    string Status = Class.CryptorEngine.Decrypt(dt.Rows[0][4].ToString(), true);
                    if (Status == "No")
                    {
                        string Limitdate = Class.CryptorEngine.Decrypt(dt.Rows[0][2].ToString(), true);
                        if (Limitdate != "0")
                        {

                            Encrypt(dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());

                        }
                    }
                }
            }
        }

        public void Encrypt(string Date, string Value)
        {
            string cipherText = Class.CryptorEngine.Encrypt(DateTime.Now.ToShortDateString(), true);

            if (Date != cipherText)
            {
                string Decryptvalue = Class.CryptorEngine.Decrypt(Value, true);
                int a = Convert.ToInt32(Decryptvalue) - 1;
                Act.Value = Class.CryptorEngine.Encrypt(a.ToString(), true);
                Act.Status = Class.CryptorEngine.Encrypt("No", true);
                Act.Date = cipherText;
                Act.UpdateActivation();

            }
        }

        private void pictureBoxHome_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in maindocpanel.Pages)
            {
                if (kr.Name == "Home")
                {
                    maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    break;

                }
            }
            if (Flag == 0)
            {
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                maindocpanel.Pages.Add(kp);
                DashBoard cn = new DashBoard();
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = "Home";
                kp.Name = "Home";
                maindocpanel.SelectedPage = kp;
            }
            Flag = 0;
        }

        

        private void pictureBoxclosetab_Click(object sender, EventArgs e)
        {
            DashBoard cn = new DashBoard();            
            maindocpanel.Pages.Clear();
            ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
            maindocpanel.Pages.Add(kp);            
            cn.Show();
            cn.TopLevel = false;
            //  splitContainer1.Panel2.Controls.Add(ad);
            kp.Controls.Add(cn);
            cn.Dock = DockStyle.Fill;
            kp.Text = "Home";
            kp.Name = "Home";
            maindocpanel.SelectedPage = kp;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
            p.WaitForInputIdle();
        }

        private void maindocpanel_CloseAction(object sender, ComponentFactory.Krypton.Navigator.CloseActionEventArgs e)
        {
            ComponentFactory.Krypton.Docking.KryptonDockableNavigator n = sender as ComponentFactory.Krypton.Docking.KryptonDockableNavigator;
            ComponentFactory.Krypton.Navigator.KryptonPage k = new ComponentFactory.Krypton.Navigator.KryptonPage();
            k = n.SelectedPage;
            if (k.Name == "Home")
            {
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                maindocpanel.Pages.Add(kp);
                //DashBoard cn = (DashBoard)Application.OpenForms["DashBoard"];
                DashBoard cn = new DashBoard();
                cn.Show();
                cn.TopLevel = false;
                ////  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = "Home";
                kp.Name = "Home";
                maindocpanel.SelectedPage = kp;
                
            }
            
            
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Find_Item Fi = new Find_Item();
            Fi.ShowDialog();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
            {
                if (kr.Name == "Payment Voucher")
                {
                    this.maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    break;

                }
            }
            if (Flag == 0)
            {
                ItemMasterView cn = new ItemMasterView();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Payment Voucher";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;
                this.maindocpanel.SelectedPage = kp;
            }
            Flag = 0;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
            {
                if (kr.Name == "Receipt Voucher")
                {
                    this.maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    break;
                }
            }
            if (Flag == 0)
            {
                PaymentVoucher2 cn = new PaymentVoucher2(1);
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Receipt Voucher";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;
                this.maindocpanel.SelectedPage = kp;
            }
            Flag = 0;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
            {
                if (kr.Name == "Day Book")
                {
                    this.maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    break;
                }
            }
            if (Flag == 0)
            {
                Accounts.Day_Book cn = new Accounts.Day_Book("Day Book");
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Day Book";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;
                this.maindocpanel.SelectedPage = kp;
            }
            Flag = 0;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
            {
                if (kr.Name == "Payment Voucher")
                {
                    this.maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    break;
                }
            }
            if (Flag == 0)
            {
                PaymentVoucher2 cn = new PaymentVoucher2(0);
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Payment Voucher";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;
                this.maindocpanel.SelectedPage = kp;
            }
            Flag = 0;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
            {
                if (kr.Name == "Purchase Cash")
                {
                    this.maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    this.onlyhide();
                    break;
                }
            }
            if (Flag == 0)
            {
                PurchaseMaster cn = new PurchaseMaster("PUR.CSS", "Cash");
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Purchases";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;
                this.maindocpanel.SelectedPage = kp;
                this.onlyhide();
            }
            Flag = 0;
        }

        private void picitemmaster_MouseHover(object sender, EventArgs e)
        {
            PictureBox name = (sender as PictureBox);
            ToolTip tt = new ToolTip();
            switch (name.Name)
            {
                case "picitemmaster":
                    tt.SetToolTip(name, "Item Master");
                    break;
                case "picpurchase":
                    tt.SetToolTip(name, "Purchase");
                    break;
                case "picreceiptvoucher":
                    tt.SetToolTip(name, "Receipt Voucher");
                    break;
                case "picpaymentvoucher":
                    tt.SetToolTip(name, "Payment Voucher");
                    break;
                case "picdaybook":
                    tt.SetToolTip(name, "Day Book");
                    break;
                case "picsearchitem":
                    tt.SetToolTip(name, "Search Items");
                    break;
                case "piccalculator":
                    tt.SetToolTip(name, "Calculator");
                    break;
                case "piccloseall":
                    tt.SetToolTip(name, "Close All Windows");
                    break;
                case "picsales":
                    tt.SetToolTip(name, "Sales");
                    break;
                case "pichome":
                    tt.SetToolTip(name, "Dashboard");
                    break;
                case "picledger":
                    tt.SetToolTip(name, "Add Ledger");
                    break;
                case "piccashbook":
                    tt.SetToolTip(name, "Cash Book");
                    break;
                case "pic_sup":
                    tt.SetToolTip(name, "Supplier");
                    break;
                case "pic_cus":
                    tt.SetToolTip(name, "Customer");
                    break;
                case "pic_production":
                    tt.SetToolTip(name, "Production");
                    break;
                default:
                    break;
            }
        }

        private void picsales_Click(object sender, EventArgs e)
        {
            if (General.IsEnabled(Settings.POSTerminal))
            {
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
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
                this.maindocpanel.SelectedPage = kp;
                this.onlyhide();
            }
            else
            {
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
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
                this.maindocpanel.SelectedPage = kp;
                this.onlyhide();
            }
        }

        private void picitemmaster_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
            {
                if (kr.Name == "Item Master")
                {
                    this.maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    this.onlyhide();
                    break;
                }
            }
            if (Flag == 0)
            {
                ItemMasterView cn = new ItemMasterView();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Item Master";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;
                this.maindocpanel.SelectedPage = kp;
                this.onlyhide();
            }
            Flag = 0;
        }

        private void piccashbook_Click(object sender, EventArgs e)
        {
            //foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
            //{
            //    if (kr.Name == "Cash Book")
            //    {
            //        this.maindocpanel.SelectedPage = kr;
            //        Flag = 1;
            //        this.onlyhide();
            //        break;

            //    }
            //}
            //if (Flag == 0)
            //{
            //    Accounts.Day_Book cn = new Accounts.Day_Book("Cash Book");
            //    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
            //    this.maindocpanel.Pages.Add(kp);

            //    cn.Show();
            //    cn.TopLevel = false;
            //    //  splitContainer1.Panel2.Controls.Add(ad);
            //    kp.Controls.Add(cn);
            //    cn.Dock = DockStyle.Fill;
            //    kp.Text = cn.Text;
            //    kp.Name = "Cash Book";
            //    // kp.Focus();
            //    cn.FormBorderStyle = FormBorderStyle.None;

            //    this.maindocpanel.SelectedPage = kp;
            //    this.onlyhide();
            //}
            //Flag = 0;
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
            {
                if (kr.Name == "Ledger Report")
                {
                    this.maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    this.onlyhide();
                    break;
                }
            }
            if (Flag == 0)
            {
                Accounts.LedgerReport cn = new Accounts.LedgerReport();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Ledger Report";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;
                this.maindocpanel.SelectedPage = kp;
                this.onlyhide();
            }
            Flag = 0;

        }

        private void picleger_Click(object sender, EventArgs e)
        {
             foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
            {
                if (kr.Name == "ledgers")
                {
                    this.maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    this.onlyhide();
                    break;

                }
            }
            if (Flag == 0)
            {
                Accounts.FrmLedger cn = new Accounts.FrmLedger();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "FrmLedger";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;
                this.maindocpanel.SelectedPage = kp;
                this.onlyhide();
            }
            Flag = 0;
            //Accounts.LedgerReport cn = new Accounts.LedgerReport();
            //ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
            //this.maindocpanel.Pages.Add(kp);

            //cn.Show();
            //cn.TopLevel = false;
            ////  splitContainer1.Panel2.Controls.Add(ad);
            //kp.Controls.Add(cn);
            //cn.Dock = DockStyle.Fill;
            //kp.Text = cn.Text;
            //kp.Name = "Ledger Report";
            //// kp.Focus();
            //cn.FormBorderStyle = FormBorderStyle.None;

            //this.maindocpanel.SelectedPage = kp;
            //this.onlyhide();
         
        }

        public void back()
        {
            string ds = null, uid = null, pwd = null, db = null, path = null;
            SqlDataReader rd = null;//= new SqlDataReader();
            string query = "select * from tbl_backup where id='1'";            
            rd = Model.DbFunctions.GetDataReader(query);
            if (rd.Read() == true)
            {

                ds = rd["data_source"].ToString();
                uid = rd["user_id"].ToString();
                db = rd["db"].ToString();
                path = rd["path"].ToString();
                pwd = Convert.ToString(rd["pass"]);
            }
            Model.DbFunctions.CloseConnection();

            try
            {
                // Backups objBackup = new Backups();
                Server sqlServerInstance = new Server(new Microsoft.SqlServer.Management.Common.ServerConnection(new System.Data.SqlClient.SqlConnection("Data Source=" + ds + ";Initial Catalog=" + db + "; uid=" + uid + "; pwd=" + pwd + ";")));
                Backup objBackup = new Backup();
                string a = db;
                if (s == "Monday")
                {
                    objBackup.Devices.AddDevice(path + "\\sysbizz\\backup\\Monday\\Sysbizz.bak", DeviceType.File);
                    objBackup.Database = db;
                    objBackup.Action = BackupActionType.Database;
                    objBackup.SqlBackup(sqlServerInstance);
                    // MessageBox.Show("The backup of database " + "sysbizz" + " completed sccessfully", "Microsoft SQL Server Management Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (s == "Tuesday")
                {
                    objBackup.Devices.AddDevice(path + "\\sysbizz\\backup\\Tuesday\\Sysbizz.bak", DeviceType.File);
                    objBackup.Database = db;
                    objBackup.Action = BackupActionType.Database;
                    objBackup.SqlBackup(sqlServerInstance);
                    MessageBox.Show("The backup of database " + "sysbizz" + " completed sccessfully", "Microsoft SQL Server Management Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (s == "Wednesday")
                {
                    objBackup.Devices.AddDevice(path + "\\sysbizz\\backup\\Wed nesday\\Sysbizz.bak", DeviceType.File);
                    objBackup.Database = db;
                    objBackup.Action = BackupActionType.Database;
                    objBackup.SqlBackup(sqlServerInstance);
                    // MessageBox.Show("The backup of database " + "sysbizz" + " completed sccessfully", "Microsoft SQL Server Management Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (s == "Thursday")
                {
                    objBackup.Devices.AddDevice(path + "\\sysbizz\\backup\\Thursday\\Sysbizz.bak", DeviceType.File);
                    objBackup.Database = db;
                    objBackup.Action = BackupActionType.Database;
                    objBackup.SqlBackup(sqlServerInstance);
                    //MessageBox.Show("The backup of database " + "sysbizz" + " completed sccessfully", "Microsoft SQL Server Management Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else if (s == "Friday")
                {
                    objBackup.Devices.AddDevice(path + "\\sysbizz\\backup\\Friday\\Sysbizz.bak", DeviceType.File);
                    objBackup.Database = db;
                    objBackup.Action = BackupActionType.Database;
                    objBackup.SqlBackup(sqlServerInstance);
                    // MessageBox.Show("The backup of database " + "sysbizz" + " completed sccessfully", "Microsoft SQL Server Management Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (s == "Saturday")
                {
                    objBackup.Devices.AddDevice(path + "\\sysbizz\\backup\\Saturday\\Sysbizz.bak", DeviceType.File);
                    objBackup.Database = db;
                    objBackup.Action = BackupActionType.Database;
                    objBackup.SqlBackup(sqlServerInstance);
                    //   MessageBox.Show("The backup of database " + "sysbizz" + " completed sccessfully", "Microsoft SQL Server Management Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (s == "Sunday")
                {
                    objBackup.Devices.AddDevice(path + "\\sysbizz\\backup\\Sunday\\Sysbizz.bak", DeviceType.File);
                    objBackup.Database = db;
                    objBackup.Action = BackupActionType.Database;
                    objBackup.SqlBackup(sqlServerInstance);
                    // MessageBox.Show("The backup of database " + "sysbizz" + " completed sccessfully", "Microsoft SQL Server Management Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            back();
        }

        private void pictureBox7_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            ComponentFactory.Krypton.Docking.KryptonDockableNavigator n = sender as ComponentFactory.Krypton.Docking.KryptonDockableNavigator;
            ComponentFactory.Krypton.Navigator.KryptonPage k = new ComponentFactory.Krypton.Navigator.KryptonPage();
            k = n.SelectedPage;
            if (k.Name == "Home")
            {
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                maindocpanel.Pages.Add(kp);
                DashBoard cn = new DashBoard();
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = "Home";
                kp.Name = "Home";
                maindocpanel.SelectedPage = kp;
            }
        }

        private void Initial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Logout", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
              //  Properties.Settings.Default.ConnectionStrings = dsSettings.ConnOriginal();
               // Properties.Settings.Default.Save();
                SqlConnection con = new SqlConnection(ClassS.getConnection());
                Model.DbFunctions.conn = con;
               
                ActivationCheck();
                emplog.Id = log.UpdateLog;
                emplog.UpdateEmplog();
                this.Hide();
                log.Show();
            }
            else
            {
                e.Cancel = true;
            }
        }
        SClass ClassS = new SClass();
        private void btnSwitch_Click(object sender, EventArgs e)
        {
          // File.Delete(@"C:\Windows\System32\Root123\Root.xml");
          //  Directory.Delete(@"C:\Windows\System32\Root123");
            string currentConn = Properties.Settings.Default.ConnectionStrings;

            //if (dsSettings.ConnOriginal() == currentConn && dsSettings.ConnDuplicate() != string.Empty && btnSwitch.BackColor == Color.MediumVioletRed)
            //{
            //    dbSet = true;
            //    txt_Multiaccount.Visible = true;

            //    this.ActiveControl = txt_Multiaccount;

            //}
            //if (dsSettings.ConnDuplicate() == currentConn && dsSettings.ConnOriginal() != string.Empty && btnSwitch.BackColor == Color.MidnightBlue)
            //{
            //    Properties.Settings.Default.ConnectionStrings = dsSettings.ConnOriginal();
            //    Properties.Settings.Default.Save();
            //    btnSwitch.BackColor = System.Drawing.Color.MediumVioletRed;
            //    pictureBoxclosetab_Click(this.piccloseall, null);
            //    picsales.Focus();
            //}
            //if (dsSettings.ConnDuplicate() == string.Empty && btnSwitch.BackColor == Color.MediumVioletRed)
            //{
            //    dbSet = false;
            //    txt_Multiaccount.Visible = true;
            //    this.ActiveControl = txt_Multiaccount;

            //}

            if (CheckXml()!=""&& btnSwitch.BackColor == Color.MediumVioletRed)
            {
                dbSet = true;
                txt_Multiaccount.Visible = true;

                this.ActiveControl = txt_Multiaccount;

            }
            if (ClassS.getConnection()!=""&& btnSwitch.BackColor == Color.MidnightBlue)
            {
                //Properties.Settings.Default.ConnectionStrings = dsSettings.ConnOriginal();
                //Properties.Settings.Default.Save();
                SqlConnection con = new SqlConnection(ClassS.getConnection());
                Model.DbFunctions.conn = con;
                btnSwitch.BackColor = System.Drawing.Color.MediumVioletRed;
                pictureBoxclosetab_Click(this.piccloseall, null);
                picsales.Focus();
                
            }
            if (CheckXml() =="" && btnSwitch.BackColor == Color.MediumVioletRed)
            {
                dbSet = false;
                txt_Multiaccount.Visible = true;
                this.ActiveControl = txt_Multiaccount;

            }
            
        }
        public string CheckXml()
        {
            string conn = "";
            if (!Directory.Exists(@"C:\Windows\System32\Root123"))
            {
               // CreateXml();
                Directory.CreateDirectory(@"C:\Windows\System32\Root123");
                CreateXml();
            }
            else if (!File.Exists(@"C:\Windows\System32\Root123\Root.xml"))
            {
                CreateXml();
            }

            XmlDocument xml = new XmlDocument();

            xml.Load(@"C:\Windows\System32\Root123\Root.xml");

            foreach (XmlElement element in xml.SelectNodes("//RootLine"))
            {
                conn = element.InnerText;
                if (conn!="")
                {
                    conn= Class.CryptorEngine.Decrypt(conn, true);
                }
                
            }
            return conn;
        }
        public void CreateXml()
        {
            try
            {

                XElement xml = new XElement("Root",
                   new XElement("RootLine", ""));

                xml.Save(@"C:\Windows\System32\Root123\Root.xml");
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
            }
        }
        private void txt_Multiaccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txt_Multiaccount.Text == "incorrect")
                {
                    try
                    {
                        if (dbSet == true)
                        {
                           
                           // Properties.Settings.Default.ConnectionStrings = dsSettings.ConnDuplicate();
                           // Properties.Settings.Default.Save();
                            SqlConnection con= new SqlConnection(CheckXml());
                            Model.DbFunctions.conn = con;
                            duplicateDate = GetDate();
                            if (HasCurrentDate())
                            {
                                if (originalDate != duplicateDate)
                                {
                                    UpdateCurrentDate();
                                }
                            }
                            else
                            {
                                InsertCurrentDate();
                            }
                            btnSwitch.BackColor = Color.MidnightBlue;
                            txt_Multiaccount.Clear();
                            txt_Multiaccount.Visible = false;
                            pictureBoxclosetab_Click(sender,e);

                            DialogResult dr = MessageBox.Show("Do you Want to take Backup of Non-Tax Db?", "Sysbizz", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dr.Equals(DialogResult.Yes))
                            {
                                if (Properties.Settings.Default.backupLocation == "")
                                    BackupClass.Backup1(Environment.CurrentDirectory + "\\sysbizz\\backup-nt\\" + s);
                                else
                                    BackupClass.Backup1(Properties.Settings.Default.backupLocation + "\\sysbizz\\backup-nt\\" + s);
                            }
                        }
                        else
                        {
                            
                            picsales.Focus();
                            frmMsSqlInstaller MSQLI = new frmMsSqlInstaller();
                            MSQLI.Show();
                            MSQLI.BringToFront();
                            Initial.flag = true;
                            txt_Multiaccount.Clear();
                           
                            txt_Multiaccount.Visible = false;
                        }
                    }
                    catch
                    {
                        
                        picsales.Focus();
                        frmMsSqlInstaller MSQLI = new frmMsSqlInstaller();
                        MSQLI.Show();
                        MSQLI.BringToFront();
                        Initial.flag = true;
                        txt_Multiaccount.Clear();
                        txt_Multiaccount.Visible = false;
                    }

                }
                else
                {
                    txt_Multiaccount.Text = string.Empty;
                    MessageBox.Show("Password is incorrect");
                }
                Common.preventDingSound(e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                txt_Multiaccount.Visible = false;
            }  
        }
        public static string GetDate()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd;
            //conn.Open();
            //cmd = new SqlCommand("SELECT Date FROM tbl_CurrentDate", conn);
            //string CurrentDate = cmd.ExecuteScalar().ToString();
            //conn.Close();
            return General.getCurrentDate();
        }
        public static bool HasCurrentDate()
        {
        //    SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //    SqlCommand cmd;
            int countvalue = 0;
            //conn.Open();
            //cmd = new SqlCommand("SELECT COUNT(Date) FROM tbl_CurrentDate", conn);
            countvalue = General.hasGetCurrentDate();
          //  conn.Close();
            if (countvalue == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void InsertCurrentDate()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd;
            //cmd = new SqlCommand("INSERT INTO tbl_CurrentDate (Date)VALUES(@d1)", conn);
            //cmd.Parameters.Add("@d1", SqlDbType.Date).Value = originalDate;
            //conn.Open();
            //cmd.Connection = conn;
            //cmd.ExecuteNonQuery();
            //conn.Close();
            General.insertCurrentDate(originalDate);
        }
        public static void UpdateCurrentDate()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd;
            //cmd = new SqlCommand("UPDATE tbl_CurrentDate SET Date =@d1", conn);
            //cmd.Parameters.Add("@d1", SqlDbType.Date).Value = originalDate;
            //conn.Open();
            //cmd.Connection = conn;
            //cmd.ExecuteNonQuery();
            //conn.Close();
            General.updateCurrentDate(originalDate);
        }

        private void txt_Multiaccount_TextChanged(object sender, EventArgs e)
        {
           // MessageBox.Show("Password is incorrect");
        }

        private void pic_cus_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
            {
                if (kr.Name == "Customer")
                {
                    this.maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    break;
                }
            }
            if (Flag == 0)
            {
                CustomerMaster cn = new CustomerMaster();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Customer";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;
                this.maindocpanel.SelectedPage = kp;
            }
            Flag = 0;
        }

        private void pic_sup_Click(object sender, EventArgs e)
        {
            foreach (ComponentFactory.Krypton.Navigator.KryptonPage kr in this.maindocpanel.Pages)
            {
                if (kr.Name == "Supplier")
                {
                    this.maindocpanel.SelectedPage = kr;
                    Flag = 1;
                    break;
                }
            }
            if (Flag == 0)
            {
                SupplierMaster cn = new SupplierMaster();
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                this.maindocpanel.Pages.Add(kp);
                cn.Show();
                cn.TopLevel = false;
                //  splitContainer1.Panel2.Controls.Add(ad);
                kp.Controls.Add(cn);
                cn.Dock = DockStyle.Fill;
                kp.Text = cn.Text;
                kp.Name = "Supplier";
                // kp.Focus();
                cn.FormBorderStyle = FormBorderStyle.None;
                this.maindocpanel.SelectedPage = kp;
            }
            Flag = 0;
        }

        private void pic_production_Click(object sender, EventArgs e)
        {
            Manufacture.FrmProduction cn = new Manufacture.FrmProduction();
            ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.maindocpanel.Pages.Add(kp);
            cn.Show();
            cn.TopLevel = false;
            //  splitContainer1.Panel2.Controls.Add(ad);
            kp.Controls.Add(cn);
            cn.Dock = DockStyle.Fill;
            kp.Text = "Production";
            kp.Name = "Production";
            // kp.Focus();
            cn.FormBorderStyle = FormBorderStyle.None;
            this.maindocpanel.SelectedPage = kp;
        }

        private void chequeReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports.Cheque_Report chrpt = new reports.Cheque_Report();
            chrpt.Show();

        }

        private void cashierReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports.Salesman_report smrpt = new reports.Salesman_report();
            smrpt.Show();
        }

        private void menuMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void supplierPaymentHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customerPaymentHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void KryptonGroupPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
