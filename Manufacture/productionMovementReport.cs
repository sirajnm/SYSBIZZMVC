using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory.Manufacture
{
 public class productionMovementReport
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();

        public DataTable getData()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = @"SELECT batch.ItemCode as CODE,A.DESC_ENG AS NAME,batch.batch as BATCH,isnull( productBatch.Qty,0)+isnull(sales_return.Qty,0)+isnull(stock_in.Qty,0)as INWARD,isnull(sales.qty,0)+isnull(stock_out.Qty,0)as OUTWARD FROM(select distinct(batch),itemCode from ProductionProducts)as batch left outer join (select batch,itemCode,sum(qty*PackSize )as qty from ProductionProducts group by Batch,ItemCode)as productBatch on batch.batch=productBatch.batch and batch.itemCode=productBatch.itemCode left outer join (select batch,item_code,sum(quantity*uom_qty)as qty from INV_SALES_DTL where DOC_TYPE='SAL.CSR' group by BATCH,ITEM_CODE)as sales_return on batch.itemCode=sales_return.item_code and batch.batch=sales_return.batch  left outer join (select batch,item_code,sum(quantity*uom_qty)as qty from INV_SALES_DTL where DOC_TYPE in('SAL.CRD','SAL.CSS') group by BATCH,ITEM_CODE)as SALES on batch.itemCode=SALES.item_code and batch.batch=SALES.batch left outer join (select batch,item_code,sum(quantity*uom_qty)as qty from INV_STK_TRX_DTL where DOC_TYPE in('INV.STK.IN','INV.STK.OPN') group by BATCH,ITEM_CODE)as STOCK_IN on batch.itemCode=STOCK_IN.item_code and batch.batch=STOCK_IN.batch left outer join (select batch,item_code,sum(quantity*uom_qty)as qty from INV_STK_TRX_DTL where DOC_TYPE in('INV.STK.OUT') group by BATCH,ITEM_CODE)as STOCK_OUT on batch.itemCode=STOCK_OUT.item_code and batch.batch=STOCK_OUT.batch left outer join INV_ITEM_DIRECTORY as A on A.CODE=batch.ItemCode ORDER BY batch.ItemCode";
                /*string query = @"SELECT CASE WHEN ROW_NUMBER() OVER (PARTITION BY batch.ItemCode ORDER BY batch.batch) = 1 THEN batch.ItemCode ELSE '' END AS CODE,CASE WHEN ROW_NUMBER() OVER (PARTITION BY batch.ItemCode
ORDER BY batch.batch) = 1 THEN A.DESC_ENG ELSE '' END AS NAME,batch.batch as BATCH,isnull( productBatch.Qty,0)+isnull(sales_return.Qty,0)+isnull(stock_in.Qty,0)as INWARD,isnull(sales.qty,0)+isnull(stock_out.Qty,0)as OUTWARD FROM(select distinct(batch),itemCode from ProductionProducts)as batch left outer join (select batch,itemCode,sum(qty*PackSize )as qty from ProductionProducts group by Batch,ItemCode)as productBatch on batch.batch=productBatch.batch and batch.itemCode=productBatch.itemCode left outer join (select batch,item_code,sum(quantity*uom_qty)as qty from INV_SALES_DTL where DOC_TYPE='SAL.CSR' group by BATCH,ITEM_CODE)as sales_return on batch.itemCode=sales_return.item_code and batch.batch=sales_return.batch  left outer join (select batch,item_code,sum(quantity*uom_qty)as qty from INV_SALES_DTL where DOC_TYPE in('SAL.CRD','SAL.CSS') group by BATCH,ITEM_CODE)as SALES on batch.itemCode=SALES.item_code and batch.batch=SALES.batch left outer join (select batch,item_code,sum(quantity*uom_qty)as qty from INV_STK_TRX_DTL where DOC_TYPE in('INV.STK.IN','INV.STK.OPN') group by BATCH,ITEM_CODE)as STOCK_IN on batch.itemCode=STOCK_IN.item_code and batch.batch=STOCK_IN.batch left outer join (select batch,item_code,sum(quantity*uom_qty)as qty from INV_STK_TRX_DTL where DOC_TYPE in('INV.STK.OUT') group by BATCH,ITEM_CODE)as STOCK_OUT on batch.itemCode=STOCK_OUT.item_code and batch.batch=STOCK_OUT.batch left outer join INV_ITEM_DIRECTORY as A on A.CODE=batch.ItemCode ORDER BY batch.ItemCode";
                    //if (conn.State == ConnectionState.Closed)
                 * */
                //    conn.Open();
                //cmd.Connection = conn;
                //SqlDataAdapter Adp = new SqlDataAdapter();
                //Adp.SelectCommand = cmd;
                //Adp.Fill(dt);
                dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
              //  MessageBox.Show("Error Getting Activation Data \n Error: " + ex.Message);
            }
            return dt;
        }

    }
}
