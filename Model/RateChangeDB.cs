using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class RateChangeDB
    {
        int rId;

        public int RId
        {
            get { return rId; }
            set { rId = value; }
        }
        string itemCode, reference;

        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        decimal price, salePrice, qty;

        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public decimal SalePrice
        {
            get { return salePrice; }
            set { salePrice = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        bool status;

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        public SqlDataReader BatchByItemCode()
        {
            string query = "select max(R_id)as batch FROM RateChange Where Item_code='" + itemCode + "'";
            return DbFunctions.GetDataReader(query);
        }

        public void Insert()
        {
            string query = "INSERT INTO RateChange(Item_code,datee,Price,Sale_Price, Qty) VALUES('" + itemCode + "','" + date + "','" + price + "','" + salePrice + "','" + Convert.ToDecimal(qty) + "' )";
            DbFunctions.InsertUpdate(query);
        }

        public DataTable PriceQtyByRid()
        {
            string query = "select Price,Qty from RateChange where R_Id='" + rId + "'";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable PriceQtyByItemId()
        {

            string query = "select Price,Qty,R_Id from RateChange where Item_code='" + itemCode + "'";
            return DbFunctions.GetDataTable(query);
        }
    }
}
