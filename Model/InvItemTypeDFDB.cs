using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class InvItemTypeDFDB
    {
        string itemCode, salType, unitCode, batchId, branch;

        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }

        public string BatchId
        {
            get { return batchId; }
            set { batchId = value; }
        }

        public string UnitCode
        {
            get { return unitCode; }
            set { unitCode = value; }
        }

        public string SalType
        {
            get { return salType; }
            set { salType = value; }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        decimal price;

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

        public SqlDataReader GetDatabyItemCode()
        {
            string query = "SELECT UNIT_CODE,SAL_TYPE,PRICE FROM INV_ITEM_PRICE_DF WHERE ITEM_CODE = '" + itemCode + "'";
            return DbFunctions.GetDataReader(query);
        }
    }
}
