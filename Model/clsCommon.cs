using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory.Model
{
    class clsCommon
    {
        private string code;
        private string id;
        private string desc_eng;
        private string desc_arb;
        private string tblName;
        public string TblName
        {
            get { return tblName; }
            set { tblName = value; }
        }
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Desc_arb
        {
            get { return desc_arb; }
            set { desc_arb = value; }
        }
        public string Desc_eng
        {
            get { return desc_eng; }
            set { desc_eng = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public void add()
        {
            string Query = "INSERT INTO " + tblName + "(CODE,DESC_ENG,DESC_ARB)" +
                           "VALUES ('" + code + "','" + desc_eng + "','" + desc_arb + "')";
            DbFunctions.InsertUpdate(Query);
        }
        public void Update()
        {
            string Query = "UPDATE " + tblName + " SET CODE = '" + code + "',DESC_ENG = '" + desc_eng + "',DESC_ARB = '" + desc_arb + "' WHERE CODE = '" + id + "'";
            DbFunctions.InsertUpdate(Query);
        }
        public void Delete()
        {
            string Query = "DELETE FROM " + tblName + " WHERE CODE = '" + id + "'";
            DbFunctions.InsertUpdate(Query);
        }
        public DataTable Select()
        {
            string Query = "SELECT  CODE from " + tblName + " where CODE='" + code + "'";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable SelectWithoutAr()
        {
            string Query = " SELECT  CODE,DESC_ENG FROM " + tblName;
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable SelectWithAr()
        {
            string Query = "SELECT  * FROM " + tblName;
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable BindSales_Warranty(string serial)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT INV_SALES_HDR.DOC_NO, INV_SALES_HDR.DOC_TYPE as 'Doc Type',INV_SALES_HDR.DOC_DATE_GRE as Date, REC_CUSTOMER.DESC_ENG as Customer, INV_SALES_HDR.TAX_TOTAL as 'Tax Total', INV_SALES_HDR.DISCOUNT, INV_SALES_HDR.NET_AMOUNT as 'Net Amount',INV_SALES_DTL.ITEMCODE FROM            INV_SALES_DTL LEFT OUTER JOIN INV_SALES_HDR ON INV_SALES_DTL.DOC_NO = INV_SALES_HDR.DOC_NO LEFT OUTER JOIN REC_CUSTOMER ON INV_SALES_HDR.CUSTOMER_CODE = REC_CUSTOMER.CODE WHERE        (INV_SALES_DTL.SERIALNO = '" + serial + "')";
                dt = DbFunctions.GetDataTable(query);
                return dt;
            }
            catch
            {
                return dt;
            }
        }

        public DataTable BindPurchase_Warranty(string serial)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT INV_PURCHASE_HDR.DOC_NO, INV_PURCHASE_HDR.DOC_TYPE as 'Doc Type', PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, INV_PURCHASE_HDR.DOC_DATE_HIJ as 'Hij Date', INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Total', INV_PURCHASE_HDR.GROSS as 'Gross Total', INV_PURCHASE_DTL.SERIALNO,INV_PURCHASE_DTL.ITEMCODE FROM            INV_PURCHASE_DTL LEFT OUTER JOIN  INV_PURCHASE_HDR ON INV_PURCHASE_DTL.DOC_NO = INV_PURCHASE_HDR.DOC_NO LEFT OUTER JOIN  PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE        (INV_PURCHASE_DTL.SERIALNO = '" + serial + "')";
                dt = DbFunctions.GetDataTable(query);
                return dt;
            }
            catch
            {
                return dt;
            }
        }

        public SqlDataReader GetItemWarranty(string item_code)
        {
            SqlDataReader Dr = null;
            try
            {
                string itemcode = item_code;
                string query = "SELECT PERIOD,PERIODTYPE,HASWARRENTY FROM INV_ITEM_DIRECTORY WHERE CODE='" + itemcode + "'";
                Dr = DbFunctions.GetDataReader(query);
                return Dr;
            }
            catch
            {
                return Dr;
            }
        }

        public string TaxType()
        {
            string query = "SELECT TaxType FROM SYS_SETUP";
            return (Convert.ToString(DbFunctions.GetAValue(query)));
        }

        public SqlDataReader GetTaxRate(int TaxId)
        {
            string query = "SELECT TaxRate from GEN_TAX_MASTER where TaxId=" + TaxId;
            return DbFunctions.GetDataReader(query);
        }


        public string PriceByItem(string itemcode)
        {
            string query = "select PRICE FROM INV_ITEM_PRICE WHERE SAL_TYPE='PUR' AND ITEM_CODE='" + itemcode + "'";
            return (string)DbFunctions.GetAValue(query);
        }

        public bool TaxEnable()
        {
            string query = "SELECT TAX FROM SYS_SETUP";
            return (bool)DbFunctions.GetAValue(query);
        }

        public DataTable getAllFromTable(string sheetName)
        {
            string query = "SELECT * From [" + sheetName + "]";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable GetCurrency(string ccode)
        {
            string query = "SELECT * FROM GEN_CURRENCY WHERE CODE='" + ccode + "'";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable GetSalesMan()
        {
            string query = "SELECT EMPID,CONCAT(EMP_FNAME,' ',EMP_MNAME,' ',EMP_LNAME)as name from EMP_EMPLOYEES WHERE EMP_DESIG=21";
            return DbFunctions.GetDataTable(query);
        }

        string item_name, category, trade_mark, group, custcode, salemancode;

        public string Custcode
        {
            get { return custcode; }
            set { custcode = value; }
        }

        public string Salemancode
        {
            get { return salemancode; }
            set { salemancode = value; }
        }
        DateTime startDate, endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        public string Trade_mark
        {
            get { return trade_mark; }
            set { trade_mark = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }

        public DataTable salesReport_date()
        {
            string query = "SELECT SALES.ITEM_CODE,INV_ITEM_DIRECTORY.DESC_ENG AS ITEM_NAME,SALES.QTY AS QUANTITY,SALES.[GROSS TOTAL],SALES.[TAX AMOUNT],SALES.[NET TOTAL] FROM INV_ITEM_DIRECTORY LEFT OUTER JOIN (SELECT ITEM_CODE,SUM(QUANTITY) AS QTY,SUM(GROSS_TOTAL) AS [GROSS TOTAL],SUM(ITEM_TAX) AS [TAX AMOUNT],SUM(ITEM_TOTAL) AS [NET TOTAL] FROM INV_SALES_DTL LEFT OUTER JOIN INV_SALES_HDR ON INV_SALES_DTL.DOC_ID=INV_SALES_HDR.DOC_ID WHERE (CONVERT(VARCHAR, INV_SALES_HDR.DOC_DATE_GRE,101) BETWEEN @d1 AND @d2)  GROUP BY ITEM_CODE) AS SALES ON INV_ITEM_DIRECTORY.CODE=SALES.ITEM_CODE LEFT OUTER JOIN INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY=INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP]=INV_ITEM_GROUP.CODE LEFT OUTER JOIN INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK=INV_ITEM_TM.CODE WHERE ITEM_CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%'";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@d1", startDate.ToShortDateString());
            parameter.Add("@d2", endDate.ToShortDateString());
            DataTable dt = DbFunctions.GetDataTable(query, parameter);
            var tqty = ((from s in dt.AsEnumerable()
                          select decimal.Parse(s["QUANTITY"].ToString())) as IEnumerable<decimal>).Sum();
            var total = ((from s in dt.AsEnumerable()
                            select decimal.Parse(s["GROSS TOTAL"].ToString())) as IEnumerable<decimal>).Sum();
            var ttax = ((from s in dt.AsEnumerable()
                         select decimal.Parse(s["TAX AMOUNT"].ToString())) as IEnumerable<decimal>).Sum();
            var tnet = ((from s in dt.AsEnumerable()
                         select decimal.Parse(s["NET TOTAL"].ToString())) as IEnumerable<decimal>).Sum();

            dt.Rows.Add(null, "Total", tqty, total, ttax, tnet);

            return dt ;
        }

        public DataTable salesReport_all()
        {
            string query = "SELECT SALES.ITEM_CODE,INV_ITEM_DIRECTORY.DESC_ENG AS ITEM_NAME,SALES.QTY as QUANTITY,SALES.[GROSS TOTAL],SALES.[TAX AMOUNT],SALES.[NET TOTAL] FROM INV_ITEM_DIRECTORY LEFT OUTER JOIN (SELECT ITEM_CODE,SUM(QUANTITY) AS QTY,SUM(GROSS_TOTAL) AS [GROSS TOTAL],SUM(ITEM_TAX) AS [TAX AMOUNT],SUM(ITEM_TOTAL) AS [NET TOTAL] FROM INV_SALES_DTL GROUP BY ITEM_CODE) AS SALES ON INV_ITEM_DIRECTORY.CODE=SALES.ITEM_CODE LEFT OUTER JOIN INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY=INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP]=INV_ITEM_GROUP.CODE LEFT OUTER JOIN INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK=INV_ITEM_TM.CODE WHERE ITEM_CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%'";
            DataTable dt = DbFunctions.GetDataTable(query);
            var tqty = ((from s in dt.AsEnumerable()
                         select decimal.Parse(s["QUANTITY"].ToString())) as IEnumerable<decimal>).Sum();
            var total = ((from s in dt.AsEnumerable()
                          select decimal.Parse(s["GROSS TOTAL"].ToString())) as IEnumerable<decimal>).Sum();
            var ttax = ((from s in dt.AsEnumerable()
                         select decimal.Parse(s["TAX AMOUNT"].ToString())) as IEnumerable<decimal>).Sum();
            var tnet = ((from s in dt.AsEnumerable()
                          select decimal.Parse(s["NET TOTAL"].ToString())) as IEnumerable<decimal>).Sum();

            dt.Rows.Add(null, "Total", tqty, total,ttax,tnet);

            return dt;
        }

        public decimal sum_sales_date()
        {
            string query = "SELECT SUM(SALES.TOTAL) FROM INV_ITEM_DIRECTORY LEFT OUTER JOIN (SELECT ITEM_CODE,SUM(QUANTITY) AS QTY,SUM(GROSS_TOTAL) AS TOTAL FROM INV_SALES_DTL LEFT OUTER JOIN INV_SALES_HDR ON INV_SALES_DTL.DOC_ID=INV_SALES_HDR.DOC_ID WHERE INV_SALES_HDR.DOC_DATE_GRE BETWEEN @d1 AND @d2 GROUP BY ITEM_CODE ) AS SALES ON INV_ITEM_DIRECTORY.CODE=SALES.ITEM_CODE LEFT OUTER JOIN INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY=INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP]=INV_ITEM_GROUP.CODE LEFT OUTER JOIN INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK=INV_ITEM_TM.CODE WHERE ITEM_CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%'";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@d1", startDate.ToShortDateString());
            parameter.Add("@d2", endDate.ToShortDateString());
            return Convert.ToDecimal(DbFunctions.GetAValue(query, parameter));
        }

        public decimal sum_sales_all()
        {
            string query = "SELECT SUM(SALES.TOTAL) FROM INV_ITEM_DIRECTORY LEFT OUTER JOIN (SELECT ITEM_CODE,SUM(QUANTITY) AS QTY,SUM(GROSS_TOTAL) AS TOTAL FROM INV_SALES_DTL GROUP BY ITEM_CODE ) AS SALES ON INV_ITEM_DIRECTORY.CODE=SALES.ITEM_CODE LEFT OUTER JOIN INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY=INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP]=INV_ITEM_GROUP.CODE LEFT OUTER JOIN INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK=INV_ITEM_TM.CODE WHERE ITEM_CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%'";
            return Convert.ToDecimal(DbFunctions.GetAValue(query));
        }

        public DataTable salesReport_CustSalesman_Date()
        {
            string query = "SELECT DOC_ID AS 'INVOICE NO',REC_CUSTOMER.DESC_ENG as 'CUSTOMER NAME',CAST(INV_SALES_HDR.DOC_DATE_GRE AS date) AS [DATE],convert(decimal(18,2),NET_AMOUNT) as NET_AMOUNT FROM INV_SALES_HDR LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.SALESMAN_CODE=" + salemancode + " AND INV_SALES_HDR.DOC_TYPE='SAL.CRD' AND INV_SALES_HDR.DOC_DATE_GRE BETWEEN @d1 AND @d2 AND INV_SALES_HDR.CUSTOMER_CODE LIKE '%" + custcode + "%' ORDER BY DOC_ID ASC";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@d1", startDate);
            parameter.Add("@d2", endDate);
            return DbFunctions.GetDataTable(query, parameter);
        }

        public DataTable salesReport_CustSalesman()
        {
            string query = "SELECT DOC_ID AS 'INVOICE NO',REC_CUSTOMER.DESC_ENG as 'CUSTOMER NAME',CAST(INV_SALES_HDR.DOC_DATE_GRE AS date) AS [DATE],convert(decimal(18,2),NET_AMOUNT) as NET_AMOUNT FROM INV_SALES_HDR LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.SALESMAN_CODE=" + salemancode + " AND INV_SALES_HDR.DOC_TYPE='SAL.CRD' AND INV_SALES_HDR.CUSTOMER_CODE LIKE '%" + custcode + "%' ORDER BY DOC_ID ASC";
            return DbFunctions.GetDataTable(query);
        }

        public bool IsCusSup()
        {
            string query = "SELECT  IsCusSup  FROM   SYS_SETUP";
            if (DbFunctions.GetAValue(query) != DBNull.Value || DbFunctions.GetAValue(query).ToString() != "")
            {
                return Convert.ToBoolean(DbFunctions.GetAValue(query));
            }
            else
            {
                return false;
            }
        }
        public DataTable getAllCurrency()
        {
            string query = "SELECT * FROM GEN_CURRENCY ";
            return DbFunctions.GetDataTable(query);
        }
        public void deleteCurrency()
        {
            string query = "DELETE FROM GEN_CURRENCY WHERE CODE = '" + code + "'";
            DbFunctions.InsertUpdate(query);
        }
    }
}
