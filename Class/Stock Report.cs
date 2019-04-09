using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory.Class
{
    class Stock_Report
    {

        
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand(); 
        private SqlDataAdapter adapter = new SqlDataAdapter();

        public string Code { get; set; }
        public string Unit { get; set; }
        public string docType { get; set; }
        public string supplier { get; set; }
        public string itemName { get; set; }
        public DateTime  startDate { get; set; }
        public DateTime endDate { get; set; }
        public DataTable BindType()
        {
            //DataTable dt = new DataTable();           
            //cmd.CommandText = "Select CODE,DESC_ENG,DESC_ARB FROM INV_ITEM_TYPE";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);      
            string Query = "Select CODE,DESC_ENG,DESC_ARB FROM INV_ITEM_TYPE";
            //cmd.Connection = conn;
            return DbFunctions.GetDataTable(Query);
            //return dt;

        }

        public DataTable BindSaleType()
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "Select CODE,DESC_ENG,DESC_ARB FROM INV_ITEM_TYPE";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //return dt;
            string Query = "Select CODE,DESC_ENG,DESC_ARB FROM INV_ITEM_TYPE";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable Bind_item_name()
        {
            //DataTable dt = new DataTable();          
            //cmd.CommandText = "Select CODE,DESC_ENG FROM INV_ITEM_DIRECTORY";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);           
            //return dt;
            string Query = "Select CODE,DESC_ENG FROM INV_ITEM_DIRECTORY";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable bindItemName()
        {
            //DataTable dt = new DataTable();          
            //cmd.CommandText = "Select CODE,DESC_ENG FROM INV_ITEM_DIRECTORY";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);           
            //return dt;
            string Query = "SELECT        INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name] FROM   INV_ITEM_DIRECTORY ";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable Bind_item_id()
        {
            //DataTable dt = new DataTable();         
            //cmd.CommandText = "Select CODE FROM INV_ITEM_DIRECTORY";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);         
            //return dt;
            string Query = "Select CODE FROM INV_ITEM_DIRECTORY";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable BindGroup()
        {
            //DataTable dt = new DataTable();          
            //cmd.CommandText = "Select CODE,DESC_ENG,DESC_ARB FROM INV_ITEM_GROUP";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //return dt;                      
            string Query = "Select CODE,DESC_ENG,DESC_ARB FROM INV_ITEM_GROUP";
            return DbFunctions.GetDataTable(Query);
        }

       


        public DataTable BindCategory()
        {
            //DataTable dt = new DataTable();          
            //cmd.CommandText = "Select CODE,DESC_ENG,DESC_ARB FROM INV_ITEM_CATEGORY";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);        
            //return dt;
            string Query = "Select CODE,DESC_ENG,DESC_ARB FROM INV_ITEM_CATEGORY";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable BindUOM()
        {
            //DataTable dt = new DataTable(); 
            //cmd.CommandText = "Select CODE,DESC_ENG,DESC_ARB FROM INV_UNIT";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);          
            //return dt;
            string Query = "Select CODE,DESC_ENG,DESC_ARB FROM INV_UNIT";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable BindPriceType()
        {
            //DataTable dt = new DataTable();          
            //cmd.CommandText = "Select CODE,DESC_ENG,DESC_ARB FROM GEN_PRICE_TYPE";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);           
            //return dt;
            string Query = "Select CODE,DESC_ENG,DESC_ARB FROM GEN_PRICE_TYPE";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable BindTrademark()
        {
            //DataTable dt = new DataTable();            
            //cmd.CommandText = "Select CODE,DESC_ENG,DESC_ARB FROM INV_ITEM_TM";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);          
            //return dt;
            string Query = "Select CODE,DESC_ENG,DESC_ARB FROM INV_ITEM_TM";
            return DbFunctions.GetDataTable(Query);
        }
       
        public DataTable BindSupplier()
        {
            //DataTable dt = new DataTable();           
            //cmd.CommandText = "SELECT    CODE, DESC_ENG FROM         PAY_SUPPLIER";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);           
            //return dt;
            string Query = "SELECT    CODE, DESC_ENG FROM         PAY_SUPPLIER";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable BindCustomerbY_Saleman(string salesman)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "SELECT    CODE, DESC_ENG FROM REC_CUSTOMER WHERE SALESMAN_CODE='" + salesman + "'";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //return dt;
            string Query = "SELECT    CODE, DESC_ENG FROM REC_CUSTOMER WHERE SALESMAN_CODE='" + salesman + "'";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable BindBr()
        {
            //DataTable dt = new DataTable();           
            //cmd.CommandText = "SELECT CODE,DESC_ENG FROM GEN_BRANCH";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);          
            //return dt;
            string Query = "SELECT CODE,DESC_ENG FROM GEN_BRANCH";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable BindCustomer()
        {
            //DataTable dt = new DataTable();         
            //cmd.CommandText = "SELECT    CODE, DESC_ENG FROM         REC_CUSTOMER";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);           
            //return dt;
            string Query = "SELECT    CODE, DESC_ENG FROM         REC_CUSTOMER";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable BindSalesTypes()
        {
            //DataTable dt = new DataTable();          
            //cmd.CommandText = "SELECT     CODE, DESC_ENG FROM         SYS_DOC_TYPE WHERE     (CODE LIKE 'PUR.%') or (CODE LIKE 'LGR.%')";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);            
            //return dt;
            string Query = "SELECT     CODE, DESC_ENG FROM         SYS_DOC_TYPE WHERE     (CODE LIKE 'PUR.%') or (CODE LIKE 'LGR.%')";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable GetPurType()
        {
            //conn.Open();
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT CODE,DESC_ENG FROM GEN_PUR_TYPE";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = conn;
            //DataTable DT = new DataTable();
            //SqlDataAdapter ad = new SqlDataAdapter(cmd);
            //ad.Fill(DT);
            //conn.Close();
            string Query = "SELECT CODE,DESC_ENG FROM GEN_PUR_TYPE";
             DataTable DT = DbFunctions.GetDataTable(Query);
            DataRow row = DT.NewRow();
            row[1] = "<<-SELECT TYPE->>";
            DT.Rows.InsertAt(row, 0);
            return DT;
        }

        public DataTable BindSTypes()
        {
            //DataTable dt = new DataTable();         
            //cmd.CommandText = "SELECT     CODE, DESC_ENG FROM         SYS_DOC_TYPE WHERE     (CODE LIKE 'SAL.%') ";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //return dt;
            string Query = "SELECT     CODE, DESC_ENG FROM         SYS_DOC_TYPE WHERE     (CODE LIKE 'SAL.%') ";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable BindSTypesWithoutReturn()
        {
            //DataTable dt = new DataTable();            
            //cmd.CommandText = "SELECT     CODE, DESC_ENG FROM         SYS_DOC_TYPE WHERE     (CODE LIKE 'SAL.%') and (CODE<>'SAL.CSR') AND (CODE<>'SAL.CDR') ";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);         
            //return dt;
            string Query = "SELECT     CODE, DESC_ENG FROM         SYS_DOC_TYPE WHERE     (CODE LIKE 'SAL.%') and (CODE<>'SAL.CSR') AND (CODE<>'SAL.CDR') ";
            return DbFunctions.GetDataTable(Query);
        }


        public DataTable GetItemStockValue()
        {
            DataTable dt = new DataTable();
            

              //  cmd.CommandText = "STOCK";
               // cmd.Connection = conn;
              //  cmd.CommandType = CommandType.StoredProcedure;
              //  adapter.SelectCommand = cmd;
              //  cmd.Parameters.AddWithValue("@Code", Code);
              //  cmd.Parameters.AddWithValue("@Unit", Unit);
               // adapter.Fill(dt);
               // cmd.Parameters.Clear();

           
            return dt;

        }
        public DataTable dateWasePurchaseReportWithItem()
        {
            string Query = "SELECT    distinct    INV_PURCHASE_HDR.DOC_NO,CASE WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CSS' THEN 'CASH PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CRD' THEN 'CREDIT PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='LGR.PRT' THEN 'PURCHASE RETURN' ELSE '' END as Type, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL as Discount, INV_PURCHASE_HDR.NET_VAL as 'Net Value',  INV_PURCHASE_DTL.ITEM_CODE as 'Item code', INV_PURCHASE_DTL.ITEM_DESC_ENG as 'Item Name'  FROM            INV_PURCHASE_HDR INNER JOIN INV_PURCHASE_DTL ON INV_PURCHASE_HDR.DOC_NO = INV_PURCHASE_DTL.DOC_NO LEFT OUTER JOIN PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE        (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%') AND (INV_PURCHASE_HDR.DOC_DATE_GRE BETWEEN @Date1 AND @Date2) AND (INV_PURCHASE_DTL.ITEM_DESC_ENG LIKE '%' + @Name + '%')and (INV_PURCHASE_HDR.FLAGDEL=1)and (INV_PURCHASE_HDR.DOC_TYPE IN('PUR.CSS','LGR.CRD','PUR.CRD'))";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@DOC_TYPE",docType);
            parameters.Add("@SUP_CODE", supplier);
            parameters.Add("@Date1", startDate);
            parameters.Add("@Date2",endDate);
            parameters.Add("@Name", itemName);
            return DbFunctions.GetDataTable(Query,parameters);

        }
        public DataTable dateWasePurchaseReportWithOutItem()
        {
            string Query = "SELECT distinct INV_PURCHASE_HDR.DOC_NO,CASE WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CSS' THEN 'CASH PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CRD' THEN 'CREDIT PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='LGR.PRT' THEN 'PURCHASE RETURN' ELSE '' END as Type, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL as Discount, INV_PURCHASE_HDR.NET_VAL as 'Net Value' FROM            INV_PURCHASE_HDR LEFT OUTER JOIN    PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE        (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%') AND  (INV_PURCHASE_HDR.DOC_DATE_GRE BETWEEN @Date1 AND @Date2)and(INV_PURCHASE_HDR.FLAGDEL=1)and (INV_PURCHASE_HDR.DOC_TYPE IN('PUR.CSS','LGR.CRD','PUR.CRD')";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@DOC_TYPE", docType);
            parameters.Add("@SUP_CODE", supplier);
            parameters.Add("@Date1", startDate);
            parameters.Add("@Date2", endDate);
            parameters.Add("@Name", itemName);
            return DbFunctions.GetDataTable(Query, parameters);

        }
        public DataTable PurchaseReportWithItem()
        {
          //  string Query = "SELECT        INV_PURCHASE_HDR.DOC_NO,CASE WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CSS' THEN 'CASH PURCHASE'  WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CRD' THEN 'CREDIT PURCHASE'  WHEN INV_PURCHASE_HDR.DOC_TYPE='LGR.PRT' THEN 'PURCHASE RETURN' ELSE '' END as Type, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_DTL.ITEM_CODE as 'Item Code', INV_PURCHASE_DTL.ITEM_DESC_ENG as 'Item Name', INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as 'Freight', INV_PURCHASE_HDR.GROSS as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL as Discount,INV_PURCHASE_HDR.NET_VAL as 'Net Value' FROM            INV_PURCHASE_HDR INNER JOIN INV_PURCHASE_DTL ON INV_PURCHASE_HDR.DOC_NO = INV_PURCHASE_DTL.DOC_NO LEFT OUTER JOIN  PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE        (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%') AND  (INV_PURCHASE_DTL.ITEM_DESC_ENG LIKE N'%' + @Name + N'%')and (INV_PURCHASE_HDR.FLAGDEL=1)and (INV_PURCHASE_HDR.DOC_TYPE IN('PUR.CSS','LGR.CRD'))";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string Query = "SELECT  INV_PURCHASE_HDR.DOC_NO,CASE WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CSS' THEN 'CASH PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CRD' THEN 'CREDIT PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='LGR.PRT' THEN 'PURCHASE RETURN' ELSE '' END as Type, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_DTL.ITEM_CODE as 'Item Code', INV_PURCHASE_DTL.ITEM_DESC_ENG as 'Item Name', INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL as Discount,INV_PURCHASE_HDR.NET_VAL as 'Net Value' FROM   INV_PURCHASE_HDR INNER JOIN INV_PURCHASE_DTL ON INV_PURCHASE_HDR.DOC_NO = INV_PURCHASE_DTL.DOC_NO LEFT OUTER JOIN  PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%')and (INV_PURCHASE_HDR.FLAGDEL=1) AND  (INV_PURCHASE_DTL.ITEM_DESC_ENG LIKE N'%' + @Name + N'%') and (INV_PURCHASE_HDR.DOC_TYPE IN('PUR.CSS','LGR.CRD','PUR.CRD'))";
            parameters.Add("@DOC_TYPE", docType);
            parameters.Add("@SUP_CODE", supplier);
           
            parameters.Add("@Name", itemName);
            return DbFunctions.GetDataTable(Query, parameters);

        }
        public DataTable PurchaseReportWithOutItem()
        {
            string Query = "SELECT  INV_PURCHASE_HDR.PUR_TYPE AS [PURC TYPE],INV_PURCHASE_HDR.DOC_ID AS ID,INV_PURCHASE_HDR.DOC_NO,CASE WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CSS' THEN 'CASH PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CRD' THEN 'CREDIT PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='LGR.PRT' THEN 'PURCHASE RETURN' ELSE '' END as Type, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL as Discount,INV_PURCHASE_HDR.NET_VAL as 'Net Value' FROM            INV_PURCHASE_HDR LEFT OUTER JOIN  PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%')and (INV_PURCHASE_HDR.FLAGDEL=1)and (INV_PURCHASE_HDR.DOC_TYPE IN('PUR.CSS','LGR.CRD','PUR.CRD'))";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@DOC_TYPE", docType);
            parameters.Add("@SUP_CODE", supplier);

            parameters.Add("@Name", itemName);
            return DbFunctions.GetDataTable(Query, parameters);

        }
        public DataTable getCompanyDetails()
        {
            //DataTable dt = new DataTable();         
            //cmd.CommandText = "SELECT     CODE, DESC_ENG FROM         SYS_DOC_TYPE WHERE     (CODE LIKE 'SAL.%') ";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //return dt;
            string Query = "SELECT * FROM SYS_SETUP";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable getMinimumStock()
        {
            //DataTable dt = new DataTable();         
            //cmd.CommandText = "SELECT     CODE, DESC_ENG FROM         SYS_DOC_TYPE WHERE     (CODE LIKE 'SAL.%') ";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //return dt;
            string Query = "SELECT StockValues.DESC_ENG as Name,StockValues.Code,QTY as Stock,StockValues.MinQty,StockValues.ReodrQty,StockValues.MQty FROM (select INV_ITEM_DIRECTORY.CODE,INV_ITEM_DIRECTORY.DESC_ENG,stock.QTY,Isnull(INV_ITEM_DIRECTORY.MINIMUM_QTY,0) as MINQTY,Isnull(INV_ITEM_DIRECTORY.REORDER_QTY,0) aS REODRQTY,(stock.QTY-Isnull(INV_ITEM_DIRECTORY.MINIMUM_QTY,0)) AS MQTY from INV_ITEM_DIRECTORY left outer join (select item_id,sum(qty) AS QTY from  tblstock group by item_id) AS STOCK ON INV_ITEM_DIRECTORY.CODE=STOCK.item_id where INV_ITEM_DIRECTORY.MINIMUM_QTY IS NOT NULL) AS StockValues";
            return DbFunctions.GetDataTable(Query);
        }
    }
}
