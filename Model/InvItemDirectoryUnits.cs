using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class InvItemDirectoryUnits
    {
       private  string itemCode, branch, store, barcode, unitcode;
       private string invItemSalePriceType;

        public InvItemDirectoryUnits()
        {

        }

        public InvItemDirectoryUnits(string barcode)
        {
            string query = "select ITEM_CODE,BARCODE,UNIT_CODE,PACK_SIZE from [INV_ITEM_DIRECTORY_UNITS] WHERE barcode = '" + barcode + "'";

            DataTable dt = DbFunctions.GetDataTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ItemCode = dr["Item_code"].ToString();
                    Barcode = dr["Barcode"].ToString();
                    Unitcode = dr["Unit_code"].ToString();
                    PackSize = Convert.ToSingle(dr["Pack_Size"]);
                }
            }
            else
            {
                ItemCode = "";
                Barcode = "";
                Unitcode = "";
                PackSize = 0;
            }



        }


        public string InvItemSalePriceType
        {
            get { return invItemSalePriceType; }
            set { invItemSalePriceType = value; }
        }

        public string Unitcode
        {
            get { return unitcode; }
            set { unitcode = value; }
        }

        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
        }

        public string Store
        {
            get { return store; }
            set { store = value; }
        }

        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        float packSize;

        public float PackSize
        {
            get { return packSize; }
            set { packSize = value; }
        }
        int fsort;

        public int Fsort
        {
            get { return fsort; }
            set { fsort = value; }
        }
        public float GetUOM()
        {
            String query = "SELECT PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + itemCode + "' and UNIT_CODE = '"+ Unitcode+ "'  ";
            object a = DbFunctions.GetAValue(query);
            return a != null ? Convert.ToSingle(a): 0 ;
        }
        public SqlDataReader GetDataByItemCode()
        {
            string query = "SELECT UNIT_CODE,PACK_SIZE,BARCODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + itemCode + "'";
            return DbFunctions.GetDataReader(query);
        }

        public void Insert()
        {
            string query = "INSERT INTO INV_ITEM_DIRECTORY_UNITS(ITEM_CODE,BARCODE,UNIT_CODE,PACK_SIZE) VALUES('" + itemCode + "','" + barcode + "','" + unitcode + "','" + packSize + "')";
            DbFunctions.InsertUpdate(query);
        }

        public DataTable UnitPacksizeSales()
        {
            string query = "SELECT UNIT_CODE, PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + itemCode + "'";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable DetailsBySaleType()
        {
            string query=" SELECT INV_ITEM_DIRECTORY.DESC_ENG AS [ITEM_NAME],BATCH.[PRICE BATCH], INV_ITEM_PRICE.ITEM_CODE, INV_ITEM_PRICE.SAL_TYPE,INV_ITEM_PRICE.PRICE, INV_ITEM_PRICE.UNIT_CODE  FROM INV_ITEM_DIRECTORY  INNER JOIN (select  CONVERT(varchar, MAX(batch_increment)) id,MAX(batch_id) [PRICE BATCH],Item_id from tblstock group by Item_id ) AS BATCH  ON INV_ITEM_DIRECTORY.CODE=BATCH.Item_id LEFT OUTER JOIN INV_ITEM_PRICE ON BATCH.[PRICE BATCH]=INV_ITEM_PRICE.BATCH_ID AND INV_ITEM_PRICE.SAL_TYPE='" + invItemSalePriceType + "' ";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getPrice()
        {
            string query = "SELECT PRICE FROM INV_ITEM_PRICE WHERE SAL_TYPE = '" + invItemSalePriceType + "' AND ITEM_CODE='" + itemCode + "' AND BATCH_ID='" + branch + "'";
            return DbFunctions.GetDataTable(query);
        }
    }
}
