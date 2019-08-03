using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory.Class
{
    class CompanySetup
    {     
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);   
        //private SqlCommand cmd = new SqlCommand(); 
        //private SqlDataAdapter adapter = new SqlDataAdapter();

        public int Id { get; set; }
        public String Company_Name { get; set; }
        public string ARBCompany_Name { get; set; }
        public String WebSite { get; set; }
        public String Logo { get; set; }
        public String Other_Details { get; set; }
        public String TIN_No { get; set; }
        public String CST_No { get; set; }
        public String PAN_No { get; set; }
        public String Country { get; set; }
        public string FYCODE { get; set; }
        public DateTime SDate { get; set; }
        public DateTime EDate { get; set; }
        public bool Status { get; set; }
        public int FinaYearId { get; set; }


        public String CODE { get; set; }
        public String DESC_ENG { get; set; }
        public string DESC_ARB { get; set; }     
        public int ALLOW_APPLIANCES { get; set; }
        public int ALLOW_SPARES { get; set; }
        public int ALLOW_SERVICES { get; set; }
        
        public string ADDRESS1 { get; set; }
        public string ARBADDRESS_1 { get; set; }
        public string ARBADDRESS_2 { get; set; }
        public string Acc_No { get; set; }
        public string ifsc { get; set; }


        public String ADDRESS_1 { get; set; }
        public String ADDRESS_2 { get; set; }
        public String TELE_1 { get; set; }
        public String DEFAULT_CURRENCY_CODE { get; set; }
        public String Email { get; set; }
        public String Fax { get; set; }
        public String BranchId { get; set; }

        public bool BATCH { get; set; }
        public bool BARCODE { get; set; }
        public bool POSTerminal { get; set; }
        public bool Free { get; set; }
        public bool Mrp { get; set; }
        public bool GrossValue { get; set; }
        public bool NetValue { get; set; }
        public bool Description { get; set; }
        public bool Arabic { get; set; }
        public string BranchCode { get; set; }
        public bool TAX { get; set; }
        public string Payment_Voucher { get; set; }
        public string Receipt_Voucher { get; set; }
        public string Invoice { get; set; }
        public bool priceBatch { get; set; }
        public bool HasType { get; set; }
        public bool HasCategory { get; set; }
        public bool HasGroup { get; set; }
        public bool HasTM { get; set; }

        public bool MoveToPrice { get; set; }
        public bool MoveToDisc { get; set; }
        public bool ShowPurchase { get; set; }
        public bool MoveToTaxper { get; set; }
        public bool FocusCustomer { get; set; }
        public bool FocusSalesMan { get; set; }
        public bool Focus_Rate_type { get; set; }
        public bool Focus_Sale_Type { get; set; }
        public bool MoveToUnit { get; set; }
        public bool MoveToQty { get; set; }
        public bool SalebyItemName { get; set; }
        public bool SalebyItemCode { get; set; }
        public bool ExclusiveTax { get; set; }
        public bool SalebyBarcode { get; set; }
        public bool Inv_print { get; set; }

        public bool SelectLastPurchase { get; set; }
        public bool HasRoundOff { get; set; }
        public decimal DiscountPerct { get; set; }
        public decimal DiscountAmt { get; set; }
        public bool HasDiscountLimit { get; set; }
        public bool FocusDate { get; set; }

        public string ItemName { get; set; }
        public bool PrintInvoice { get; set; }
        public string DefaultRateType { get; set; }
        public string DefaultSaleType { get; set; }
        public string DefaultTax { get; set; }
        public bool StockOut { get; set; }
        public bool AllowCustomerDiscount { get; set; }

        //purchase form
        public bool PUR_MoveDisc { get; set; }
        public bool PUR_MoveRtlper { get; set; }
        public bool PUR_MoveRtlAmt { get; set; }
        public bool PUR_MoveTaxper { get; set; }
        public bool PUR_MoveTaxAmt { get; set; }
        public bool PUR_MoveTotal { get; set; }
        public bool PUR_FocusDate { get; set; }
        public bool PUR_FocusSupplier { get; set; }
        public bool PUR_FocusInvoice { get; set; }
        public bool PUR_FocusReference { get; set; }
        public bool PUR_FocusBarcode { get; set; }
        public bool PUR_FocusItemCode { get; set; }
        public bool PUR_FocusItemName { get; set; }
        public string Rep_LoadinDays { get; set; }
        public decimal PUR_expcper { get; set; }
        public decimal Dec_qty { get; set; }
        public int PurchaseAccountLedger { get; set; }
        public int InventoryAccountLedger { get; set; }

        public int SalesLedger { get; set; }
        public int COGSLedger { get; set; }

        public string CREDIT_PERIOD { get; set; }
        public bool ACTIVE_PERIOD { get; set; }
        public string DEBIT_PERIOD { get; set; }
        public bool ACTIVE_DEBIT_PERIOD{get;set;}
        public string com_Name;
        public bool PUR_tax_Exclusive { get; set; }
        public bool Pro_tax_Exclusive { get; set; }

        public string B2B { get; set; }
        public string B2C { get; set; }

        public DataTable getCompanyDetails()
        {
            string Query = "Select * from Tbl_CompanySetup";
            return DbFunctions.GetDataTable(Query);
        }

        public bool ReadCompanyName()
        {
            try
            {
                string Query = "SELECT DESC_ENG,Current_Branch FROM SYS_SETUP";
                DataTable dt = DbFunctions.GetDataTable(Query);
               
                if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception EXC)
            {
                MessageBox.Show("Database Server is Not Installed, Contact product vendor"+EXC.Message);
                return false;
            }
        }

        public string ReadBranch()
        {
            string Query = "SELECT Current_Branch FROM SYS_SETUP";
            string st = (string)DbFunctions.GetAValue(Query);
            return st;
        }
        public void updateTaxLedger(int id, string taxtype)
        {
          
            string Query = "update tb_ledgers SET LEDGERNAME=@LEDGER WHERE LEDGERID=" + id;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@LEDGER", taxtype);
            DbFunctions.InsertUpdate(Query, parameters);

        }
        public String getCurrentState()
        {
            string st = "";
            string Query = "SELECT CurrentState FROM SYS_SETUP";
            if (DbFunctions.GetAValue(Query) != DBNull.Value && DbFunctions.GetAValue(Query) != "")
            {
                st = (string)DbFunctions.GetAValue(Query);
            }
            return st;
        }

        public DataTable getcompanyname()
        {
            string Query = "Select Company_Name from Tbl_CompanySetup";
            return DbFunctions.GetDataTable(Query);
        }

        public void GetFinancialYearStart()
        {            
            string Query = "Acc_FinancialStartDate";
            Dictionary<string, object> parameters =new Dictionary<string,object>();
            parameters.Add("@Status", Status);
            DbFunctions.InsertUpdateProcedure(Query, parameters);

        }


        public void UpdateFinancialYearStatus()
        {
            string Query = "update tbl_FinancialYear set Status=@Status where Status='true'";
            Dictionary<string, object> parameters =new Dictionary<string,object>();
            parameters.Add("@Status", Status);
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void UpdateFinancialYear()
        {
            string Query = "update tbl_FinancialYear set SDate=@SDate, EDate=@EDate, Status=@Status where Status='true'";
            Dictionary<string, object> parameters =new Dictionary<string,object>();
            parameters.Add("@SDate", SDate);
            parameters.Add("@EDate", EDate);
            parameters.Add("@Status", Status);
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void insertFinancialYear()
        {
            string Query = "insert into tbl_FinancialYear(FinancialYearCode, SDate, EDate, Status, CurrentFY) values(@FYCODE, @SDate, @EDate, @Status, 1)";
            Dictionary<string, object> parameters =new Dictionary<string,object>();
            parameters.Add("@FYCODE", FYCODE);
            parameters.Add("@SDate", SDate);
            parameters.Add("@EDate", EDate);
            parameters.Add("@Status", true);
            DbFunctions.InsertUpdate(Query, parameters);
        }


        public void insertcompanydetails()
        {
            string Query = "insert into Tbl_CompanySetup(Company_Name,ARBCompany_Name, Logo, TIN_No, PAN_No, CST_No, WebSite,Country) values(@Company_Name,@ARBCompany_Name,@Logo,@TIN_No,@PAN_No,@CST_No,@WebSite,@country)";
            Dictionary<string, object> parameters =new Dictionary<string,object>();
            parameters.Add("@Company_Name", Company_Name);
            parameters.Add("@ARBCompany_Name", ARBCompany_Name);
            parameters.Add("@Logo", Logo);
            parameters.Add("@TIN_No", TIN_No);
            parameters.Add("@PAN_No", PAN_No);
            parameters.Add("@CST_No", CST_No);
            parameters.Add("@WebSite", WebSite);
            parameters.Add("@country", Country);
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void updateBank()
        {
            string Query = "update Tbl_CompanySetup SET ACC_NO=@accno,IFSC=@ifsc WHERE Id=@Id";
            Dictionary<string, object> parameters =new Dictionary<string,object>();
            parameters.Add("@accno", Acc_No);
            parameters.Add("@ifsc", ifsc);
            parameters.Add("@Id", Id);          
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void Prefix_For_Invoice()
        {
            string Query = "SELECT COUNT(*) FROM GEN_DOC_SERIAL";
            try
            {
                int row = Convert.ToInt32(DbFunctions.GetAValue(Query));
                if (row == 0)
                {
                    Query = "INSERT INTO GEN_DOC_SERIAL(BRANCH_CODE,DOC_TYPE,PRIFIX,SUFIX) VALUES(@branch,'SALES',@B2B,@B2C)";
                    Dictionary<string, object> parameters =new Dictionary<string,object>();
                    parameters.Add("@branch", BranchCode);
                    parameters.Add("@B2B", B2B);
                    parameters.Add("@B2C", B2C);
                    DbFunctions.InsertUpdate(Query, parameters);
                }
                else
                {
                    Query = "UPDATE GEN_DOC_SERIAL SET PRIFIX=@B2B,SUFIX=@B2C";
                    Dictionary<string, object> parameters =new Dictionary<string,object>();
                    parameters.Add("@B2B", B2B);
                    parameters.Add("@B2C", B2C);
                    DbFunctions.InsertUpdate(Query, parameters);
                }
            }
            catch(SqlException e)
            {
                MessageBox.Show(e.Message);
            }


        }

        public void updateCompanydetils()
        {
            
            string Query = "update Tbl_CompanySetup set Company_Name=@Company_Name,Logo=@Logo,Other_Details=@Other_Details,TIN_No=@TIN_No,PAN_No=@PAN_No,CST_No=@CST_No,WebSite=@WebSite,ARBCompany_Name=@ARBCompany_Name where Id=@Id";
            Dictionary<string, object> parameters =new Dictionary<string,object>();
            parameters.Add("@Id", Id);
            parameters.Add("@Company_Name", Company_Name);
            parameters.Add("@Logo", Logo);
            parameters.Add("@Other_Details", Other_Details);
            parameters.Add("@TIN_No", TIN_No);
            parameters.Add("@PAN_No", PAN_No);
            parameters.Add("@CST_No", CST_No);
            parameters.Add("@WebSite", WebSite);
            parameters.Add("@ARBCompany_Name", DESC_ARB);
            DbFunctions.InsertUpdate(Query, parameters);
        }
        public void UpdateReportSetup()
        {
            string Query = "update SYS_SETUP set Rep_LoadinDays=@Rep_LoadinDays where DESC_ENG=@CompanyName";
            Dictionary<string, object> parameters =new Dictionary<string,object>();
            parameters.Add("@CompanyName", Company_Name);
            parameters.Add("@Rep_LoadinDays", Rep_LoadinDays);
            DbFunctions.InsertUpdate(Query, parameters);

        }

        public void UpdateProduction()
        {
            try
            {
                string Query = "update SYS_SETUP set Production_Tax=@Ptax where DESC_ENG=@CompanyName";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@CompanyName", Company_Name);
                parameters.Add("@Ptax", Pro_tax_Exclusive);
                DbFunctions.InsertUpdate(Query, parameters);
            }
            catch
            {
 
            }
        }

        public void UpdateGeneralSetup()
        {

            string Query = "update SYS_SETUP set MoveToPrice=@MoveToPrice,SelectLastPurchase=@SelectLastPurchase,"
            + "MoveToDisc=@MoveToDisc,ShowPurchase=@ShowPurchase,FocusCustomer=@FocusCustomer,FocusSalesMan=@FocusSalesMan,"
            + "MoveToUnit=@MoveToUnit,MoveToQty=@MoveToQty,SalebyItemName=@SalebyItemName,SalebyItemCode=@SalebyItemCode,"
            + "SalebyBarcode=@SalebyBarcode,MoveToTaxper=@MoveToTaxper,DiscountPerct=@DiscountPerct,DiscountAmt=@DiscountAmt,"
            + "HasDiscountLimit=@HasDiscountLimit,FocusDate=@FocusDate, AllowCustomerDiscount=@AllowCustomerDiscount,"
            + "StockOut=@StockOut,Free=@Free,Mrp=@Mrp,GrossValue=@GrossValue,NetValue=@NetValue,Description=@Description,DefaultRateType=@DefaultRateType, DefaultSaleType=@DefaultSaleType,"
            + "PrintInvoice=@PrintInvoice,Focus_Rate_type=@Focus_Rate_Type,Focus_Sale_Type=@Focus_Sale_Type,Exclusive_tax=@Exclusive_tax, "
                        + "SalesLedger=@SalesLedgerAccountLedger, COGSLedger=@COGSLedgerAccountLedger "
                        + " where DESC_ENG=@CompanyName ";
                        

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@CompanyName", Company_Name);
            parameters.Add("@MoveToPrice", MoveToPrice);
            parameters.Add("@HasRoundOff", HasRoundOff);
            parameters.Add("@SelectLastPurchase", SelectLastPurchase);
            parameters.Add("@MoveToDisc", MoveToDisc);
            parameters.Add("@ShowPurchase", ShowPurchase);
            parameters.Add("@SalebyBarcode", SalebyBarcode);
            parameters.Add("@SalebyItemCode", SalebyItemCode);
            parameters.Add("@SalebyItemName", SalebyItemName);
            parameters.Add("@MoveToQty", MoveToQty);
            parameters.Add("@MoveToUnit", MoveToUnit);
            parameters.Add("@FocusCustomer", FocusCustomer);
            parameters.Add("@FocusSalesMan", FocusSalesMan);
            parameters.Add("@MoveToTaxper", MoveToTaxper);
            parameters.Add("@DiscountAmt", DiscountAmt);
            parameters.Add("@DiscountPerct", DiscountPerct);
            parameters.Add("@HasDiscountLimit", HasDiscountLimit);
            parameters.Add("@FocusDate", FocusDate);
            parameters.Add("@AllowCustomerDiscount", AllowCustomerDiscount);
            parameters.Add("@StockOut", StockOut);
            parameters.Add("@Free", Free);
            parameters.Add("@Mrp", Mrp);
            parameters.Add("@GrossValue", GrossValue);
            parameters.Add("@NetValue", NetValue);
            parameters.Add("@Description", Description);
            parameters.Add("@DefaultRateType", DefaultRateType);
            parameters.Add("@DefaultSaleType", DefaultSaleType);
            parameters.Add("@PrintInvoice", PrintInvoice);
            parameters.Add("@Focus_Rate_Type", Focus_Rate_type);
            parameters.Add("@Focus_Sale_Type", Focus_Sale_Type);
            parameters.Add("@Exclusive_tax", ExclusiveTax);
            parameters.Add("@SalesLedgerAccountLedger", SalesLedger);
            parameters.Add("@COGSLedgerAccountLedger", COGSLedger);
       
            DbFunctions.InsertUpdate(Query, parameters);




        }

        public void UpdateGeneralItemSetup()
        {

            string Query = "update SYS_SETUP set HasType=@HasType,HasCategory=@HasCategory,HasGroup=@HasGroup,"
            + "HasTM=@HasTM ,BARCODE=@BARCODE,Current_Branch=@Current_Branch, Arabic=@Arabic,"
            + "POSTerminal=@POSTerminal,"
            + "Receipt_Voucher=@Receipt_Voucher,Payment_Voucher=@Payment_Voucher,Invoice=@Invoice,BATCH=@BATCH,"
            + "DefaultTax=@DefaultTax,ItemName=@ItemName,Dec_qty=@Dec_qty,ACTIVE_PERIOD=@ACTIVE_PERIOD,"
            + "CREDIT_PERIOD=@CREDIT_PERIOD,TAX=@TAX ,ACTIVE_DEBIT_PERIOD=@ACTIVE_DEBIT_PERIOD ,DEBIT_PERIOD=@DEBIT_PERIOD,"
            + "HasRoundOff=@HasRoundOff,priceBatch=@priceBatch where DESC_ENG=@CompanyName";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@HasType", HasType);
            parameters.Add("@HasCategory", HasCategory);
            parameters.Add("@HasGroup", HasGroup);
            parameters.Add("@HasTM", HasTM);
            parameters.Add("@BARCODE", BARCODE);
            parameters.Add("@Current_Branch", BranchCode);
            parameters.Add("@Arabic", Arabic);
            parameters.Add("@POSTerminal", POSTerminal);
            parameters.Add("@Receipt_Voucher", Receipt_Voucher);
            parameters.Add("@Payment_Voucher", Payment_Voucher);
            parameters.Add("@Invoice", Invoice);
            parameters.Add("@BATCH", BATCH);
            parameters.Add("@CompanyName", Company_Name);
            parameters.Add("@DefaultTax", DefaultTax);
            parameters.Add("@ItemName", ItemName);
            parameters.Add("@HasRoundOff", HasRoundOff);
            parameters.Add("@Dec_qty", Dec_qty);
            parameters.Add("@ACTIVE_PERIOD", ACTIVE_PERIOD);
            parameters.Add("@CREDIT_PERIOD", CREDIT_PERIOD);
            parameters.Add("@TAX", TAX);
            parameters.Add("@ACTIVE_DEBIT_PERIOD", ACTIVE_DEBIT_PERIOD);
            parameters.Add("@priceBatch", priceBatch);
            parameters.Add("@DEBIT_PERIOD", DEBIT_PERIOD);
          


            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void UpdateGeneralPurchaseSetup()
        {
            string Query = "UPDATE SYS_SETUP SET PurchaseLedger = @PurchaseAccountLedger, InventoryLedger = @InventoryAccountLedger,  PUR_MoveDisc = @PUR_MoveDisc, PUR_MoveRtlper = @PUR_MoveRtlper, PUR_MoveRtlAmt = @PUR_MoveRtlAmt, PURMoveTaxper = @PUR_MoveTaxper, PURMoveTaxAmt = @PUR_MoveTaxAmt, PURMoveTotal = @PUR_MoveTotal, PURFocusDate = @PUR_FocusDate, PURFocusSupplier = @PUR_FocusSupplier, PUR_FocusInvoice = @PUR_FocusInvoice, PUR_FocusReference = @PUR_FocusReference, PUR_FocusBarcode = @PUR_FocusBarcode, PUR_FocusItemCode = @PUR_FocusItemCode,PUR_FocusItemName=@PUR_FocusItemName,PUR_expcper=@PUR_expcper,Pur_Exclusive_tax=@Pur_Exclusive_tax where DESC_ENG=@Company_Name ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@PUR_FocusBarcode", PUR_FocusBarcode);
            parameters.Add("@PUR_FocusDate", PUR_FocusDate);
            parameters.Add("@PUR_FocusInvoice", PUR_FocusInvoice);
            parameters.Add("@PUR_FocusItemCode", PUR_FocusItemCode);
            parameters.Add("@PUR_FocusReference", PUR_FocusReference);
            parameters.Add("@PUR_FocusSupplier", PUR_FocusSupplier);
            parameters.Add("@PUR_MoveDisc", PUR_MoveDisc);
            parameters.Add("@PUR_MoveRtlAmt", PUR_MoveRtlAmt);
            parameters.Add("@PUR_MoveRtlper", PUR_MoveRtlper);
            parameters.Add("@PUR_MoveTaxAmt", PUR_MoveTaxAmt);
            parameters.Add("@PUR_MoveTaxper", PUR_MoveTaxper);
            parameters.Add("@PUR_MoveTotal", PUR_MoveTotal);
            parameters.Add("@PUR_FocusItemName", true);
            parameters.Add("@Company_Name", Company_Name);
            parameters.Add("@PUR_expcper", PUR_expcper);
            parameters.Add("@Pur_Exclusive_tax", PUR_tax_Exclusive);
            parameters.Add("@PurchaseAccountLedger", PurchaseAccountLedger);
            parameters.Add("@InventoryAccountLedger", InventoryAccountLedger);
          
            DbFunctions.InsertUpdate(Query, parameters);
        }
        public DataTable getDEFAULTcurrency()
        {
            string Query = "Select  DEFAULT_CURRENCY from SYS_SETUP";
             return DbFunctions.GetDataTable(Query);
        }

        public DataTable getcurrency()
        {
            string Query = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM GEN_CURRENCY";
            return DbFunctions.GetDataTable(Query);
         }


        public DataTable GetFinancialYear()
        {
            string Query = "SELECT * from tbl_FinancialYear where Status=@Status";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Status", Status);
            return DbFunctions.GetDataTable(Query,parameters);

        }
        public DataTable GetCurrentFinancialYear()
        {
            string Query = "SELECT * from tbl_FinancialYear where  CurrentFY = 1";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Status", Status);
            return DbFunctions.GetDataTable(Query, parameters);

        }

        public Boolean IsCurrentFY(DateTime transdate)
        {
            string query = "Select * from tbl_FinancialYear where CurrentFY = 1 and @transdate between SDate and EDate";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@transdate", transdate);
            DataTable dt = DbFunctions.GetDataTable(query, parameters);
            if (dt.Rows.Count >= 1) return true;
            return false;


        }


        public DataTable getbaranchdetails()
        {

            string Query = "SELECT DESC_ENG, ADDRESS_1, ADDRESS_2, TELE_1, Email, Fax, ARBADDRESS_1, ARBADDRESS_2, DESC_ARB, CODE, DEFAULT_CURRENCY_CODE,ALLOW_SERVICE FROM            GEN_BRANCH";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable getbaranchs()
        {
            string Query = "SELECT CODE,DESC_ENG FROM GEN_BRANCH";
            return DbFunctions.GetDataTable(Query);

        }

        public void UpdateBranchDetails()
        {
            string Query = "update GEN_BRANCH set  CODE=@CODE, DESC_ENG=@DESC_ENG, DESC_ARB=@DESC_ARB, ALLOW_APPLIANCES=@ALLOW_APPLIANCES, ALLOW_SPARES=@ALLOW_SPARES,ALLOW_SERVICE=@ALLOW_SERVICE,ADDRESS_1=@ADDRESS_1,ADDRESS_2=@ADDRESS_2,TELE_1=@TELE_1,DEFAULT_CURRENCY_CODE=@DEFAULT_CURRENCY_CODE,Email=@Email,Fax=@Fax,ARBADDRESS_1=@ARBADDRESS_1,ARBADDRESS_2=@ARBADDRESS_2 where CODE=@CODE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ALLOW_APPLIANCES", ALLOW_APPLIANCES);
            parameters.Add("@CODE", CODE);
            parameters.Add("@DESC_ENG", DESC_ENG);
            parameters.Add("@DESC_ARB", DESC_ARB);
            parameters.Add("@ALLOW_SPARES", ALLOW_SPARES);
            parameters.Add("@ADDRESS_1", ADDRESS_1);
            parameters.Add("@ADDRESS_2", ADDRESS_2);
            parameters.Add("@ALLOW_SERVICE", ALLOW_SERVICES);
          
            parameters.Add("@TELE_1", TELE_1);
            parameters.Add("@DEFAULT_CURRENCY_CODE", DEFAULT_CURRENCY_CODE);
            parameters.Add("@Email", Email);
            parameters.Add("@Fax", Fax);
            parameters.Add("@BranchId", BranchId);
            parameters.Add("@ARBADDRESS_1", ARBADDRESS_1);
            parameters.Add("@ARBADDRESS_2", ARBADDRESS_2);
            DbFunctions.InsertUpdate(Query, parameters);            

        }

        public void InsertBranch()
        {
            string Query = "Insert into GEN_BRANCH( CODE, DESC_ENG, DESC_ARB, ALLOW_APPLIANCES, ALLOW_SPARES, ALLOW_SERVICE, ADDRESS_1, ADDRESS_2, TELE_1, DEFAULT_CURRENCY_CODE, Email,Fax,ARBADDRESS_1,ARBADDRESS_2) values( @CODE, @DESC_ENG, @DESC_ARB, @ALLOW_APPLIANCES, @ALLOW_SPARES, @ALLOW_SERVICE, @ADDRESS_1, @ADDRESS_2, @TELE_1, @DEFAULT_CURRENCY_CODE, @Email,@Fax,@ARBADDRESS_1,@ARBADDRESS_2)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
         
            parameters.Add("@CODE", CODE);
            parameters.Add("@DESC_ENG", DESC_ENG);
            parameters.Add("@DESC_ARB", DESC_ARB);
            parameters.Add("@ALLOW_SPARES", ALLOW_SPARES);
            parameters.Add("@ADDRESS_1", ADDRESS_1);
            parameters.Add("@ADDRESS_2", ADDRESS_2);
            parameters.Add("@ALLOW_SERVICE", ALLOW_SERVICES);
            parameters.Add("@ALLOW_APPLIANCES", ALLOW_APPLIANCES);
            parameters.Add("@TELE_1", TELE_1);
            parameters.Add("@DEFAULT_CURRENCY_CODE", DEFAULT_CURRENCY_CODE);
            parameters.Add("@Email", Email);
            parameters.Add("@Fax", Fax);
            //parameters.Add("@BranchId", BranchId);
            parameters.Add("@ARBADDRESS_1", ARBADDRESS_1);
            parameters.Add("@ARBADDRESS_2", ARBADDRESS_2);
            DbFunctions.InsertUpdate(Query, parameters);

        }

        public void deletebranch()
        {
            string Query = "delete from GEN_BRANCH where CODE=@CODE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@CODE", CODE);
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void syssetup_addBranch()
        {
            string Query = "Update SYS_SETUP set Current_Branch=@CODE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@CODE", "HDO");
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void syssetup_addcompany()
        {
            string Query = "Update SYS_SETUP set DESC_ENG=@CODE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@CODE", Company_Name);
            DbFunctions.InsertUpdate(Query, parameters);
      
        }


        public DataTable SysSetup_selectcompany()
        {
            string Query = "SELECT     * from SYS_SETUP";
            return DbFunctions.GetDataTable(Query);
        }

        public void syssetup_addcurrentbranch()
        {
           
            string Query = "update SYS_SETUP set Current_Branch=@CODE, BARCODE=@BARCODE,Arabic=@Arabic, POSTerminal=@POSTerminal where DESC_ENG=@DESC_ENG";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@DESC_ENG", Company_Name);
            parameters.Add("@CODE", CODE);
            parameters.Add("@BARCODE", BARCODE);
            parameters.Add("@Arabic", Arabic);
            parameters.Add("@POSTerminal", POSTerminal);
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public DataTable getGeneralSetup()
        {
            string Query = "SELECT POSTerminal, Arabic, TAX, BARCODE, Current_Branch,Receipt_Voucher, Payment_Voucher, Invoice,HasType,HasCategory,HasGroup,HasTM,BATCH,"
            + "movetoprice,selectlastpurchase,hasroundoff,movetodisc,showpurchase,salebybarcode,salebyitemcode,salebyitemname,movetoqty,movetounit,movetotaxper,focussalesman,"
            + "focuscustomer,showpurchase,discountperct,discountamt,hasdiscountlimit,focusdate,allowcustomerdiscount,defaulttax,stockout,defaultratetype, defaultsaletype,"
            + "printinvoice,itemname,pur_movedisc, pur_movertlper, pur_movertlamt, purmovetaxper, purmovetaxamt, purmovetotal, purfocusdate, purfocussupplier,"
            + "pur_focusinvoice,pur_focusreference, pur_focusbarcode, pur_focusitemcode, pur_focusitemname,active_period,credit_period,active_debit_period,debit_period,free,"
            + "Mrp,GrossValue,NetValue,Description,Focus_Rate_Type,Focus_Sale_Type,Exclusive_tax,Pur_Exclusive_tax,priceBatch FROM SYS_SETUP";
                return DbFunctions.GetDataTable(Query);
        }
        public DataTable getDec()
        {
            string Query = "SELECT  Dec_qty  FROM   SYS_SETUP";
            return DbFunctions.GetDataTable(Query);
        }
        public Boolean hasRoundoff()
        {           
            string Query = "SELECT  HasRoundoff  FROM   SYS_SETUP";
            Boolean value = Convert.ToBoolean(DbFunctions.GetAValue(Query));
            return value;
        }


        public DataTable selectallbranch()
        {
            string Query = "SELECT CODE + '  ' + DESC_ENG AS description, CODE FROM GEN_BRANCH";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable GetCurrentBranchDetails()
        {
            string Query = "SELECT GEN_BRANCH.DESC_ENG, GEN_BRANCH.ADDRESS_1, GEN_BRANCH.ADDRESS_2, GEN_BRANCH.TELE_1, GEN_BRANCH.Email, GEN_BRANCH.Fax, SYS_SETUP.Invoice, SYS_SETUP.Receipt_Voucher, SYS_SETUP.Payment_Voucher, GEN_BRANCH.DESC_ARB, GEN_BRANCH.ARBADDRESS_1, GEN_BRANCH.ARBADDRESS_2,GEN_BRANCH.DEFAULT_CURRENCY_CODE,GEN_BRANCH.ALLOW_SERVICE FROM            SYS_SETUP INNER JOIN GEN_BRANCH ON SYS_SETUP.Current_Branch = GEN_BRANCH.CODE AND SYS_SETUP.Current_Branch = GEN_BRANCH.CODE";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable DYNAMICPAGESIZE()
        {
            string Query = "Select * FROM INV_PRINTPOSITION where SETTINGS =1";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable DYNAMICPOSITIONS()
        {
            string Query = "Select * FROM INV_PRINTPOSITION where SETTINGS =0";
            return DbFunctions.GetDataTable(Query);

        }
        public DataTable getCountries()
        {
            string Query = "Select * FROM GEN_COUNTRY";
            return DbFunctions.GetDataTable(Query);

        }
        public void InsertCurrentDate(string Date)
        {
             string Query = "insert into tbl_CurrentDate (Date)values(@Date)";
             Dictionary<string, object> parameters = new Dictionary<string, object>();
             parameters.Add("@Date", Date);
             DbFunctions.InsertUpdate(Query, parameters);
        
        }


        public void DeleteCurrentDate()
        {
            string Query = "Delete from tbl_CurrentDate";
            DbFunctions.InsertUpdate(Query);
        }


        public int GetCurrentDate()
        {
            int countvalue = 0;
            
           string Query = "select isnull( count([Date]),0) from tbl_CurrentDate";
            string tmp= DbFunctions.GetAValue(Query).ToString();
            if (tmp != "")
            {
                countvalue = Convert.ToInt32(DbFunctions.GetAValue(Query));
            }
            else
            {
                countvalue = 0;
            }
            return countvalue;
        }

        public string GettDate()
        {
            string Query = "select Date from tbl_CurrentDate";
            string CurrentDate = DbFunctions.GetAValue(Query).ToString() ;
            return CurrentDate;
        }

        public void SysSetup_InsertCompany()
        {
            string Query = "INSERT INTO SYS_SETUP(DESC_ENG,Current_Branch) VALUES(@companyName,@branch)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@companyName", Company_Name);
            parameters.Add("@branch", "HDO");
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void SysSetup_UpdateCompanyName()
        {
            string Query = "UPDATE SYS_SETUP SET DESC_ENG=@desc_Eng WHERE DESC_ENG=@dsc_Eng";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@desc_Eng", Company_Name);
            parameters.Add("@dsc_Eng", com_Name);
            DbFunctions.InsertUpdate(Query, parameters);
        }
        public string getTaxUin()
        {
            string Query = "select TAX_UIN from GEN_COUNTRY WHERE CODE='"+Country+"'";
            string CurrentDate = DbFunctions.GetAValue(Query).ToString();
            return CurrentDate;
        }
    }
}
