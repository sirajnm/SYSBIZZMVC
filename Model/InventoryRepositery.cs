using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory.Model
{
    class InventoryRepositery
    {

        public static bool InventoryPurchase(ItemLedger ile)
        {

            ile.EntryNo = GetMaxEntryNO();
            return Insert(ile);

        }

        

        public static bool InventoryPurchase(InvPurchaseHdrDB purchhdr)
        {
            InvPurchaseDtlDB purchdetail = new InvPurchaseDtlDB();
            purchdetail.DocNo = purchhdr.DocNo;
            DataTable dt = purchdetail.getPurchaseDlt();
            foreach(DataRow dr in dt.Rows)
            {
                ItemLedger il = new ItemLedger();
                il.Branch = dr["Branch"].ToString();
                il.EntryDate = purchhdr.DocDateGre;
                il.EntryType = "Purchase";
                il.EntryNo = GetMaxEntryNO();
                il.BatchEntryNo = il.EntryNo;
                il.DocumentNo = purchhdr.DocNo;
                il.ItemNo = dr["Item_Code"].ToString();
                il.UOM = dr["UOM"].ToString();
                il.UOMQuantity = Convert.ToSingle(dr["QTY_RCVD"]);
                InvItemDirectoryUnits iu = new InvItemDirectoryUnits();
                iu.ItemCode = il.ItemNo;
                iu.Unitcode = il.UOM;
                il.Quantity = il.UOMQuantity * iu.GetUOM();
                il.CostValueApplied = Convert.ToSingle(dr["NET_AMOUNT"]);
                il.UnitCostApplied = il.CostValueApplied / il.Quantity;
               
                if (!Insert(il))
                    return false;
            }
            return true;
        }

        public static bool InventoryPurchase(InvPurchaseHdrDB purchhdr, bool edit)
        {
            InvPurchaseDtlDB purchdetail = new InvPurchaseDtlDB();
            purchdetail.DocNo = purchhdr.DocNo;
            DataTable dt = purchdetail.getPurchaseDlt();
            foreach (DataRow dr in dt.Rows)
            {
                ItemLedger il = new ItemLedger();
            }


                return true;
        }

        public static bool InventorySales(InvSalesHdrDB saleshdr)
        {
            InvSalesDtlDB salesdtl = new InvSalesDtlDB();
            salesdtl.DocNo = saleshdr.DocNo;
            DataTable dt = salesdtl.DtlsbyDocNo();
            foreach(DataRow dr in dt.Rows)
            {
                float ReqQty = Convert.ToSingle( dr["Quantity"]);

                float TrQty;
                List<ItemLedger> availableEntries = AvailableEntries(dr["Item_Code"].ToString(), saleshdr.DocDateGre);
                foreach (ItemLedger av in availableEntries)
                {


                    if (ReqQty <= av.Quantity)
                    {
                        TrQty = ReqQty;
                        ReqQty = 0;
                    }
                    else
                    {
                        TrQty = av.Quantity;
                        ReqQty = ReqQty - av.Quantity;

                    }

                    ItemLedger il = new ItemLedger();
                    il.Branch = saleshdr.Branch;
                    il.DocumentNo = saleshdr.DocNo;
                    il.EntryType = "Sales";
                    il.EntryDate = saleshdr.DocDateGre;
                    il.ItemNo = dr["Item_Code"].ToString(); 
                    il.UOM = dr["UOM"].ToString();

                   // il.UOMQuantity = -Convert.ToSingle(dr["UOM_Qty"]);
                    il.Quantity = -TrQty;
                    il.EntryNo = GetMaxEntryNO();
                    il.BatchEntryNo = av.EntryNo;
                    il.UnitCostApplied = av.CostValueApplied / av.Quantity;
                    il.CostValueApplied = il.UnitCostApplied * il.Quantity;

                    if (!Insert(il))
                    {
                        return false;
                    }
                    if (ReqQty == 0)
                    {
                        break;
                    }

                }

               
            }
            return true;

        }

          

        public static bool InventoryRetailSales(TransactionHeaderTEMP transhead)
        {
            foreach (TransactionDetailTEMP detail in transhead.TransactionDetails)
            {
                float ReqQty = detail.Quantity;

                float TrQty;
                List<ItemLedger> availableEntries = AvailableEntries(detail.ItemNo, transhead.TransactionDate);
                if(availableEntries.Count == 0)
                {
                    TrQty = ReqQty;
                    ReqQty = 0;

                    ItemLedger il = new ItemLedger();
                    il.Branch = transhead.Branch;
                    il.DocumentNo = transhead.TransactionNo;
                    il.EntryType = "Retail";
                    il.EntryDate = transhead.TransactionDate;
                    il.ItemNo = detail.ItemNo;
                    il.UOM = detail.UOM;

                    //il.UOMQuantity = -detail.UOMQty;
                    il.Quantity = -TrQty;
                    il.EntryNo = GetMaxEntryNO();
                    il.BatchEntryNo = il.EntryNo;
                    il.UnitCostApplied = 0;
                    il.CostValueApplied = il.UnitCostApplied * il.Quantity;

                    if (!Insert(il))
                    {
                        return false;
                    }
                    if (ReqQty == 0)
                    {
                        break;
                    }

                }
                foreach (ItemLedger av in availableEntries)
                {
                    
                    
                    if (ReqQty <= av.Quantity )
                    {
                        TrQty = ReqQty;
                        ReqQty = 0;
                    }else
                    {
                        TrQty = av.Quantity;
                        ReqQty = ReqQty - av.Quantity;

                    }

                    ItemLedger il = new ItemLedger();
                    il.Branch = transhead.Branch;
                    il.DocumentNo = transhead.TransactionNo;
                    il.EntryType = "Retail";
                    il.EntryDate = transhead.TransactionDate;
                    il.ItemNo = detail.ItemNo;
                    il.UOM = detail.UOM;

                    //il.UOMQuantity = -detail.UOMQty;
                    il.Quantity = -TrQty;
                    il.EntryNo = GetMaxEntryNO();
                    il.BatchEntryNo = av.EntryNo;
                    il.UnitCostApplied = av.CostValueApplied / av.Quantity;
                    il.CostValueApplied = il.UnitCostApplied * il.Quantity;
                    
                    if (!Insert(il))
                    {
                        return false;
                    }
                    if (ReqQty == 0)
                    {
                        break;
                    }

                }
                
            }
            return true;

        }
        private static List<ItemLedger> AvailableEntries(string ItemNo, DateTime TransDate)
        {
            string query = "Select BatchEntryNo, Min(EntryDate) BatchDate, sum(Quantity) AvailableQty, sum(CostValueApplied) CostValue from ItemLedger ";
            query += " where ItemNo = @ItemNo and EntryDate <= @EntryDate Group by BatchEntryNo  Having sum(Quantity) > 0 ";
            Dictionary<string, Object> parameter = new Dictionary<string, object>();
            parameter.Add("@EntryDate", TransDate);
            parameter.Add("@ItemNo", ItemNo);
            DataTable dt = DbFunctions.GetDataTable(query, parameter);
            List<ItemLedger> availableentries = new List<ItemLedger>();
            foreach(DataRow dr in dt.Rows)
            {
                ItemLedger availableentry = new ItemLedger();
                
                availableentry.EntryNo = Convert.ToInt16(dr["BatchEntryNo"]);
                availableentry.EntryDate = Convert.ToDateTime(dr["BatchDate"]);
                availableentry.Quantity = Convert.ToSingle(dr["AvailableQty"]);
                availableentry.CostValueApplied = Convert.ToSingle(dr["CostValue"]);
                availableentries.Add(availableentry);
            }
            return availableentries;
        }
        private static bool GetAverageCost(ref ItemLedger il)
        {
            string query = "select  ItemNo, Min(EntryDate) EntryDate, sum(Quantity) Quantity, sum(CostValueApplied) CostAmount from ItemLedger ";
            query += " where ItemNo = @ItemNo and EntryDate <= @EntryDate   group by BatchEntryNo, ItemNo having sum(Quantity) > 0";
            Dictionary<string, Object> parameter = new Dictionary<string, object>();
            parameter.Add("@EntryDate", il.EntryDate);
            parameter.Add("@ItemNo", il.ItemNo);
            DataTable dt = DbFunctions.GetDataTable(query, parameter);
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToSingle(dr["Quantity"]) >= il.Quantity)
                {
                    il.UnitCostApplied = Convert.ToSingle(dr["CostAmount"]) / Convert.ToSingle(dr["Quantity"]);
                    il.CostValueApplied = il.Quantity * il.UnitCostApplied;
                    return true;
                }
            }
            return false;
        }

        public static int GetMaxEntryNO()
        {
            string query = "Select Max(EntryNo) from ItemLedger";
            object eno = DbFunctions.GetAValue(query);
            return eno == DBNull.Value ? 1 : Convert.ToInt32(eno)+1;

        }

        public static bool AdjustCostGL(DateTime StartDate)
        {
            List<ItemLedger> ile = GetItemLedgerEntries(StartDate);
            var purchasentries = ile.Where(a => a.EntryType == "Purchase").GroupBy(a => new { a.DocumentNo, a.EntryDate }).Select(a => new { Document = a.Key.DocumentNo, EntrDate = a.Key.EntryDate, Amount = a.Sum(b => b.CostValueApplied) });
            var salesentries = ile.Where(a => a.EntryType == "Sales").GroupBy(a => new { a.DocumentNo, a.EntryDate }).Select(a => new { Document = a.Key.DocumentNo, EntrDate = a.Key.EntryDate, Amount = a.Sum(b => b.CostValueApplied) });
            var retailentries = ile.Where(a => a.EntryType == "Retail").GroupBy(a => new { a.DocumentNo, a.EntryDate }).Select(a => new { Document = a.Key.DocumentNo, EntrDate = a.Key.EntryDate, Amount = a.Sum(b => b.CostValueApplied) });
            string postingsetupquery = "Select * from SYS_SETUP";
            try
            {
                TbLedgersDB tb = new TbLedgersDB(); ;
                DataTable dt = DbFunctions.GetDataTable(postingsetupquery);
                foreach (var pe in purchasentries)
                {
                    Class.Transactions tr = new Class.Transactions();
                    tr.ACCID = dt.Rows[0]["PurchaseLedger"].ToString();
                    tr.ACCNAME = tb.GetLedgerName(tr.ACCID);
                    tr.VOUCHERNO = pe.Document;
                    tr.VOUCHERTYPE = "Adjust Cost";
                    tr.DATED = pe.EntrDate.ToString();
                    tr.BRANCH = dt.Rows[0]["Current_Branch"].ToString();
                    tr.DEBIT = "0";
                    tr.CREDIT = pe.Amount.ToString();
                    tr.NARRATION = "Purchase " + tr.BRANCH + " "+ tr.VOUCHERNO;
                    tr.PARTICULARS = "Purchase " + tr.BRANCH + " " + tr.VOUCHERNO;
                    tr.insertTransaction();

                    tr.ACCID = dt.Rows[0]["InventoryLedger"].ToString();
                    tr.ACCNAME = tb.GetLedgerName(tr.ACCID);
                    tr.VOUCHERNO = pe.Document;
                    tr.VOUCHERTYPE = "Adjust Cost";
                    tr.DATED = pe.EntrDate.ToString();
                    tr.BRANCH = dt.Rows[0]["Current_Branch"].ToString();
                    tr.DEBIT = pe.Amount.ToString(); 
                    tr.CREDIT = "0";
                    tr.NARRATION = "Purchase " + tr.BRANCH + " " + tr.VOUCHERNO;
                    tr.PARTICULARS = "Purchase " + tr.BRANCH + " " + tr.VOUCHERNO;
                    tr.insertTransaction();

                }
                foreach (var se in salesentries)
                {
                    Class.Transactions tr = new Class.Transactions();
                    tr.ACCID = dt.Rows[0]["InventoryLedger"].ToString();
                    tr.ACCNAME = tb.GetLedgerName(tr.ACCID);
                    tr.VOUCHERNO = se.Document;
                    tr.VOUCHERTYPE = "Adjust Cost";
                    tr.DATED = se.EntrDate.ToString();
                    tr.BRANCH = dt.Rows[0]["Current_Branch"].ToString();
                    tr.DEBIT = "0";
                    tr.CREDIT = se.Amount.ToString();
                    tr.NARRATION = "Sales " + tr.BRANCH + " " + tr.VOUCHERNO;
                    tr.PARTICULARS = "Sales " + tr.BRANCH + " " + tr.VOUCHERNO;
                    tr.insertTransaction();

                    tr.ACCID = dt.Rows[0]["COGSLedger"].ToString();
                    tr.ACCNAME = tb.GetLedgerName(tr.ACCID);
                    tr.VOUCHERNO = se.Document;
                    tr.VOUCHERTYPE = "Adjust Cost";
                    tr.DATED = se.EntrDate.ToString();
                    tr.BRANCH = dt.Rows[0]["Current_Branch"].ToString();
                    tr.DEBIT = se.Amount.ToString();
                    tr.CREDIT = "0";
                    tr.NARRATION = "Sales " + tr.BRANCH + " " + tr.VOUCHERNO;
                    tr.PARTICULARS = "Sales " + tr.BRANCH + " " + tr.VOUCHERNO;
                    tr.insertTransaction();


                }

                foreach (var re in retailentries)
                {
                    Class.Transactions tr = new Class.Transactions();
                    tr.ACCID = dt.Rows[0]["InventoryLedger"].ToString();
                    tr.ACCNAME = tb.GetLedgerName(tr.ACCID);
                    tr.VOUCHERNO = re.Document;
                    tr.VOUCHERTYPE = "Adjust Cost";
                    tr.DATED = re.EntrDate.ToString();
                    tr.BRANCH = dt.Rows[0]["Current_Branch"].ToString();
                    tr.DEBIT = "0";
                    tr.CREDIT = re.Amount.ToString();
                    tr.NARRATION = "Sales " + tr.BRANCH + " " + tr.VOUCHERNO;
                    tr.PARTICULARS = "Sales " + tr.BRANCH + " " + tr.VOUCHERNO;
                    tr.insertTransaction();

                    tr.ACCID = dt.Rows[0]["COGSLedger"].ToString();
                    tr.ACCNAME = tb.GetLedgerName(tr.ACCID);
                    tr.VOUCHERNO = re.Document;
                    tr.VOUCHERTYPE = "Adjust Cost";
                    tr.DATED = re.EntrDate.ToString();
                    tr.BRANCH = dt.Rows[0]["Current_Branch"].ToString();
                    tr.DEBIT = re.Amount.ToString();
                    tr.CREDIT = "0";
                    tr.NARRATION = "Sales " + tr.BRANCH + " " + tr.VOUCHERNO;
                    tr.PARTICULARS = "Sales " + tr.BRANCH + " " + tr.VOUCHERNO;
                    tr.insertTransaction();


                }

                foreach(ItemLedger il in ile)
                {
                    Update(il);
                }

            }
            catch (Exception)
            {

                throw;
            }
            

            return true;
        }


        public static  bool Insert(ItemLedger ile)
        {
            string query = "Insert Into ItemLedger (EntryNo, EntryType, EntryDate,  DocumentNo, Branch, ItemNo, UOM, UOMQuantity, Quantity, UnitcostApplied, CostValueApplied, BatchEntryNo, CostAdjusted ";
            if (ile.SupplierBatch != null)
            {
                query += ", SupplierBatch";
            }
            if(ile.ExpiryDate > new DateTime(1002, 1, 1))
            {
                query += ", ExpiryDate";
            }
            query += ") ";

            query += " values ( @EntryNo, @EntryType, @EntryDate,  @DocumentNo, @Branch, @ItemNo, @UOM, @UOMQuantity, @Quantity, @UnitcostApplied, @CostValueApplied, @BatchEntryNo, 0 ";
            if (ile.SupplierBatch != null)
            {
                query += ",  @SuplierBatch";
            }
            if (ile.ExpiryDate > new DateTime(1002,1,1))
            {
                query += ",  @ExpiryDate";
            }

            query += ")";
            
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@EntryNo", ile.EntryNo);
            parameters.Add("@EntryType", ile.EntryType);
            parameters.Add("@EntryDate", ile.EntryDate);
            parameters.Add("@DocumentNo", ile.DocumentNo);
            parameters.Add("@Branch", ile.Branch);
            parameters.Add("@ItemNo", ile.ItemNo);
            parameters.Add("@UOM", ile.UOM);
            parameters.Add("@UOMQuantity", ile.UOMQuantity);
            parameters.Add("@Quantity", ile.Quantity);
            parameters.Add("@UnitCostApplied", ile.UnitCostApplied);
            parameters.Add("@CostValueApplied", ile.CostValueApplied);
            parameters.Add("@BatchEntryNo", ile.BatchEntryNo);
            if(ile.SupplierBatch != null)
            parameters.Add("@SupplierBatch", ile.SupplierBatch);
            if (ile.ExpiryDate > new DateTime(1002, 1, 1))
                parameters.Add("@ExpiryDate", ile.ExpiryDate);
            
            if (DbFunctions.InsertUpdate(query, parameters) >= 1)
                return true;
            return false;
                
        }
        private static bool Update(ItemLedger ile)
        {
            string query = "Update ItemLedger set CostAdjusted = 1 where EntryNo = @EntryNo ";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@EntryNo", ile.EntryNo);
            return DbFunctions.InsertUpdate(query, parameter) >= 1 ? true: false;


            return true;
        }
       

        private static List<ItemLedger> GetItemLedgerEntries(DateTime startdate)
        {
            string query = "Select * from ItemLedger where EntryDate >= @EntryDate and CostAdjusted = 1 ";
            Dictionary<string, Object> parameter = new Dictionary<string, object>();

            parameter.Add("@EntryDate", startdate);
            DataTable dt = DbFunctions.GetDataTable(query, parameter);
            List<ItemLedger> ile = new List<ItemLedger>();
            foreach (DataRow dr in dt.Rows)
            {
                ItemLedger il = new ItemLedger();
                il.Branch = dr["Branch"].ToString();
                il.EntryType = dr["EntryType"].ToString();
                il.EntryNo = Convert.ToInt16(dr["EntryNo"]);
                il.EntryDate = Convert.ToDateTime(dr["EntryDate"]);
                il.ItemNo = dr["ItemNo"].ToString();
                il.UOM = dr["UOM"].ToString();
                il.UOMQuantity = Convert.ToInt16(dr["UOMQuantity"]);
                il.Quantity = Convert.ToInt16(dr["Quantity"]);
                il.BatchEntryNo = Convert.ToInt16(dr["BatchEntryNo"]);
                ile.Add(il);
            }
            return ile;
        }



    }
}
