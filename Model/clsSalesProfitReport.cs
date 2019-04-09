using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory.Model
{
    class clsSalesProfitReport
    {
        private string startDate;
        private string endDate;
       
        private DateTime toDate;
        private DateTime fromDate;

        public DateTime FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }
        public DateTime ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }
        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public string StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DataTable getSysSetup()
        {
            string Query = "SELECT * FROM SYS_SETUP";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable getItemDetails()
        {
            string Query = "SELECT  INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],CODE FROM   INV_ITEM_DIRECTORY ";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable getPurchasePrice()
        {
            string Query = "VAR_PURCHASE_COST";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Op", "s2");
            parameters.Add("@Date1",fromDate);
            parameters.Add("@Date2", toDate);
            return DbFunctions.GetDataTableProcedure(Query,parameters);
        }
        public DataTable getLastPurchasePrice()
        {
            string Query = "VAR_LASTPURCHASE_COST";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Op", "s2");
            parameters.Add("@Date1", fromDate);
            parameters.Add("@Date2", toDate);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }
        public DataTable getLastPurchaseRate()
        {
            string Query = "VAR_LASTPURCHASE_RATE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Op", "s2");
            parameters.Add("@Date1", fromDate);
            parameters.Add("@Date2", toDate);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }
        public DataTable getLastSalesRate()
        {
            string Query = "VAR_LASTSALES_RATE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Op", "s2");
            parameters.Add("@Date1", fromDate);
            parameters.Add("@Date2", toDate);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }
        public DataTable getLastSalesCost()
        {
            string Query = "VAR_LASTSALES_COST";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Op", "s2");
            parameters.Add("@Date1", fromDate);
            parameters.Add("@Date2", toDate);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }
        public DataTable getPurchasePriceWthoutDate()
        {
            string Query = "VAR_PURCHASE_COST";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Op", "s1");
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }
        public DataTable getLastPurchaseCostWthoutDate()
        {
            string Query = "VAR_LASTPURCHASE_COST";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Op", "s1");
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }
        public DataTable getLastPurchaseRateWthoutDate()
        {
            string Query = "VAR_LASTPURCHASE_RATE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Op", "s1");
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }
        public DataTable getLastSalesRateWthoutDate()
        {
            string Query = "VAR_LASTSALES_RATE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Op", "s1");
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }
        public DataTable getLastSalesCostWthoutDate()
        {
            string Query = "VAR_LASTSALES_COST";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Op", "s1");
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }
    }
}
