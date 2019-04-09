using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class RawMaterials
    {
        string mfgId, rawId;

        public string RawId
        {
            get { return rawId; }
            set { rawId = value; }
        }

        public string MfgId
        {
            get { return mfgId; }
            set { mfgId = value; }
        }
        decimal mfgQty, rawQty;

        public decimal RawQty
        {
            get { return rawQty; }
            set { rawQty = value; }
        }

        public decimal MfgQty
        {
            get { return mfgQty; }
            set { mfgQty = value; }
        }


        public int Insert()
        {
            string query = "INSERT INTO MFG_RAWMATERIAL(MFG_ID,MFG_QTY ,RAW_ID, RAW_QTY) VALUES('" + MfgId + "','" + MfgQty + "','" + RawId + "','" + RawQty + "')";
            return DbFunctions.InsertUpdate(query);
        }

        public bool DeleteEntry()
        {
            string query = "Delete From MFG_RAWMATERIAL where MFG_ID='" + MfgId + "'";
            int row = DbFunctions.InsertUpdate(query);
            if (row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ExistingMfg()
        {
            string query = "Select * From MFG_RAWMATERIAL where MFG_ID='" + MfgId + "'";
            DataTable dt = DbFunctions.GetDataTable(query);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public DataTable ExistingMfgByID()
        {
            string query = "Select MFG_ID,MFG_QTY,RAW_ID,INV_ITEM_DIRECTORY.DESC_ENG,RAW_QTY From MFG_RAWMATERIAL INNER JOIN INV_ITEM_DIRECTORY ON RAW_ID=CODE where MFG_ID='" + MfgId + "'";
            DataTable dt = DbFunctions.GetDataTable(query);
            return dt;
        }

        public DataTable StockDtl()
        {
            string query = "SELECT Item_id,batch_id,Qty,UNIT_CODE ,Cost_price,batch_increment FROM tblStock inner join INV_ITEM_DIRECTORY_UNITS on ITEM_CODE=Item_id where Item_id='" + rawId + "' and Qty>0 order by batch_increment";
            //string query = "SELECT Item_id,batch_id,Qty,UNIT_CODE ,Cost_price,batch_increment FROM tblStock inner join INV_ITEM_DIRECTORY_UNITS on ITEM_CODE=Item_id where Item_id='" + rawId + "' order by batch_increment";
            DataTable dt = DbFunctions.GetDataTable(query);
            return dt;
        }

        public Double FinalStock()
        {
            string query = "SELECT Sum(Qty) as Qty FROM tblStock where Item_id='"+rawId+"' group by Item_id";
            return Convert.ToDouble(DbFunctions.GetAValue(query));
        }

    }
}
