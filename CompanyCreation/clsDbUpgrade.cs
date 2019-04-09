using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys_Sols_Inventory.CompanyCreation
{
    class clsDbUpgrade
    {
      public static void  updateStoredProcedure()
        {
            string item_suggestion_for_sales= @"ALTER PROCEDURE [dbo].[item_suggestion_for_sales]

         @isService bit

        AS
        DECLARE @DynamicPivotQuery AS NVARCHAR(MAX),
	    @pType AS NVARCHAR(50),
        @PivotColumnNames AS NVARCHAR(MAX),
        @PivotSelectColumnNames AS NVARCHAR(MAX)
--Get distinct values of the PIVOT Column
SELECT @PivotColumnNames = ISNULL(@PivotColumnNames + ',', '')
+ QUOTENAME(SAL_TYPE)
FROM(SELECT DISTINCT SAL_TYPE FROM INV_ITEM_PRICE_DF) AS SALE_TYPE
--Get distinct values of the PIVOT Column with isnull
SELECT @PivotSelectColumnNames
    = ISNULL(@PivotSelectColumnNames + ',', '')
    + 'ISNULL(' + QUOTENAME(SAL_TYPE) + ', 0) AS '
    + QUOTENAME(SAL_TYPE)
FROM(SELECT DISTINCT SAL_TYPE FROM INV_ITEM_PRICE_DF) AS SALE_TYPE
--Prepare the PIVOT query using the dynamic

if @isService = 0
begin
select @pType = '''FGD'''
end
ELSE
BEGIN
select @pType = '''FGD''' + ',' + '''SRV'''
END
SET @DynamicPivotQuery =


N'SELECT price_table.ITEM_CODE, item.DESC_ENG AS [ITEM NAME],stock.QTY  AS STOCK,

price_table.UNIT_CODE, unit.BARCODE as Barcode,  
 item.DESC_ARB, CONCAT(item.PERIOD, '' '', item.PERIODTYPE) AS PERIOD,
GEN_TAX_MASTER.TaxRate, INV_ITEM_TYPE.DESC_ENG as [TYPE], INV_ITEM_CATEGORY.DESC_ENG as CATEGORY, 
INV_ITEM_GROUP.DESC_ENG AS[GROUP], INV_ITEM_TM.DESC_ENG AS TRADEMARK, item.TaxId, 
item.HASSERIAL,price_table.'+@PivotColumnNames+',item.PRODUCT_TYPE PTYPE FROM
INV_ITEM_DIRECTORY as item
LEFT JOIN(SELECT ITEM_CODE, UNIT_CODE, ' + @PivotSelectColumnNames + ' FROM INV_ITEM_PRICE_DF PIVOT(SUM(PRICE) FOR SAL_TYPE IN(' + @PivotColumnNames + ')) AS PVTTable) AS price_table
ON item.CODE = price_table.ITEM_CODE
INNER JOIN INV_ITEM_DIRECTORY_UNITS as unit ON item.code = unit.item_code AND price_table.UNIT_CODE = unit.unit_code and unit.pack_size = 1
  LEFT OUTER JOIN(SELECT item_id ITEM_CODE, ISNULL(SUM(Qty), 0) AS QTY  FROM tblStock
  GROUP BY item_id) AS stock ON stock.ITEM_CODE = item.CODE


LEFT JOIN GEN_TAX_MASTER ON item.TaxId = GEN_TAX_MASTER.TaxId
LEFT JOIN INV_ITEM_TYPE ON item.TYPE = INV_ITEM_TYPE.CODE
LEFT JOIN INV_ITEM_CATEGORY ON item.CATEGORY = INV_ITEM_CATEGORY.CODE
LEFT JOIN INV_ITEM_GROUP ON item.[GROUP] = INV_ITEM_GROUP.CODE
LEFT JOIN INV_ITEM_TM ON item.TRADEMARK = INV_ITEM_TM.CODE
WHERE item.PRODUCT_TYPE IN('+@pType+')
ORDER BY[ITEM NAME], unit.UNIT_CODE ASC'
--Execute the Dynamic Pivot Query
EXEC sp_executesql @DynamicPivotQuery
RESULT: " ;
Model.DbFunctions.InsertUpdate(item_suggestion_for_sales);


string item_suggestion_without_stock = @"ALTER PROCEDURE [dbo].[item_suggestion_without_stock]


	AS
	DECLARE @DynamicPivotQuery AS NVARCHAR(MAX),
        @PivotColumnNames AS NVARCHAR(MAX),
        @PivotSelectColumnNames AS NVARCHAR(MAX)
--Get distinct values of the PIVOT Column
SELECT @PivotColumnNames= ISNULL(@PivotColumnNames + ',','')
+ QUOTENAME(SAL_TYPE)
FROM (SELECT DISTINCT SAL_TYPE FROM INV_ITEM_PRICE_DF) AS SALE_TYPE
--Get distinct values of the PIVOT Column with isnull
SELECT @PivotSelectColumnNames 
    = ISNULL(@PivotSelectColumnNames + ',','')
    + 'ISNULL(' + QUOTENAME(SAL_TYPE) + ', 0) AS '
    + QUOTENAME(SAL_TYPE)
FROM (SELECT DISTINCT SAL_TYPE FROM INV_ITEM_PRICE_DF) AS SALE_TYPE
--Prepare the PIVOT query using the dynamic
SET @DynamicPivotQuery =


N'SELECT price_table.ITEM_CODE, item.DESC_ENG AS [ITEM NAME],price_table.UNIT_CODE, price_table.' + @PivotColumnNames + ', unit.BARCODE as Barcode,  
 item.DESC_ARB, CONCAT(item.PERIOD, '' '', item.PERIODTYPE) AS PERIOD, 
GEN_TAX_MASTER.TaxRate, INV_ITEM_TYPE.DESC_ENG as [TYPE], INV_ITEM_CATEGORY.DESC_ENG as CATEGORY, 
INV_ITEM_GROUP.DESC_ENG AS [GROUP], INV_ITEM_TM.DESC_ENG AS TRADEMARK, item.TaxId, 
item.HASSERIAL FROM 
INV_ITEM_DIRECTORY as item
LEFT JOIN (SELECT ITEM_CODE, UNIT_CODE, ' + @PivotSelectColumnNames + ' FROM INV_ITEM_PRICE_DF PIVOT(SUM(PRICE) FOR SAL_TYPE IN (' + @PivotColumnNames + ')) AS PVTTable) AS price_table 
ON item.CODE = price_table.ITEM_CODE 
LEFT JOIN INV_ITEM_DIRECTORY_UNITS as unit ON item.code = unit.item_code AND price_table.UNIT_CODE = unit.unit_code 

LEFT JOIN GEN_TAX_MASTER ON item.TaxId = GEN_TAX_MASTER.TaxId 
LEFT JOIN INV_ITEM_TYPE ON item.TYPE = INV_ITEM_TYPE.CODE 
LEFT JOIN INV_ITEM_CATEGORY ON item.CATEGORY = INV_ITEM_CATEGORY.CODE 
LEFT JOIN INV_ITEM_GROUP ON item.[GROUP] = INV_ITEM_GROUP.CODE 
LEFT JOIN INV_ITEM_TM ON item.TRADEMARK = INV_ITEM_TM.CODE 
WHERE item.PRODUCT_TYPE NOT IN(''SRV'')
ORDER BY [ITEM NAME], unit.UNIT_CODE ASC'
--Execute the Dynamic Pivot Query
EXEC sp_executesql @DynamicPivotQuery
RESULT:";
Model.DbFunctions.InsertUpdate(item_suggestion_without_stock);
  string item_suggestionForReturn = @"ALTER PROCEDURE [dbo].[item_suggestionForReturn]
@isService bit


    AS
    DECLARE @DynamicPivotQuery AS NVARCHAR(MAX),
	    @pType AS NVARCHAR(50),
        @PivotColumnNames AS NVARCHAR(MAX),
        @PivotSelectColumnNames AS NVARCHAR(MAX)
--Get distinct values of the PIVOT Column
SELECT @PivotColumnNames = ISNULL(@PivotColumnNames + ',', '')
+ QUOTENAME(SAL_TYPE)
FROM(SELECT DISTINCT SAL_TYPE FROM INV_ITEM_PRICE_DF) AS SALE_TYPE
--Get distinct values of the PIVOT Column with isnull
SELECT @PivotSelectColumnNames
    = ISNULL(@PivotSelectColumnNames + ',', '')
    + 'ISNULL(' + QUOTENAME(SAL_TYPE) + ', 0) AS '
    + QUOTENAME(SAL_TYPE)
FROM(SELECT DISTINCT SAL_TYPE FROM INV_ITEM_PRICE_DF) AS SALE_TYPE

if @isService = 0
begin
select @pType = '''FGD'''
end
ELSE
BEGIN
select @pType = '''FGD''' + ',' + '''SRV'''
END
--Prepare the PIVOT query using the dynamic
SET @DynamicPivotQuery =


N'SELECT price_table.ITEM_CODE, item.DESC_ENG AS [ITEM_NAME],price_table.UNIT_CODE, price_table.' + @PivotColumnNames + ', unit.BARCODE as Barcode,  
 item.DESC_ARB, CONCAT(item.PERIOD, '' '', item.PERIODTYPE) AS PERIOD,
GEN_TAX_MASTER.TaxRate, INV_ITEM_TYPE.DESC_ENG as [TYPE], INV_ITEM_CATEGORY.DESC_ENG as CATEGORY, 
INV_ITEM_GROUP.DESC_ENG AS[GROUP], INV_ITEM_TM.DESC_ENG AS TRADEMARK, item.TaxId, 
item.HASSERIAL,item.PRODUCT_TYPE PTYPE FROM
INV_ITEM_DIRECTORY as item
LEFT JOIN(SELECT ITEM_CODE, UNIT_CODE, ' + @PivotSelectColumnNames + ' FROM INV_ITEM_PRICE_DF PIVOT(SUM(PRICE) FOR SAL_TYPE IN(' + @PivotColumnNames + ')) AS PVTTable) AS price_table
ON item.CODE = price_table.ITEM_CODE
LEFT JOIN INV_ITEM_DIRECTORY_UNITS as unit ON item.code = unit.item_code AND price_table.UNIT_CODE = unit.unit_code and unit.pack_size = 1

LEFT JOIN GEN_TAX_MASTER ON item.TaxId = GEN_TAX_MASTER.TaxId
LEFT JOIN INV_ITEM_TYPE ON item.TYPE = INV_ITEM_TYPE.CODE
LEFT JOIN INV_ITEM_CATEGORY ON item.CATEGORY = INV_ITEM_CATEGORY.CODE
LEFT JOIN INV_ITEM_GROUP ON item.[GROUP] = INV_ITEM_GROUP.CODE
LEFT JOIN INV_ITEM_TM ON item.TRADEMARK = INV_ITEM_TM.CODE
WHERE item.PRODUCT_TYPE IN('+@pType+')
ORDER BY[ITEM_NAME], unit.UNIT_CODE ASC'
--Execute the Dynamic Pivot Query
EXEC sp_executesql @DynamicPivotQuery
RESULT:";
 Model.DbFunctions.InsertUpdate(item_suggestionForReturn);


            string itemSuggestion_test= @"ALTER PROCEDURE [dbo].[itemSuggestion_test]
	AS
	DECLARE @DynamicPivotQuery AS NVARCHAR(MAX),
        @PivotColumnNames AS NVARCHAR(MAX),
        @PivotSelectColumnNames AS NVARCHAR(MAX),
		 @TYPE1 AS NVARCHAR(MAX)='PUR.CSS\"",\""PUR.CRD',

          @TYPE2 AS NVARCHAR(MAX) = 'SAL.CSS,SAL.CRD'


--Get distinct values of the PIVOT Column
SELECT @PivotColumnNames = ISNULL(@PivotColumnNames + ',', '')
+ QUOTENAME(SAL_TYPE)
FROM(SELECT DISTINCT SAL_TYPE FROM INV_ITEM_PRICE) AS SALE_TYPE
--Get distinct values of the PIVOT Column with isnull
SELECT @PivotSelectColumnNames
    = ISNULL(@PivotSelectColumnNames + ',', '')
    + 'ISNULL(' + QUOTENAME(SAL_TYPE) + ', 0) AS '
    + QUOTENAME(SAL_TYPE)
FROM(SELECT DISTINCT SAL_TYPE FROM INV_ITEM_PRICE) AS SALE_TYPE
--Prepare the PIVOT query using the dynamic


SET @DynamicPivotQuery =
N'SELECT price_table.*,  item.DESC_ENG AS [ITEM NAME], (tblStock.Qty) AS STOCK,
 item.DESC_ARB, CONCAT(item.PERIOD, '' '', item.PERIODTYPE) AS PERIOD,
GEN_TAX_MASTER.TaxRate, INV_ITEM_TYPE.DESC_ENG as [TYPE], INV_ITEM_CATEGORY.DESC_ENG as CATEGORY, 
INV_ITEM_GROUP.DESC_ENG AS[GROUP], INV_ITEM_TM.DESC_ENG AS TRADEMARK, item.TaxId, 
item.HASSERIAL, tblStock.cost_price,tblStock.ManBatch AS[BATCH CODE], tblStock.Exdate AS[EXPIRY DATE], tblStock.supplier_id, PAY_SUPPLIER.DESC_ENG as Supplier,item.HSN,tblStock.batch_increment,item.PRODUCT_TYPE PTYPE FROM
INV_ITEM_DIRECTORY as item
LEFT JOIN tblStock ON item.code = tblStock.Item_id


LEFT JOIN(SELECT ITEM_CODE, UNIT_CODE, batch_id,' + @PivotColumnNames + ' FROM INV_ITEM_PRICE PIVOT(SUM(PRICE) FOR SAL_TYPE IN(' + @PivotColumnNames + ')) AS PVTTable) AS price_table
ON tblStock.batch_id = price_table.batch_id



LEFT JOIN GEN_TAX_MASTER ON item.TaxId = GEN_TAX_MASTER.TaxId
LEFT JOIN INV_ITEM_TYPE ON item.TYPE = INV_ITEM_TYPE.CODE
LEFT JOIN INV_ITEM_CATEGORY ON item.CATEGORY = INV_ITEM_CATEGORY.CODE
LEFT JOIN INV_ITEM_GROUP ON item.[GROUP] = INV_ITEM_GROUP.CODE
LEFT JOIN INV_ITEM_TM ON item.TRADEMARK = INV_ITEM_TM.CODE
LEFT JOIN PAY_SUPPLIER ON tblStock.supplier_id = PAY_SUPPLIER.CODE


 ORDER BY[ITEM NAME]'
--Execute the Dynamic Pivot Query
EXEC sp_executesql @DynamicPivotQuery
RESULT:";
Model.DbFunctions.InsertUpdate(itemSuggestion_test);
        }
      public static void updateView()
       {
            string itemMasterViewSer = "if exists(select 1 from sys.views where name='itemDirectoryWithServ' and type='v') drop view itemDirectoryWithServ";
            Model.DbFunctions.InsertUpdate(itemMasterViewSer);
            itemMasterViewSer ="CREATE VIEW [dbo].[itemDirectoryWithServ]"
            + " AS  SELECT        CODE, DESC_ENG, DESC_ARB, TYPE, [GROUP], CATEGORY, TRADEMARK, SUPPLIER, COST_PRICE, LAST_COST_PRICE, SALE_PRICE, MINIMUM_QTY,  MAXIMUM_QTY, REORDER_QTY, FIRST_UNIT_CODE, FIRST_UNIT_BARCODE, CASE WHEN HAS_SECOND = 'Y' THEN 'Yes' ELSE 'No' END AS HAS_SECOND, SECOND_UNIT_CODE, SECOND_UNITS, SECOND_UNIT_BARCODE, CASE WHEN HAS_THIRD = 'Y' THEN 'Yes' ELSE 'No' END AS HAS_THIRD, THIRD_UNIT_CODE, "
            + "  THIRD_UNITS, THIRD_UNIT_BARCODE, CASE WHEN HAS_FOURTH = 'Y' THEN 'Yes' ELSE 'No' END AS HAS_FOURTH, FOURTH_UNIT_CODE, FOURTH_UNITS, "
            + " FOURTH_UNIT_BARCODE, CASE WHEN IN_ACTIVE = 'Y' THEN 'Yes' ELSE 'No' END AS IN_ACTIVE, TaxId, LOCATION, HASSERIAL, HASWARRENTY, PERIOD,"
            + "PERIODTYPE,PRODUCT_TYPE"
            + " FROM dbo.INV_ITEM_DIRECTORY";
                
            Model.DbFunctions.InsertUpdate(itemMasterViewSer);

            string itemMasterView = "	 if exists(select 1 from sys.views where name='itemDirectory' and type='v') drop view itemDirectory";
            Model.DbFunctions.InsertUpdate(itemMasterView);
            itemMasterView =@"CREATE VIEW[dbo].[itemDirectory]
               AS
               SELECT        CODE, DESC_ENG, DESC_ARB, TYPE, [GROUP], CATEGORY, TRADEMARK, SUPPLIER, COST_PRICE, LAST_COST_PRICE, SALE_PRICE, MINIMUM_QTY, 
               MAXIMUM_QTY, REORDER_QTY, FIRST_UNIT_CODE, FIRST_UNIT_BARCODE, CASE WHEN HAS_SECOND = 'Y' THEN 'Yes' ELSE 'No' END AS HAS_SECOND, 
               SECOND_UNIT_CODE, SECOND_UNITS, SECOND_UNIT_BARCODE, CASE WHEN HAS_THIRD = 'Y' THEN 'Yes' ELSE 'No' END AS HAS_THIRD, THIRD_UNIT_CODE, 
               THIRD_UNITS, THIRD_UNIT_BARCODE, CASE WHEN HAS_FOURTH = 'Y' THEN 'Yes' ELSE 'No' END AS HAS_FOURTH, FOURTH_UNIT_CODE, FOURTH_UNITS, 
               FOURTH_UNIT_BARCODE, CASE WHEN IN_ACTIVE = 'Y' THEN 'Yes' ELSE 'No' END AS IN_ACTIVE, TaxId, LOCATION, HASSERIAL, HASWARRENTY, PERIOD, 
               PERIODTYPE,PRODUCT_TYPE
               FROM  dbo.INV_ITEM_DIRECTORY WHERE PRODUCT_TYPE NOT IN('SRV')";
               Model.DbFunctions.InsertUpdate(itemMasterView);

        }
        public static void upgradeDatabase()



        {
            updateStoredProcedure();
            updateView();
        }
    }
}
