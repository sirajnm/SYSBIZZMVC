using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory.Class
{
    class CollectionDay
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();

        public string Id { get; set; }
        public string SupId { get; set; }
        public string Repeat { get; set; }
        public string RepeatMode { get; set; }
        public string Day { get; set; }
        public string Date { get; set; }

        public void AddCollection()
        {
            string Query = "insert into Tbl_CollectionDay(SupId, Repeat, RepeatMode, Day, Date) values(@SupId, @Repeat, @RepeatMode, @Day, @Date)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@SupId", SupId);
            parameters.Add("@Repeat", Repeat);
            parameters.Add("@RepeatMode", RepeatMode);
            parameters.Add("@Day", Day);
            parameters.Add("@Date", Date);
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void AddCollectionCus()
        {
            
            string Query = "insert into Tbl_CollectionDay_Cus(CusId, Repeat, RepeatMode, Day, Date) values(@SupId, @Repeat, @RepeatMode, @Day, @Date)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@SupId", SupId);
            parameters.Add("@Repeat", Repeat);
            parameters.Add("@RepeatMode", RepeatMode);
            parameters.Add("@Day", Day);
            parameters.Add("@Date", Date);
            DbFunctions.InsertUpdate(Query, parameters);
        }
        
        public void DeleteCollection()
        {
            
            string Query = "Delete from Tbl_CollectionDay where SupId=@SupId";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@SupId", SupId);
            DbFunctions.InsertUpdate(Query, parameters);
        }
        public void DeleteCollection_cus()
        {
            
            string Query = "Delete from Tbl_CollectionDay_Cus where CusId=@SupId";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@SupId", SupId);
            DbFunctions.InsertUpdate(Query, parameters);
        }
        public DataTable GetSupplierDetails(string SupplierId)
        {
            
            string Query = " SELECT     Id, SupId, Repeat, RepeatMode, Day, Date FROM         Tbl_CollectionDay WHERE     (SupId = @SupId)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@SupId", SupplierId);
           return DbFunctions.GetDataTable(Query, parameters);
        }
        public DataTable GetCusDetails(string SupplierId)
        {
            
            string Query = " SELECT     Id, CusId, Repeat, RepeatMode, Day, Date FROM         Tbl_CollectionDay_Cus WHERE     (CusId = @SupId)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@SupId", SupplierId);
            return DbFunctions.GetDataTable(Query, parameters);
        }
    }
}
