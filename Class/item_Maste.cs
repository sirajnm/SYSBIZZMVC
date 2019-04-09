using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Class
{
   public class item_Maste
    {
        //private static SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private static SqlCommand cmd = new SqlCommand();
        public string CODE { get; set; }
        public string DESC_ENG { get; set; }
        public string DESC_ARB { get; set; }
        public string TYPE { get; set; }
        public string GROUP { get; set; }
        public string CATEGORY { get; set; }
        public string TRADEMARK { get; set; }
        public string Supplier { get; set; }
        public decimal COST_PRICE { get; set; }
        public decimal SALE_PRICE { get; set; }
        public int MINIMUM_QTY { get; set; }
        public int MAXIMUM_QTY { get; set; }
        public int REORDER_QTY { get; set; }
        public string IN_ACTIVE { get; set; }
        public int TaxId { get; set; }
        public string LOCATION { get; set; }
        public decimal HASSERIAL { get; set; }
        public decimal HASWARRENTY { get; set; }
        public decimal PERIOD { get; set; }
        public string PERIODTYPE { get; set; }
        public string ID { get; set; }
        public string hsn { get; set; }
        public string productType { get; set; }

        string query;
        //purchase and sales
        public string ITEM_DESC_ENG { get; set; }


        public void update_item_master(item_Maste values, string table)
        { 
            query = "UPDATE_ITEM_MASTER";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@CODE", values.CODE);
            Parameter.Add("@DESC_ENG", values.DESC_ENG);
            Parameter.Add("@DESC_ARB", values.DESC_ARB);
            Parameter.Add("@TYPE", values.TYPE);
            Parameter.Add("@GROUP", values.GROUP);
            Parameter.Add("@CATEGORY", values.CATEGORY);
            Parameter.Add("@TRADEMARK", values.TRADEMARK);
            Parameter.Add("@SUPPLIER", values.Supplier);
            Parameter.Add("@COST_PRICE", values.COST_PRICE);
            Parameter.Add("@SALE_PRICE", values.SALE_PRICE);
            Parameter.Add("@MINIMUM_QTY", values.MINIMUM_QTY);
            Parameter.Add("@MAXIMUM_QTY", values.MAXIMUM_QTY);
            Parameter.Add("@REORDER_QTY", values.REORDER_QTY);
            Parameter.Add("@IN_ACTIVE", values.IN_ACTIVE);
            Parameter.Add("@TaxId", values.TaxId);
            Parameter.Add("@LOCATION", values.LOCATION);
            Parameter.Add("@HASSERIAL", values.HASSERIAL);
            Parameter.Add("@HASWARRENTY", values.HASWARRENTY);
            Parameter.Add("@PERIOD", values.PERIOD);          
            Parameter.Add("@PERIODTYPE", values.PERIODTYPE);            
            Parameter.Add("@ID", values.ID);
            Parameter.Add("@ITEM_DESC_ENG", values.ITEM_DESC_ENG);
            Parameter.Add("@op", table);
            Parameter.Add("@hsn", values.hsn);
            Parameter.Add("@pType", values.productType);
            DbFunctions.InsertUpdateProcedure(query, Parameter);
        }

        public DataTable getitem(string name, string rate_type)
        {
            query = "SELECT        INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name], INV_ITEM_DIRECTORY.DESC_ARB,INV_ITEM_DIRECTORY.PERIODTYPE,INV_ITEM_DIRECTORY.PERIOD, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, GEN_TAX_MASTER.TaxRate, INV_ITEM_PRICE.PRICE AS SALES, ISNULL(INV_ITEM_PRICE_1.PRICE * GEN_TAX_MASTER.TaxRate / 100 + INV_ITEM_PRICE_1.PRICE, 0) AS COST, INV_ITEM_PRICE_2.PRICE AS PROMO, INV_ITEM_PRICE_3.PRICE AS WHOLESALE, INV_ITEM_PRICE_4.PRICE AS MRP, INV_ITEM_TYPE.DESC_ENG AS TYPE, INV_ITEM_CATEGORY.DESC_ENG AS CATEGORY, INV_ITEM_GROUP.DESC_ENG AS [GROUP], INV_ITEM_TM.DESC_ENG AS TRADEMARK, INV_ITEM_DIRECTORY.TaxId, INV_ITEM_DIRECTORY.HASSERIAL FROM            INV_ITEM_PRICE AS INV_ITEM_PRICE_4 LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_2 INNER JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE_2.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND INV_ITEM_PRICE_2.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN  INV_ITEM_PRICE AS INV_ITEM_PRICE_3 ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_3.ITEM_CODE AND INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_3.UNIT_CODE ON INV_ITEM_PRICE_4.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND INV_ITEM_PRICE_4.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE RIGHT OUTER JOIN INV_ITEM_PRICE ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE RIGHT OUTER JOIN INV_ITEM_DIRECTORY LEFT OUTER JOIN GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE LEFT OUTER JOIN INV_ITEM_TYPE ON INV_ITEM_DIRECTORY.TYPE=INV_ITEM_TYPE.CODE LEFT  OUTER JOIN INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY=INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP]=INV_ITEM_GROUP.CODE LEFT OUTER JOIN INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK=INV_ITEM_TM.CODE where INV_ITEM_DIRECTORY.DESC_ENG like '" + name + "%' and INV_ITEM_PRICE.SAL_TYPE='" + rate_type + "'";
            DataTable dt = new DataTable();
            DbFunctions.GetDataTable(query);
            return dt;
        }   
    }
}
