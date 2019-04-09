using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Class
{
    public class PiceList
    {
        //public string itemCode { get; set; }

        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();

        public DataTable getItemPriceList(string itemCode)
        {
            DataTable dt = new DataTable();
            string query = "SELECT SAL_TYPE,PRICE FROM INV_ITEM_PRICE WHERE ITEM_CODE=@itemCode";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@itemCode", itemCode);
            Model.DbFunctions.GetDataTable(query, Parameter);
            return dt;                       
        }
    }
}
