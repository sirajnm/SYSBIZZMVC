using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Class
{
    class Cls_additem_da
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand query = );
        Cls_additem_ba ba = new Cls_additem_ba();
        Login lg = new Login();
        string query;
        public void additem(Cls_additem_ba ba)
        {
            query = "INSERT INTO INV_ITEM_DIRECTORY(CODE,DESC_ENG,SALE_PRICE,TaxId,IN_ACTIVE) values(@cd,@name,@price,@tax,'Y')";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@cd", ba.code);
            Parameter.Add("@name", ba.name);
            Parameter.Add("@price", ba.sr);
            Parameter.Add("@tax", ba.tax);
            DbFunctions.InsertUpdate(query, Parameter);
        }

        public void addprice(Cls_additem_ba ba)
        {
            for (int i = 0; i <= 4; i++)
            {
                string s = "tla";
                string[] arr = { "RTL", "WHL", "PUR", "MRP", "PMS" };
                decimal[] ar = { ba.rr, ba.wr, ba.pr, ba.mrp, ba.pum };
                query = "INSERT INTO INV_ITEM_PRICE(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH) values(@item_code,@sale_type,@price,@unit_code,@batch_id,@branch)";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@item_code", ba.code);
                Parameter.Add("@sale_type", arr[i]);
                Parameter.Add("@price", ar[i]);
                Parameter.Add("@unit_code", ba.unit);
                Parameter.Add("@batch_id", ba.price_batch);
                Parameter.Add("@branch", s);
                DbFunctions.InsertUpdate(query, Parameter);
            }            
        }
        public void addprice_DF(Cls_additem_ba ba)
        {
            for (int i = 0; i <= 4; i++)
            {
                string s = "tla";
                string[] arr = { "RTL", "WHL", "PUR", "MRP", "PMS" };
                decimal[] ar = { ba.rr, ba.wr, ba.pr, ba.mrp, ba.pum };
                query = "INSERT INTO INV_ITEM_PRICE_DF(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH) values(@item_code,@sale_type,@price,@unit_code,@batch_id,@branch)";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@item_code", ba.code);
                Parameter.Add("@sale_type", arr[i]);
                Parameter.Add("@price", ar[i]);
                Parameter.Add("@unit_code", ba.unit);
                Parameter.Add("@batch_id", ba.price_batch);
                Parameter.Add("@branch", s);
                DbFunctions.InsertUpdate(query, Parameter);

            }
        }
        public void addunit(Cls_additem_ba ba)
        {
            string s = "tla";
            query = "INSERT INTO INV_ITEM_DIRECTORY_UNITS(ITEM_CODE,BRANCH,BARCODE,UNIT_CODE,PACK_SIZE) values(@cd,@brnch,@br,@uni,1)";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@cd", ba.code);
            Parameter.Add("@brnch", s);
            Parameter.Add("@br", ba.brcode);
            Parameter.Add("@uni", ba.unit);
            DbFunctions.InsertUpdate(query, Parameter);
        }
    }
}
