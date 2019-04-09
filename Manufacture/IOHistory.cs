using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Manufacture
{
    public partial class IOHistory : Form
    {
        string id, batch;
        public IOHistory(string id,string batch,string inward,string outward)
        {
            this.id = id;
            this.batch = batch;
            InitializeComponent();
            dgv_batchhistory.DataSource = GetHistory(inward, outward);
            lbl_batch.Text = batch;
            lbl_itemcode.Text = id;            
            //dgv_batchhistory.Rows[dgv_batchhistory.Rows.Count - 1].Cells["Reference No"].Style.BackColor = Color.Blue;
            //dgv_batchhistory.Rows[dgv_batchhistory.RowCount - 1].Cells["Reference No"].Value = "Total:";
            //dgv_batchhistory.Rows[dgv_batchhistory.RowCount - 1].Cells["Inward"].Value = inward;
            //dgv_batchhistory.Rows[dgv_batchhistory.RowCount - 1].Cells["Outward"].Value = outward;

        }

        public DataTable GetHistory(string inward,string outward)
        {
            string query = @"SELECT ROW_NUMBER() OVER (ORDER BY DATE ASC) AS NO,CONVERT(NVARCHAR(50), DATA.Date, 103) AS Date,Type as 'Mode of Transaction',CONVERT(NVARCHAR(50),Id) AS 'Reference No',CASE WHEN Type='Production' THEN qty WHEN Type='Sales Return' then qty WHEN Type='Stock In' then qty end as Inward,CASE WHEN Type='Sales' then qty WHEN Type='Stock Out' then qty end as Outward FROM (select ProductionId as Id,ProductionMaster.ProductionDate as Date,'Production' as Type,Batch,ItemCode,sum(Qty*PackSize) as qty from ProductionProducts left outer join ProductionMaster on ProductionMaster.Id=ProductionProducts.ProductionId group by ItemCode,Batch,ProductionId,ProductionDate 
UNION ALL
select INV_SALES_DTL.DOC_ID as Id,INV_SALES_HDR.DOC_DATE_GRE as Date,'Sales Return' as Type,BATCH,ITEM_CODE,sum(QUANTITY*UOM_QTY) as qty from INV_SALES_DTL left outer join INV_SALES_HDR on INV_SALES_DTL.DOC_NO=INV_SALES_HDR.DOC_NO where INV_SALES_DTL.DOC_TYPE='SAL.CSR'   group by ITEM_CODE,BATCH,INV_SALES_DTL.DOC_ID,DOC_DATE_GRE
UNION ALL
select INV_SALES_DTL.DOC_ID as Id,INV_SALES_HDR.DOC_DATE_GRE as Date,'Sales' as Type,batch,item_code,sum(quantity*uom_qty)as qty from INV_SALES_DTL left outer join INV_SALES_HDR on INV_SALES_DTL.DOC_NO=INV_SALES_HDR.DOC_NO where INV_SALES_DTL.DOC_TYPE in('SAL.CRD','SAL.CSS') group by BATCH,ITEM_CODE,INV_SALES_DTL.DOC_ID,DOC_DATE_GRE 
UNION ALL
select INV_STK_TRX_HDR.DOC_ID as Id,INV_STK_TRX_HDR.DOC_DATE_GRE as Date,'Stock In' as Type,batch,item_code,sum(quantity*uom_qty)as qty from INV_STK_TRX_DTL left outer join INV_STK_TRX_HDR ON INV_STK_TRX_HDR.DOC_NO=INV_STK_TRX_DTL.DOC_NO where INV_STK_TRX_DTL.DOC_TYPE in('INV.STK.IN','INV.STK.OPN') group by BATCH,ITEM_CODE,INV_STK_TRX_HDR.DOC_ID,DOC_DATE_GRE
UNION ALL
select INV_STK_TRX_HDR.DOC_ID as Id,INV_STK_TRX_HDR.DOC_DATE_GRE as Date,'Stock Out' as Type,batch,item_code,sum(quantity*uom_qty)as qty from INV_STK_TRX_DTL left outer join INV_STK_TRX_HDR ON INV_STK_TRX_HDR.DOC_NO=INV_STK_TRX_DTL.DOC_NO where INV_STK_TRX_DTL.DOC_TYPE in('INV.STK.OUT') group by BATCH,ITEM_CODE,INV_STK_TRX_HDR.DOC_ID,DOC_DATE_GRE) AS DATA WHERE DATA.ItemCode=@id AND DATA.Batch=@batch";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@batch", batch);
            DataTable dt= DbFunctions.GetDataTable(query, parameters);
            DataRow dr = dt.NewRow();
            dr["Reference No"] = "Total:";
            dr["Inward"] = inward;
            dr["Outward"] = outward;
            dt.Rows.Add(dr);
            return dt;            
        }

        private void IOHistory_Load(object sender, EventArgs e)
        {
            dgv_batchhistory.Rows[dgv_batchhistory.Rows.Count-1].DefaultCellStyle.ForeColor = Color.Red;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font(dgv_batchhistory.Font, FontStyle.Bold);
            dgv_batchhistory.Rows[dgv_batchhistory.Rows.Count - 1].DefaultCellStyle = style;
            dgv_batchhistory.Columns[0].Width = 50;
            dgv_batchhistory.Columns[1].Width = 120;
            dgv_batchhistory.Columns[2].Width = 150;
            dgv_batchhistory.Columns[3].Width = 60;
            dgv_batchhistory.Columns[4].Width = 60;
            dgv_batchhistory.Columns[5].Width = 60;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
