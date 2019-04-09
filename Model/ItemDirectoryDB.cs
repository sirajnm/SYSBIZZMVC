using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    public class ItemDirectoryDB
    {
        string branch, code, desc_Eng, desc_Arb, type, group, category, trademark, supplier, uom;

        public string Uom
        {
            get { return uom; }
            set { uom = value; }
        }

        public string Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

        public string Trademark
        {
            get { return trademark; }
            set { trademark = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Desc_Arb
        {
            get { return desc_Arb; }
            set { desc_Arb = value; }
        }

        public string Desc_Eng
        {
            get { return desc_Eng; }
            set { desc_Eng = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }
        decimal costPrice, lastCostPrice, salePrice, containerUnit;

        public decimal ContainerUnit
        {
            get { return containerUnit; }
            set { containerUnit = value; }
        }

        public decimal SalePrice
        {
            get { return salePrice; }
            set { salePrice = value; }
        }

        public decimal LastCostPrice
        {
            get { return lastCostPrice; }
            set { lastCostPrice = value; }
        }

        public decimal CostPrice
        {
            get { return costPrice; }
            set { costPrice = value; }
        }
        int minimumQty, maximumQty, reorderQty, containerQty, taxId, period;

        public int Period
        {
            get { return period; }
            set { period = value; }
        }

        public int TaxId
        {
            get { return taxId; }
            set { taxId = value; }
        }

        public int ContainerQty
        {
            get { return containerQty; }
            set { containerQty = value; }
        }

        public int ReorderQty
        {
            get { return reorderQty; }
            set { reorderQty = value; }
        }

        public int MaximumQty
        {
            get { return maximumQty; }
            set { maximumQty = value; }
        }

        public int MinimumQty
        {
            get { return minimumQty; }
            set { minimumQty = value; }
        }
        string conditionMsg, firstUnitCode, firstUnitBarcode, hasSecond, secondUnitCode, thirdUnitCode, hasThird, secondunitBarcode, thirdunitBarcode, hasForth, forthUnitCode, forthUnitBarcode;

        public string ForthUnitBarcode
        {
            get { return forthUnitBarcode; }
            set { forthUnitBarcode = value; }
        }

        public string ForthUnitCode
        {
            get { return forthUnitCode; }
            set { forthUnitCode = value; }
        }

        public string HasForth
        {
            get { return hasForth; }
            set { hasForth = value; }
        }

        public string ThirdunitBarcode
        {
            get { return thirdunitBarcode; }
            set { thirdunitBarcode = value; }
        }

        public string SecondunitBarcode
        {
            get { return secondunitBarcode; }
            set { secondunitBarcode = value; }
        }

        public string HasThird
        {
            get { return hasThird; }
            set { hasThird = value; }
        }

        public string ThirdUnitCode
        {
            get { return thirdUnitCode; }
            set { thirdUnitCode = value; }
        }

        public string SecondUnitCode
        {
            get { return secondUnitCode; }
            set { secondUnitCode = value; }
        }

        public string HasSecond
        {
            get { return hasSecond; }
            set { hasSecond = value; }
        }

        public string FirstUnitBarcode
        {
            get { return firstUnitBarcode; }
            set { firstUnitBarcode = value; }
        }

        public string FirstUnitCode
        {
            get { return firstUnitCode; }
            set { firstUnitCode = value; }
        }

        public string ConditionMsg
        {
            get { return conditionMsg; }
            set { conditionMsg = value; }
        }
        float secondUnits, thirdUnits, forthUnits;

        public float ForthUnits
        {
            get { return forthUnits; }
            set { forthUnits = value; }
        }

        public float ThirdUnits
        {
            get { return thirdUnits; }
            set { thirdUnits = value; }
        }

        public float SecondUnits
        {
            get { return secondUnits; }
            set { secondUnits = value; }
        }
        string inActive, location, PeriodType, hsn;

        public string Hsn
        {
            get { return hsn; }
            set { hsn = value; }
        }

        public string PeriodType1
        {
            get { return PeriodType; }
            set { PeriodType = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public string InActive
        {
            get { return inActive; }
            set { inActive = value; }
        }
        bool hasserial, hasWarrenty;

        public bool HasWarrenty
        {
            get { return hasWarrenty; }
            set { hasWarrenty = value; }
        }

        public bool Hasserial
        {
            get { return hasserial; }
            set { hasserial = value; }
        }

        public SqlDataReader GetPriceType()
        {
            string query = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";
            return DbFunctions.GetDataReader(query);
        }

        public DataTable GetCodeInvUnit()
        {
            string query = "SELECT CODE FROM INV_UNIT";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable GetSupplyerForCombo()
        {
            string query = "SELECT CODE,DESC_ENG FROM PAY_SUPPLIER";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable GetLocation()
        {
            string query = "SELECT * From GEN_LOCATION";
            return DbFunctions.GetDataTable(query);
        }

        public ItemDirectoryDB()
        {

        }
        public static ItemDirectoryDB GetanItem(string itemno)
        {
            string query = "SELECT * From INV_ITEM_DIRECTORY where Code = @itemno";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@itemno", itemno);
            DataTable dt = DbFunctions.GetDataTable(query, parameter);

            ItemDirectoryDB item = new ItemDirectoryDB();
            foreach (DataRow dr in dt.Rows)
            {


                item.Code = dr["Code"].ToString();
                item.Desc_Eng = dr["Desc_Eng"].ToString();
                item.Desc_Arb = dr["Desc_Arb"].ToString();
                item.Trademark = dr["Trademark"].ToString();
                item.Uom = dr["Uom"].ToString();
                item.Type = dr["Type"].ToString();
                item.TaxId = (int)dr["TaxId"];
            }
            return item;
        }



        public DataTable ItemForGrid()
        {
            string query = "SELECT * From INV_ITEM_DIRECTORY";
            return DbFunctions.GetDataTable(query);
        }
        public Single GetTaxRate(string itemno)
        {
            string query = "Select Max(TaxRate) TaxRate from GEN_TAX_MASTER gt inner join INV_ITEM_DIRECTORY i on i.TaxID = gt.TaxID where i.Code = @itemno";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@itemno", itemno);
            object a = DbFunctions.GetAValue(query, parameter);
            if (a == "")
            {
                return 0;
            }
            return Convert.ToSingle(a);
        }


        public DataTable GetTaxRate()
        {
            string query = "SELECT TaxId, CODE + ' --- ' +CONVERT(varchar(10),TaxRate)+' %' AS Expr1 FROM GEN_TAX_MASTER";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable GetTaxRate_Debit()
        {
            string query = "SELECT TaxRate, CODE + ' --- ' +CONVERT(varchar(10),TaxRate)+' %' AS Expr1 FROM GEN_TAX_MASTER";
            return DbFunctions.GetDataTable(query);
        }

        public int IsProductUsed(string ItemCode)
        {
            string command = "IsProductUsed";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@item_code", ItemCode);
            return Convert.ToInt32(DbFunctions.GetAValueProcedure(command,Parameters));
        }

        public void DeleteItem(string Item_Code,string doc_no)
        {
            string query = "DELETE FROM INV_ITEM_DIRECTORY WHERE CODE = '" + Item_Code + "';";
            query += "DELETE FROM INV_ITEM_DIRECTORY_PICTURE WHERE CODE = '" + Item_Code + "';";
            query += "DELETE FROM INV_ITEM_PRICE WHERE ITEM_CODE='" + Item_Code + "';";
            query += "DELETE FROM INV_ITEM_PRICE_DF WHERE ITEM_CODE='" + Item_Code + "';";
            query += "DELETE FROM tblStock WHERE Item_id='" + Item_Code + "';";
            query += "DELETE FROM INV_STK_TRX_DTL WHERE ITEM_CODE='" + Item_Code + "';";
            query += "DELETE FROM INV_STK_TRX_HDR WHERE DOC_NO='" + doc_no + "';";
            DbFunctions.InsertUpdate(query);
        }

        public bool hasSerial()
        {
            string query = "SELECT HASSERIAL FROM INV_ITEM_DIRECTORY WHERE CODE='" + code + "'";
            return (bool)DbFunctions.GetAValue(query);
        }

        public String HSN(string item)
        {
            string query = "SELECT HSN FROM INV_ITEM_DIRECTORY WHERE CODE='" + item + "'";
            return (string)DbFunctions.GetAValue(query);
        }

        public DataTable GetAllData(bool hasArabic)
        {
            string query="";
            if (hasArabic)
                query = "SELECT CODE,DESC_ENG,DESC_ARB,TYPE,[GROUP],CATEGORY,TRADEMARK,COST_PRICE,SALE_PRICE,MINIMUM_QTY,MAXIMUM_QTY,IN_ACTIVE,TaxId FROM INV_ITEM_DIRECTORY";
            else
                query = "SELECT CODE,DESC_ENG,TYPE,[GROUP],CATEGORY,TRADEMARK,COST_PRICE,SALE_PRICE,MINIMUM_QTY,MAXIMUM_QTY,IN_ACTIVE,TaxId FROM INV_ITEM_DIRECTORY";
            return  DbFunctions.GetDataTable(query);
        }

        public void InsertBulk()
        {
            string query = "INSERT INTO INV_ITEM_DIRECTORY(CODE,DESC_ENG,DESC_ARB,TYPE,[GROUP],CATEGORY,TRADEMARK,COST_PRICE,SALE_PRICE,IN_ACTIVE,TaxId,HSN) VALUES('" + code + "','" + desc_Eng + "','" + Desc_Arb + "','" + type + "','" + group + "','" + category + "','" + trademark + "','" + costPrice + "','" + salePrice + "','"+inActive+"','" + taxId + "','" + hsn + "')";
            DbFunctions.InsertUpdate(query);
        }

        public DataTable GetDownloads()
        {
           String query = @"SELECT        INV_ITEM_DIRECTORY.CODE AS Code, INV_ITEM_DIRECTORY.HSN AS [HSN Code], INV_ITEM_DIRECTORY.DESC_ENG AS Name, 
                         INV_ITEM_DIRECTORY.DESC_ARB AS [Arabic Name], INV_ITEM_TYPE.CODE AS Type, INV_ITEM_GROUP.CODE AS [Group], 
                         INV_ITEM_CATEGORY.CODE AS Category, INV_ITEM_DIRECTORY.TaxId, INV_ITEM_TM.CODE AS Brand, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AS UOM, 
                         INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_PRICE_DF_3.PRICE AS [Whole Sale], STOCK.STOCK AS Quantity, 
                         INV_ITEM_PRICE_DF_1.PRICE AS [Purchase price], INV_ITEM_PRICE_DF.PRICE AS [Retail price], INV_ITEM_PRICE_DF_2.PRICE AS [Promotional price], 
                         INV_ITEM_PRICE_DF_4.PRICE AS [Maximum price], INV_ITEM_DIRECTORY.COST_PRICE AS [Cost price],CONVERT(DECIMAL(18,2), INV_ITEM_DIRECTORY.SALE_PRICE) AS [Sales price]
FROM            (SELECT INV_ITEM_PRICE.* FROM (SELECT BATCH_DATA.BATCH_INCRE,BATCH_DATA.ITEM_ID,tblStock.batch_id FROM (SELECT MAX(BATCH_INCREMENT) AS BATCH_INCRE,ITEM_ID FROM tblStock GROUP BY ITEM_ID) AS BATCH_DATA LEFT OUTER JOIN TBLSTOCK ON BATCH_DATA.BATCH_INCRE=TBLSTOCK.BATCH_INCREMENT AND tblStock.Item_id=BATCH_DATA.Item_id) AS BATCH_DESC LEFT OUTER JOIN INV_ITEM_PRICE ON INV_ITEM_PRICE.BATCH_ID=BATCH_DESC.BATCH_ID) INV_ITEM_PRICE_DF_4 LEFT OUTER JOIN
                        (SELECT INV_ITEM_PRICE.* FROM (SELECT BATCH_DATA.BATCH_INCRE,BATCH_DATA.ITEM_ID,tblStock.batch_id FROM (SELECT MAX(BATCH_INCREMENT) AS BATCH_INCRE,ITEM_ID FROM tblStock GROUP BY ITEM_ID) AS BATCH_DATA LEFT OUTER JOIN TBLSTOCK ON BATCH_DATA.BATCH_INCRE=TBLSTOCK.BATCH_INCREMENT AND tblStock.Item_id=BATCH_DATA.Item_id) AS BATCH_DESC LEFT OUTER JOIN INV_ITEM_PRICE ON INV_ITEM_PRICE.BATCH_ID=BATCH_DESC.BATCH_ID) INV_ITEM_PRICE_DF_2 INNER JOIN
                         INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE_DF_2.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND 
                         INV_ITEM_PRICE_DF_2.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN
                         (SELECT INV_ITEM_PRICE.* FROM (SELECT BATCH_DATA.BATCH_INCRE,BATCH_DATA.ITEM_ID,tblStock.batch_id FROM (SELECT MAX(BATCH_INCREMENT) AS BATCH_INCRE,ITEM_ID FROM tblStock GROUP BY ITEM_ID) AS BATCH_DATA LEFT OUTER JOIN TBLSTOCK ON BATCH_DATA.BATCH_INCRE=TBLSTOCK.BATCH_INCREMENT AND tblStock.Item_id=BATCH_DATA.Item_id) AS BATCH_DESC LEFT OUTER JOIN INV_ITEM_PRICE ON INV_ITEM_PRICE.BATCH_ID=BATCH_DESC.BATCH_ID) INV_ITEM_PRICE_DF_3 ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_DF_3.ITEM_CODE AND 
                         INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_DF_3.UNIT_CODE ON 
                         INV_ITEM_PRICE_DF_4.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND 
                         INV_ITEM_PRICE_DF_4.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE RIGHT OUTER JOIN
                         (SELECT INV_ITEM_PRICE.* FROM (SELECT BATCH_DATA.BATCH_INCRE,BATCH_DATA.ITEM_ID,tblStock.batch_id FROM (SELECT MAX(BATCH_INCREMENT) AS BATCH_INCRE,ITEM_ID FROM tblStock GROUP BY ITEM_ID) AS BATCH_DATA LEFT OUTER JOIN TBLSTOCK ON BATCH_DATA.BATCH_INCRE=TBLSTOCK.BATCH_INCREMENT AND tblStock.Item_id=BATCH_DATA.Item_id) AS BATCH_DESC LEFT OUTER JOIN INV_ITEM_PRICE ON INV_ITEM_PRICE.BATCH_ID=BATCH_DESC.BATCH_ID) INV_ITEM_PRICE_DF ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_DF.UNIT_CODE AND 
                         INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_DF.ITEM_CODE LEFT OUTER JOIN
                         (SELECT INV_ITEM_PRICE.* FROM (SELECT BATCH_DATA.BATCH_INCRE,BATCH_DATA.ITEM_ID,tblStock.batch_id FROM (SELECT MAX(BATCH_INCREMENT) AS BATCH_INCRE,ITEM_ID FROM tblStock GROUP BY ITEM_ID) AS BATCH_DATA LEFT OUTER JOIN TBLSTOCK ON BATCH_DATA.BATCH_INCRE=TBLSTOCK.BATCH_INCREMENT AND tblStock.Item_id=BATCH_DATA.Item_id) AS BATCH_DESC LEFT OUTER JOIN INV_ITEM_PRICE ON INV_ITEM_PRICE.BATCH_ID=BATCH_DESC.BATCH_ID) INV_ITEM_PRICE_DF_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_DF_1.UNIT_CODE AND 
                         INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_DF_1.ITEM_CODE RIGHT OUTER JOIN
                         INV_ITEM_DIRECTORY LEFT OUTER JOIN
                         GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId ON 
                         INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE LEFT OUTER JOIN
                         INV_ITEM_TYPE ON INV_ITEM_DIRECTORY.TYPE = INV_ITEM_TYPE.CODE LEFT OUTER JOIN
                         INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY = INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN
                         INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP] = INV_ITEM_GROUP.CODE LEFT OUTER JOIN
                         INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK = INV_ITEM_TM.CODE LEFT OUTER JOIN
                             (SELECT        INV_ITEM_DIRECTORY.CODE, INV_ITEM_DIRECTORY.DESC_ENG,INV_ITEM_DIRECTORY_UNITS.UNIT_CODE,ISNULL(SALES.QTY, 0) + ISNULL(SALES_RETURN.QTY, 0) 
                                                         - ISNULL(PURCHASE_RETURN.QTY, 0) + ISNULL(PURCHASE.QTY, 0)+ISNULL(OPENING.QTY,0) AS STOCK FROM INV_ITEM_DIRECTORY LEFT OUTER JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_DIRECTORY.CODE=INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN
                                                             (SELECT        ITEM_CODE, ISNULL(SUM(QUANTITY), 0) AS QTY, UOM
                                                               FROM            INV_SALES_DTL
                                                               WHERE        (DOC_TYPE IN ('SAL.CSS', 'SAL.CRD'))
                                                               GROUP BY ITEM_CODE, UOM) AS SALES ON SALES.ITEM_CODE = INV_ITEM_DIRECTORY.CODE AND 
                                                         SALES.UOM = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN
                                                             (SELECT        ITEM_CODE, ISNULL(SUM(QTY_RCVD), 0) AS QTY, UOM
                                                               FROM            INV_PURCHASE_DTL
                                                               WHERE        (DOC_TYPE IN ('PUR.CSS', 'PUR.CRD'))
                                                               GROUP BY ITEM_CODE, UOM) AS PURCHASE ON PURCHASE.ITEM_CODE = INV_ITEM_DIRECTORY.CODE AND 
                                                         PURCHASE.UOM = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN

														 (SELECT        ITEM_CODE, ISNULL(SUM(QUANTITY), 0) AS QTY, UOM
                                                               FROM            INV_STK_TRX_DTL
                                                               WHERE        (DOC_TYPE IN ('INV.STK.OPN'))
                                                               GROUP BY ITEM_CODE, UOM) AS OPENING ON OPENING.ITEM_CODE = INV_ITEM_DIRECTORY.CODE AND 
                                                         OPENING.UOM = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN

                                                             (SELECT        ITEM_CODE, ISNULL(SUM(QTY_RCVD), 0) AS QTY, UOM
                                                               FROM            INV_PURCHASE_DTL
                                                               WHERE        (DOC_TYPE IN ('LGR.PRT'))
                                                               GROUP BY ITEM_CODE, UOM) AS PURCHASE_RETURN ON PURCHASE_RETURN.ITEM_CODE = INV_ITEM_DIRECTORY.CODE AND 
                                                         PURCHASE_RETURN.UOM = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN
                                                             (SELECT        ITEM_CODE, ISNULL(SUM(QUANTITY), 0) AS QTY, UOM
                                                               FROM            INV_SALES_DTL
                                                               WHERE        (DOC_TYPE IN ('SAL.CSR'))
                                                               GROUP BY ITEM_CODE, UOM) AS SALES_RETURN ON SALES_RETURN.ITEM_CODE = INV_ITEM_DIRECTORY.CODE AND 
                                                         PURCHASE_RETURN.UOM = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE)  AS STOCK ON 
                         STOCK.CODE = INV_ITEM_DIRECTORY.CODE AND STOCK.UNIT_CODE=INV_ITEM_DIRECTORY_UNITS.UNIT_CODE
WHERE        (INV_ITEM_PRICE_DF.SAL_TYPE = 'RTL') AND (INV_ITEM_PRICE_DF_1.SAL_TYPE = 'PUR') AND (INV_ITEM_PRICE_DF_2.SAL_TYPE = 'PMS') AND 
                         (INV_ITEM_PRICE_DF_3.SAL_TYPE = 'WHL') AND (INV_ITEM_PRICE_DF_4.SAL_TYPE = 'MRP')";
           return DbFunctions.GetDataTable(query);
        }
    }
}
