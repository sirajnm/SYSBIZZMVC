DECLARE @DynamicPivotQuery AS NVARCHAR(MAX),
        @PivotColumnNames AS NVARCHAR(MAX),
        @PivotSelectColumnNames AS NVARCHAR(MAX)
--Get distinct values of the PIVOT Column
SELECT @PivotColumnNames= ISNULL(@PivotColumnNames + ',','')
+ QUOTENAME(SAL_TYPE)
FROM (SELECT DISTINCT SAL_TYPE FROM INV_ITEM_PRICE) AS SALE_TYPE
--Get distinct values of the PIVOT Column with isnull
SELECT @PivotSelectColumnNames 
    = ISNULL(@PivotSelectColumnNames + ',','')
    + 'ISNULL(' + QUOTENAME(SAL_TYPE) + ', 0) AS '
    + QUOTENAME(SAL_TYPE)
FROM (SELECT DISTINCT SAL_TYPE FROM INV_ITEM_PRICE) AS SALE_TYPE
--Prepare the PIVOT query using the dynamic
SET @DynamicPivotQuery =
N'SELECT price_table.*, item.CODE AS [ITEM CODE], unit.BARCODE as Barcode, item.DESC_ENG AS [ITEM NAME], 
(tblStock.Qty/unit.PACK_SIZE) as [STOCK], item.DESC_ARB, CONCAT(item.PERIOD, '' '', item.PERIODTYPE) AS PERIOD, 
GEN_TAX_MASTER.TaxRate, INV_ITEM_TYPE.DESC_ENG as [TYPE], INV_ITEM_CATEGORY.DESC_ENG as CATEGORY, 
INV_ITEM_GROUP.DESC_ENG AS [GROUP], INV_ITEM_TM.DESC_ENG AS TRADEMARK, item.TaxId, 
item.HASSERIAL, tblStock.supplier_id, PAY_SUPPLIER.DESC_ENG as Supplier FROM 
INV_ITEM_DIRECTORY as item
LEFT JOIN (SELECT ITEM_CODE, UNIT_CODE, ' + @PivotSelectColumnNames + ' FROM INV_ITEM_PRICE PIVOT(SUM(PRICE) FOR SAL_TYPE IN (' + @PivotColumnNames + ')) AS PVTTable) AS price_table 
ON item.CODE = price_table.ITEM_CODE 
LEFT JOIN INV_ITEM_DIRECTORY_UNITS as unit ON item.code = unit.item_code AND price_table.UNIT_CODE = unit.unit_code 
LEFT JOIN tblStock ON item.code = tblStock.Item_id 
LEFT JOIN GEN_TAX_MASTER ON item.TaxId = GEN_TAX_MASTER.TaxId 
LEFT JOIN INV_ITEM_TYPE ON item.TYPE = INV_ITEM_TYPE.CODE 
LEFT JOIN INV_ITEM_CATEGORY ON item.CATEGORY = INV_ITEM_CATEGORY.CODE 
LEFT JOIN INV_ITEM_GROUP ON item.[GROUP] = INV_ITEM_GROUP.CODE 
LEFT JOIN INV_ITEM_TM ON item.TRADEMARK = INV_ITEM_TM.CODE 
LEFT JOIN PAY_SUPPLIER ON tblStock.supplier_id = PAY_SUPPLIER.CODE
ORDER BY [ITEM NAME] ASC'
--Execute the Dynamic Pivot Query
EXEC sp_executesql @DynamicPivotQuery
RESULT: