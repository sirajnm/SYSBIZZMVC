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
    public partial class Main : Form
    {
        Class.Activation Act = new Class.Activation();
        Class.EmpLog emplog = new Class.EmpLog();
        Login lg = (Login)Application.OpenForms["Login"];
        string Empid="0";
        public Main()
        {
          //  Empid = id;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void NewWindow(Form form)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Text == form.Text)
                {
                    f.WindowState = FormWindowState.Normal;
                    f.Focus();
                    return;
                }

            }
            form.MdiParent = this;
            form.Show();
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.A | Keys.F | Keys.C))
            {
                //AppLicense.Scripts sc = new AppLicense.Scripts();
                //sc.ShowDialog();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void countryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Country);
            NewWindow(c);
        }

        private void cityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.City);
            NewWindow(c);
        }

        private void typeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Type);
            NewWindow(c);
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Group);
            NewWindow(c);
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Category);
            NewWindow(c);
        }

        private void tradeMarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.TradeMark);
            NewWindow(c);
        }

        private void customerTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerType type = new CustomerType();
            NewWindow(type);
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerMaster custMaster = new CustomerMaster();
            NewWindow(custMaster);
        }

        private void itemMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemMasterView itemMaster = new ItemMasterView();
            NewWindow(itemMaster);
        }

        private void unitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Units);
            NewWindow(c);
        }

        private void rateSlabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.RateSlab);
            NewWindow(c);
        }

        private void stockOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockTransaction st = new StockTransaction("INV.STK.OUT","Out");
            NewWindow(st);
        }

        private void stockInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockTransaction st = new StockTransaction("INV.STK.IN","In");
            NewWindow(st);
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupplierMaster sm = new SupplierMaster();
            NewWindow(sm);
        }

        private void branchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Branch);
            NewWindow(c);
        }

      



        private void saleCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // SalesNew s = new SalesNew("SAL.CSS", "Cash");
           // NewWindow(s);
        }

        private void saleCreditToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // SalesNew s = new SalesNew("SAL.CRD", "Cash");
           // NewWindow(s);
        }

        private void salesReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_Return Sr = new Sales_Return("SAL.CSR", "Cash");
            NewWindow(Sr);
            //Sales s = new Sales("SAL.CSR", "Cash");
            //NewWindow(s);
        }

        private void openingStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenStockEntry s = new OpenStockEntry();
            NewWindow(s);
        }

        private void paymentVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaymentVoucher2 p = new PaymentVoucher2(0);
            NewWindow(p);
        }

        private void receiptVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaymentVoucher2 r = new PaymentVoucher2(1);
            NewWindow(r);
        }

        private void supplierStatementToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SupplierStatement s = new SupplierStatement();
            NewWindow(s);
        }

        private void customerStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerStatement c = new CustomerStatement();
            NewWindow(c);
        }

        private void addEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee.AddEmployee ae = new Employee.AddEmployee(genEnum.State);
            NewWindow(ae);

        }

        private void addJobRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee.JobRole jr = new Employee.JobRole();
            NewWindow(jr);

        }

        private void Main_Load(object sender, EventArgs e)
        {
            Empid = lg.EmpId;
            disableall();
            //SalesNew s = new SalesNew("SAL.CSS", "Cash");
            //NewWindow(s);
            List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem tsmi in menuMain.Items)
            {
                if (tsmi.Enabled == true)
                {
                    foreach (object item in tsmi.DropDownItems)
                    {
                        if (((ToolStripDropDownItem)item).Enabled == true)
                        {
                            if (((ToolStripDropDownItem)item).Text == "POS Desk")
                            {
                                SALE.Enabled = true;
                            }
                            if (((ToolStripDropDownItem)item).Text == "Purchases")
                            {
                                PURCHASE.Enabled = true;
                            }
                            if (((ToolStripDropDownItem)item).Text == "General Settings")
                            {
                                SETTINGS.Enabled = true;
                            }
                            if (((ToolStripDropDownItem)item).Text == "Item Master")
                            {
                                ADDITEM.Enabled = true;
                            }
                            if (((ToolStripDropDownItem)item).Text == "Employee Manage")
                            {
                                EMPLOYEE.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        public void disableall()
        {
            SETTINGS.Enabled = false;
            SALE.Enabled = false;
            PURCHASE.Enabled = false;
            ADDITEM.Enabled = false;
            EMPLOYEE.Enabled = false;
        }
        private void entriesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

      
        private void saleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void employeePrivilageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeePrivilege EmpPriv = new EmployeePrivilege();
            NewWindow(EmpPriv);
            EmpPriv.DisplayinGrid();
        }

        private void formToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmForms from = new frmForms();
            NewWindow(from);
        }

        private void printerSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrintDesigner fprde = new frmPrintDesigner();
            NewWindow(fprde);
        }

        private void barcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Barcode_Settings barcode = new Barcode_Settings();
            NewWindow(barcode);
        }

        private void generateSingleBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Generate_Barcode Generbar = new Generate_Barcode();
            NewWindow(Generbar);
        }

        private void gnerateMultipleBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Barcode_Stock_Items BarStockItems = new Barcode_Stock_Items();
            NewWindow(BarStockItems);
        }

        private void creditNoteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           // SalesNew sales = new SalesNew("SAL.CREDITNOTE", "Credit Note");
           // NewWindow(sales);
            //Credit_Note CN = new Credit_Note("SAL.CREDITNOTE", "Cash");
            //NewWindow(CN);
        }

        private void companySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneralSettings CompSet = new GeneralSettings();
            NewWindow(CompSet);
        }

        private void currentStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Current_Stock curnst = new Current_Stock();
            NewWindow(curnst);
        }

        private void supplierTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.SupplierType);
            NewWindow(c);
        }

        private void discountTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.DiscountType);
            NewWindow(c);
        }

        private void priceTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.PriceType);
            NewWindow(c);
        }

        private void payTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.PayType);
            NewWindow(c);
        }

        private void currenctyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Currency Curr = new Currency();
            NewWindow(Curr);
        }

        private void typeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Type);
            NewWindow(c);
        }

        private void categoryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Category);
            NewWindow(c);
        }

        private void groupToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Group);
            NewWindow(c);
        }

        private void tradeMarkToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.TradeMark);
            NewWindow(c);
        }

        private void unitsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Units);
            NewWindow(c);
        }
        
        private void taxTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tax_Type Taxtype = new Tax_Type();
            NewWindow(Taxtype);
        }

        private void jobRolePrivilageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeJobRole Empjobrole = new EmployeeJobRole();
            NewWindow(Empjobrole);
        }

        private void switchThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        Class.Login Lg = new Class.Login();
        

        private void modernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure to change theme", "Alert", MessageBoxButtons.YesNo))
                {

                    Lg.Theam = 1;
                    Lg.Empid1 = Convert.ToInt32(lg.EmpId);
                    Lg.UpdateTheam();
                    MessageBox.Show("Updated! You need to restart Application to affect changes.");
                    //this.Visible = false;
                    //Login Login = new Login();
                    //Login.Show();
                }
            }
            catch
            {
            }
            
        }

        private void classicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure to change theme", "Alert", MessageBoxButtons.YesNo))
                {

                    Lg.Theam = 0;
                    Lg.Empid1 = Convert.ToInt32(Empid);
                    Lg.UpdateTheam();
                    MessageBox.Show("Updated! You need to restart Application to affect changes.");
                    //this.Visible = false;
                    //Login Login = new Login();
                    //Login.Show();
                }
            }
            catch
            {
            }
        }

        private void vendorInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vendo_Info vendor = new Vendo_Info();
            NewWindow(vendor);
        }

        private void updatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Updates upd = new Updates();
            NewWindow(upd);
        }

        private void pOSDeskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(General.IsEnabled(Settings.Barcode))
            {
            New_Sales newsales = new New_Sales("SAL.CSS", "Cash");
            NewWindow(newsales);
            }
            else
            {
                SalesQ c = new SalesQ("SAL.CSS", "Cash");
                NewWindow(c);
                
            }
        }

        private void gnerateMultipleBarcodeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Barcode_Stock_Items bs = new Barcode_Stock_Items();
            NewWindow(bs);
        }

        private void purchaseCashToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PurchaseMaster m = new PurchaseMaster("PUR.CSS", "Cash");
            NewWindow(m);
        }

        private void purchaseCreditToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PurchaseMaster m = new PurchaseMaster("PUR.CRD", "Credit");
            NewWindow(m);
        }

        private void localPurchaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PurchaseMaster m = new PurchaseMaster("LGR.CSS", "Local Purchase");
            NewWindow(m);
        }

        private void purchaseReturnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PurchaseMaster m = new PurchaseMaster("LGR.PRT", "Purchase Return");
            NewWindow(m);
        }

        private void purchaseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports.PurchaseReportNew ps = new reports.PurchaseReportNew();
            NewWindow(ps);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
        
             SalesQ s = new SalesQ("SAL.CSS", "Cash");
             NewWindow(s);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
           SalesQ sls = new SalesQ("SAL.CRD", "Cash");
NewWindow(sls);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
           SalesQ sls = new SalesQ("SAL.CSR", "Cash");
           NewWindow(sls);
            
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            reports.SalesReportFinal c = new reports.SalesReportFinal();
            NewWindow(c);
        }

        private void itemMovementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemMovement imv = new ItemMovement();
            NewWindow(imv);
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Logout", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ActivationCheck();
                this.Hide();
               // Login log = new Login();
              //  log.Show();
               
                emplog.Id = lg.UpdateLog;
                emplog.UpdateEmplog();
                this.Hide();
                lg.Show();
                
            }
        }


        public void ActivationCheck()
        {
            try
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
            catch
            {

            }
        }

        public void Encrypt(string Date, string Value)
        {

            try
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
            catch
            {
            }
        }
        private void dataBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Backups c = new Backups();
            NewWindow(c);
        }

        private void searchItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search_Items item=new Search_Items();
            NewWindow(item);
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Help h = new Help();
            NewWindow(h);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            GeneralSettings CompSet = new GeneralSettings();
            NewWindow(CompSet);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (General.IsEnabled(Settings.Barcode))
            {
                New_Sales newsales = new New_Sales("SAL.CSS", "Cash");
                NewWindow(newsales);
            }
            else
            {
                SalesQ c = new SalesQ("SAL.CSS", "Cash");
                NewWindow(c);

            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ItemMasterView itemMaster = new ItemMasterView();
            NewWindow(itemMaster);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            PurchaseMaster m = new PurchaseMaster("PUR.CSS", "Cash");
            NewWindow(m);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Employee.AddEmployee ae = new Employee.AddEmployee(genEnum.State);
            NewWindow(ae);
        }

        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
            p.WaitForInputIdle();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Help h = new Help();
            NewWindow(h);
        }

        private void stockAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockAdjust c = new StockAdjust();
            NewWindow(c);
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports.Sales_Report_On_HDR c = new reports.Sales_Report_On_HDR();
            NewWindow(c);
        }

        private void purchaseReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Purchase_RPT_HDR c = new Purchase_RPT_HDR();
            NewWindow(c);
        }

        private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Current_Stock c = new Current_Stock();
            NewWindow(c);
        }

        private void itemPriceListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports.Item_Price_List c = new reports.Item_Price_List();
            NewWindow(c);
        }

        private void salesSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports.SalesSummary c = new reports.SalesSummary();
            NewWindow(c);
        }

        private void fastMovingItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports.FastMovingItem c = new reports.FastMovingItem();
            NewWindow(c);
        }

        private void salesVariationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports.SalesProfitReport c = new reports.SalesProfitReport();
            NewWindow(c);
        }

        private void stockLimitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports.Stock_Limits c = new reports.Stock_Limits();
            NewWindow(c);
        }

        private void stockAdjustmentRptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports.Stock_Adjust_Report c = new reports.Stock_Adjust_Report();
            NewWindow(c);
        }

        private void modifiedLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee.Modified_Log c = new Employee.Modified_Log();
            NewWindow(c);
        }

        private void employeeStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee.EmployeeSalesLog c = new Employee.EmployeeSalesLog();
            NewWindow(c);
        }

        private void accountGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.frmAccountGroup c = new Accounts.frmAccountGroup();
            NewWindow(c);
        }

        private void ledgersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.FrmLedger c = new Accounts.FrmLedger();
            NewWindow(c);
        }

        private void dayBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.Day_Book c = new Accounts.Day_Book("Day Book");
            NewWindow(c);
        }

        private void openingBalanceEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.Opening_Balance_Entry c = new Accounts.Opening_Balance_Entry();
            NewWindow(c);
        }

        private void creditNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.Debit_Note c = new Accounts.Debit_Note(1);
            NewWindow(c);
        }

        private void debitNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.Debit_Note c = new Accounts.Debit_Note(0);
            NewWindow(c);
        }

        private void cashBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.Day_Book c = new Accounts.Day_Book("Cash Book");
            NewWindow(c);
        }

        private void dueBillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.DueBills c = new Accounts.DueBills();
            NewWindow(c);
        }

        private void trailBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.Trail_Balance c = new Accounts.Trail_Balance();
            NewWindow(c);
        }

        private void chartOfAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.Chart_of_Accounts c = new Accounts.Chart_of_Accounts();
            NewWindow(c);
        }

        private void tradingPLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.Trading_PL c = new Accounts.Trading_PL();
            NewWindow(c);
        }

        private void balanceSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.Balance_Sheet c = new Accounts.Balance_Sheet();
            NewWindow(c);
        }

        private void ledgerRepotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounts.LedgerReport Rpt = new Accounts.LedgerReport();
            NewWindow(Rpt);
        }

        private void productDiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDiscount ND = new NewDiscount();
            NewWindow(ND);
        }

        private void findWarrantyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findWarranty fnw = new findWarranty();
            NewWindow(fnw);
        }

        private void itemLevelOfferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Item_level_offer fnw = new Item_level_offer(genEnum.Item_level_offer);
            NewWindow(fnw);
        }

        private void chequeReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports.Cheque_Report chqrpt = new reports.Cheque_Report();
            NewWindow(chqrpt);
        }
    }
}
