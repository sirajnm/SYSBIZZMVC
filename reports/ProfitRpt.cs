using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace Sys_Sols_Inventory.reports
{
    public partial class ProfitRpt : Form
    {
        public ProfitRpt()
        {
            InitializeComponent();
        }
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        decimal grprofit = 0;
        decimal inv_profit = 0;
        int refresh = 0;
        private void Find_Click(object sender, EventArgs e)
        {
            inv_profit = 0;
            try
            {
                if (cb_datserch.Checked == true)
                {

                    DG_REPORT.Rows.Clear();
                    string dtfrom, dtto;
                    
                    dtfrom = Date_From.Value.ToShortDateString();
                    dtto = Date_To.Value.ToShortDateString();
                    string query = "";
                    query = @"SELECT        CASE WHEN ROW_NUMBER() OVER(PARTITION BY INV_SALES_HDR.DOC_ID,INV_SALES_HDR.SALE_TYPE  ORDER BY INV_SALES_HDR.DOC_ID) = 1   THEN INV_SALES_HDR.DOC_ID ELSE NULL END AS  [Invoice no], INV_SALES_HDR.SALE_TYPE, INV_SALES_HDR.DOC_DATE_GRE, INV_SALES_DTL.ITEM_DESC_ENG, INV_SALES_DTL.QUANTITY,(ITEM_TOTAL/(INV_SALES_DTL.QUANTITY*INV_SALES_DTL.UOM_QTY)) as[SALE PRICE],((QUANTITY*INV_SALES_DTL.UOM_QTY)*(ITEM_TOTAL/(INV_SALES_DTL.QUANTITY*INV_SALES_DTL.UOM_QTY)))-ITEM_DISCOUNT AS [NET SALE], PUR_RATE.PRICE AS [COST PRICE],PUR_RATE.PRICE*(QUANTITY*UOM_QTY) as [NET COST],((QUANTITY*(ITEM_TOTAL/(INV_SALES_DTL.QUANTITY*INV_SALES_DTL.UOM_QTY))-ITEM_DISCOUNT)-PUR_RATE.PRICE*(QUANTITY*UOM_QTY)) AS PROFIT
FROM INV_SALES_DTL INNER JOIN
                  (SELECT ITEM_CODE, PRICE
                    FROM            INV_ITEM_PRICE_DF
                    WHERE        (SAL_TYPE = 'PUR')) AS PUR_RATE ON PUR_RATE.ITEM_CODE = INV_SALES_DTL.ITEM_CODE INNER JOIN
                         INV_SALES_HDR ON INV_SALES_DTL.DOC_NO = INV_SALES_HDR.DOC_NO where INV_SALES_DTL.DOC_TYPE <> 'SAL.CSR' and INV_SALES_HDR.DOC_TYPE <> 'SAL.CSR' and INV_SALES_DTL.FLAGDEL <> 'FALSE' AND INV_SALES_HDR.FLAGDEL <> 'FALSE' AND INV_SALES_DTL.DOC_ID NOT IN(select DOC_ID FROM INV_SALES_DTL WHERE DOC_TYPE = 'SAL.CSR') AND (CONVERT(VARCHAR, INV_SALES_HDR.DOC_DATE_GRE,101) >= @d1) AND (CONVERT(VARCHAR, INV_SALES_HDR.DOC_DATE_GRE,101) <= @d2) ORDER BY INV_SALES_HDR.SALE_TYPE,INV_SALES_HDR.DOC_ID";
                    
                    

                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("@d1", Date_From.Value);
                    parameter.Add("@d2", Date_To.Value);
                    DataTable dt1 = new DataTable();

                    dt1 = Model.DbFunctions.GetDataTable(query, parameter);
                    string query1 = @"SELECT PROFIT FROM (SELECT  INV_SALES_HDR.DOC_ID,INV_SALES_HDR.SALE_TYPE,SUM(((QUANTITY*(ITEM_TOTAL/(INV_SALES_DTL.QUANTITY*INV_SALES_DTL.UOM_QTY))-ITEM_DISCOUNT)-PUR_RATE.PRICE*(QUANTITY*UOM_QTY))) AS PROFIT
FROM            INV_SALES_DTL INNER JOIN
                             (SELECT        ITEM_CODE, PRICE
                               FROM            INV_ITEM_PRICE_DF
                               WHERE        (SAL_TYPE = 'PUR')) AS PUR_RATE ON PUR_RATE.ITEM_CODE = INV_SALES_DTL.ITEM_CODE INNER JOIN
                         INV_SALES_HDR ON INV_SALES_DTL.DOC_NO = INV_SALES_HDR.DOC_NO where INV_SALES_DTL.DOC_TYPE<>'SAL.CSR' and INV_SALES_HDR.DOC_TYPE<>'SAL.CSR' and INV_SALES_DTL.FLAGDEL<>'FALSE' AND INV_SALES_HDR.FLAGDEL<>'FALSE' AND INV_SALES_DTL.DOC_ID     NOT IN (select DOC_ID FROM INV_SALES_DTL WHERE DOC_TYPE='SAL.CSR') AND (CONVERT(VARCHAR, INV_SALES_HDR.DOC_DATE_GRE,101) >=@d1) AND (CONVERT(VARCHAR, INV_SALES_HDR.DOC_DATE_GRE,101) <= @d2) GROUP BY INV_SALES_HDR.DOC_NO,INV_SALES_HDR.DOC_ID,INV_SALES_HDR.SALE_TYPE) AS PROFIT_TB ORDER BY SALE_TYPE,DOC_ID ";
                    //cmd1.CommandType = CommandType.Text;
                    //SqlDataAdapter Adap1 = new SqlDataAdapter();
                    //Adap1.SelectCommand = cmd1;
                    DataTable dt2 = new DataTable();
                    dt2 = Model.DbFunctions.GetDataTable(query1, parameter);
                    try
                    {
                        int incrimenter = 0;
                        string pr = "";

                        for (int j = 0; j < dt1.Rows.Count; ++j)
                        {
                            if (j == (dt1.Rows.Count - 1))
                            {
                                pr = "";
                                pr = Convert.ToString(dt2.Rows[incrimenter][0]);
                            }
                            if (j != (dt1.Rows.Count - 1))
                            {
                                if ((dt1.Rows[j]["Invoice no"].ToString() == "" && dt1.Rows[j + 1]["Invoice no"].ToString() != "") || (dt1.Rows[j]["Invoice no"].ToString() != "" && dt1.Rows[j + 1]["Invoice no"].ToString() != ""))
                                {
                                    pr = Convert.ToString(dt2.Rows[incrimenter][0]);
                                    incrimenter++;

                                }
                                else
                                {

                                    pr = "";

                                }
                            }
                           
                            DG_REPORT.Rows.Add(dt1.Rows[j][0], dt1.Rows[j][1], dt1.Rows[j][3], dt1.Rows[j][2], dt1.Rows[j][4], dt1.Rows[j][5], dt1.Rows[j][6], dt1.Rows[j][7], dt1.Rows[j][8], dt1.Rows[j][9], pr);
                            //DG_REPORT.Rows.Add(dt1.Rows[j][0], dt1.Rows[j][1], dt1.Rows[j][3], dt1.Rows[j][2], dt1.Rows[j][4], dt1.Rows[j][5], dt1.Rows[j][6], dt1.Rows[j][7], dt1.Rows[j][8], pr, dt1.Rows[j][9]);
                            if (pr == "" || pr == "null")
                            {

                            }
                            else
                            {
                                DG_REPORT.Rows.Add();
                            }

                        }
                    }
                    catch
                    {
                    }
                    decimal netprofit = 0;



                    decimal Qty = 0, totalSale = 0, totalpur = 0;
                    for (int i = 0; i < (dt1.Rows.Count); ++i)
                    {
                        Qty += Convert.ToDecimal(dt1.Rows[i]["Quantity"].ToString());
                        totalSale += Convert.ToDecimal(dt1.Rows[i]["Net Sale"].ToString());
                        totalpur += Convert.ToDecimal(dt1.Rows[i]["Net Cost"].ToString());
                    }

                    for (int i = 0; i < (dt2.Rows.Count); ++i)
                    {
                        grprofit += Convert.ToDecimal(dt2.Rows[i]["profit"].ToString());
                    }

                    DG_REPORT.Rows.Add("","", "Total", "", Qty.ToString(), "", totalSale.ToString(), "", totalpur.ToString(), "", grprofit.ToString());

                    DG_REPORT.Rows[DG_REPORT.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    DG_REPORT.Rows.Add("","", "NET PROFIT :", "", "", "", "", "", "", "", grprofit.ToString());
                    string inv_no;
                    grprofit = 0;
                }
                else
                {
                    DG_REPORT.Rows.Clear();
                    DateTime dtfrom, dtto;
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //}

                    //else
                    //{
                    //    conn.Open();
                    //}
                    //cmd.Connection = conn;
                    dtfrom = Date_From.Value.Date;
                    dtto = Date_To.Value.Date;


                    String query = @"SELECT        CASE WHEN ROW_NUMBER() OVER(PARTITION BY INV_SALES_HDR.DOC_ID,INV_SALES_HDR.SALE_TYPE  ORDER BY INV_SALES_HDR.DOC_ID) = 1   THEN INV_SALES_HDR.DOC_ID ELSE NULL END AS  [Invoice no], INV_SALES_HDR.SALE_TYPE, INV_SALES_HDR.DOC_DATE_GRE, INV_SALES_DTL.ITEM_DESC_ENG, INV_SALES_DTL.QUANTITY,(ITEM_TOTAL/(INV_SALES_DTL.QUANTITY*INV_SALES_DTL.UOM_QTY)) as[SALE PRICE],((QUANTITY*INV_SALES_DTL.UOM_QTY)*(ITEM_TOTAL/(INV_SALES_DTL.QUANTITY*INV_SALES_DTL.UOM_QTY)))-ITEM_DISCOUNT AS [NET SALE], PUR_RATE.PRICE AS [COST PRICE],PUR_RATE.PRICE*(QUANTITY*UOM_QTY) as [NET COST],((QUANTITY*(ITEM_TOTAL/(INV_SALES_DTL.QUANTITY*INV_SALES_DTL.UOM_QTY))-ITEM_DISCOUNT)-PUR_RATE.PRICE*(QUANTITY*UOM_QTY)) AS PROFIT
FROM INV_SALES_DTL INNER JOIN
                  (SELECT ITEM_CODE, PRICE
                    FROM            INV_ITEM_PRICE_DF
                    WHERE        (SAL_TYPE = 'PUR')) AS PUR_RATE ON PUR_RATE.ITEM_CODE = INV_SALES_DTL.ITEM_CODE INNER JOIN
                         INV_SALES_HDR ON INV_SALES_DTL.DOC_NO = INV_SALES_HDR.DOC_NO where INV_SALES_DTL.DOC_TYPE <> 'SAL.CSR' and INV_SALES_HDR.DOC_TYPE <> 'SAL.CSR' and INV_SALES_DTL.FLAGDEL <> 'FALSE' AND INV_SALES_HDR.FLAGDEL <> 'FALSE' AND INV_SALES_DTL.DOC_ID NOT IN(select DOC_ID FROM INV_SALES_DTL WHERE DOC_TYPE = 'SAL.CSR')
ORDER BY INV_SALES_HDR.SALE_TYPE,INV_SALES_HDR.DOC_ID";
                    
                    DataTable dt = new DataTable();
                    // Adap.Fill(dt);
                    dt = Model.DbFunctions.GetDataTable(query);
string query1 = @"SELECT PROFIT FROM (SELECT  INV_SALES_HDR.DOC_ID,INV_SALES_HDR.SALE_TYPE,SUM(((QUANTITY*(ITEM_TOTAL/(INV_SALES_DTL.QUANTITY*INV_SALES_DTL.UOM_QTY))-ITEM_DISCOUNT)-PUR_RATE.PRICE*(QUANTITY*UOM_QTY))) AS PROFIT
FROM            INV_SALES_DTL INNER JOIN
                             (SELECT        ITEM_CODE, PRICE
                               FROM            INV_ITEM_PRICE_DF
                               WHERE        (SAL_TYPE = 'PUR')) AS PUR_RATE ON PUR_RATE.ITEM_CODE = INV_SALES_DTL.ITEM_CODE INNER JOIN
                         INV_SALES_HDR ON INV_SALES_DTL.DOC_NO = INV_SALES_HDR.DOC_NO where INV_SALES_DTL.DOC_TYPE<>'SAL.CSR' and INV_SALES_HDR.DOC_TYPE<>'SAL.CSR' and INV_SALES_DTL.FLAGDEL<>'FALSE' AND INV_SALES_HDR.FLAGDEL<>'FALSE' AND INV_SALES_DTL.DOC_ID     NOT IN (select DOC_ID FROM INV_SALES_DTL WHERE DOC_TYPE='SAL.CSR')
GROUP BY INV_SALES_HDR.DOC_NO,INV_SALES_HDR.DOC_ID,INV_SALES_HDR.SALE_TYPE) AS PROFIT_TB ORDER BY SALE_TYPE,DOC_ID ";
                    

                    DataTable dt2 = new DataTable();
                    //Adap1.Fill(dt2);
                    dt2 = Model.DbFunctions.GetDataTable(query1);
                    try
                    {
                        int incrimenter = 0;
                        string pr = "";
                        string IN = dt.Rows[0][0].ToString();
                        for (int j = 0; j < dt.Rows.Count; ++j)
                        {
                            if (j == (dt.Rows.Count - 1))
                            {
                                pr = "";
                                pr = Convert.ToString(dt2.Rows[incrimenter][0]);
                            }
                            if (j != (dt.Rows.Count - 1))
                            {
                                if ((dt.Rows[j]["Invoice no"].ToString() == "" && dt.Rows[j + 1]["Invoice no"].ToString() != "") || (dt.Rows[j]["Invoice no"].ToString() != "" && dt.Rows[j + 1]["Invoice no"].ToString() != ""))
                                {
                                    pr = Convert.ToString(dt2.Rows[incrimenter][0]);
                                    incrimenter++;

                                }
                                else
                                {

                                    pr = "";

                                }
                            }

                            DG_REPORT.Rows.Add(dt.Rows[j][0], dt.Rows[j][1], dt.Rows[j][3], dt.Rows[j][2], dt.Rows[j][4], dt.Rows[j][5], dt.Rows[j][6], dt.Rows[j][7], dt.Rows[j][8], dt.Rows[j][9], pr);
                            if (pr == "" || pr == "null")
                            {

                            }
                            else
                            {
                                DG_REPORT.Rows.Add();
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    decimal netprofit = 0;



                    decimal Qty = 0, totalSale = 0, totalpur = 0;
                    for (int i = 0; i < (dt.Rows.Count); ++i)
                    {
                        Qty += Convert.ToDecimal(dt.Rows[i]["Quantity"].ToString());
                        totalSale += Convert.ToDecimal(dt.Rows[i]["Net Sale"].ToString());
                        totalpur += Convert.ToDecimal(dt.Rows[i]["Net Cost"].ToString());
                    }

                    for (int i = 0; i < (dt2.Rows.Count); ++i)
                    {
                        grprofit += Convert.ToDecimal(dt2.Rows[i]["profit"].ToString());
                    }

                    DG_REPORT.Rows.Add("", "", "Total", "", Qty.ToString(), "", totalSale.ToString(), "", totalpur.ToString(), "", grprofit.ToString());

                    System.Windows.Forms.DataGridViewCellStyle norStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    norStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
                    DG_REPORT.Rows[DG_REPORT.Rows.Count - 1].DefaultCellStyle = norStyle;

                    DG_REPORT.Rows[DG_REPORT.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    DG_REPORT.Rows.Add("","", "NET PROFIT :", "", "", "", "", "", "", "", grprofit.ToString());


                    string inv_no;
                    grprofit = 0;
                    //this.DG_REPORT.RowsDefaultCellStyle.BackColor = Color.White;
                    //this.DG_REPORT.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
                }
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            if (DG_REPORT.Rows.Count > 0)
            {
                System.Windows.Forms.DataGridViewCellStyle norStyle = new System.Windows.Forms.DataGridViewCellStyle();
                norStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
                DG_REPORT.Rows[DG_REPORT.Rows.Count - 2].DefaultCellStyle = norStyle;

                DG_REPORT.Rows[DG_REPORT.Rows.Count - 2].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;


                DG_REPORT.Rows[DG_REPORT.Rows.Count - 1].DefaultCellStyle = norStyle;

                DG_REPORT.Rows[DG_REPORT.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;               
                

                DG_REPORT.Columns["Item_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                DG_REPORT.Columns["Datee"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                DG_REPORT.Columns["SaleType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                DG_REPORT.Columns["Invoice_no"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                DG_REPORT.Columns["Sale_price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DG_REPORT.Columns["colNetSale"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DG_REPORT.Columns["Pur_price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DG_REPORT.Columns["colNetCost"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DG_REPORT.Columns["Profit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DG_REPORT.Columns["invpr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DG_REPORT.Columns["Quatity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


                DG_REPORT.Columns["Sale_price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DG_REPORT.Columns["colNetSale"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DG_REPORT.Columns["Pur_price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DG_REPORT.Columns["colNetCost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DG_REPORT.Columns["Profit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DG_REPORT.Columns["invpr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DG_REPORT.Columns["Quatity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

        }
        decimal subt = 0;
       
        private void DG_REPORT_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DG_REPORT.Rows.Count > 0)
            {
                //try
                //{
                //    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                //    mdi.maindocpanel.Pages.Add(kp);
                //    string docno = DG_REPORT.CurrentRow.Cells["DocNo"].Value.ToString();
                //    SalesQ m = new SalesQ(docno);
                //    m.Show();
                //    m.BackColor = Color.White;
                //    m.TopLevel = false;
                //    kp.Controls.Add(m);
                //    m.Dock = DockStyle.Fill;
                //    kp.Text = m.Text;
                //    kp.Name = "PurchaseMaster";
                //    m.FormBorderStyle = FormBorderStyle.None;
                //    //kp.Focus();
                //    mdi.maindocpanel.SelectedPage = kp;
                //    // m.Focus();

                //}
                //catch
                //{
                //}
            }
        }

        private void DG_REPORT_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void DG_REPORT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DG_REPORT_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow Myrow in DG_REPORT.Rows)
            {            //Here 2 cell is target value and 1 cell is Volume
                
                 
                //if (Convert.ToInt32(Myrow.Cells[5].Value) < 0)// Or your condition 
                //{
                //    Myrow.DefaultCellStyle.BackColor = Color.Red;
                //}
                //else
                //{
                //  //  Myrow.DefaultCellStyle.BackColor = Color.DarkGray;
                //}
            }
        }

        private void ProfitRpt_Load(object sender, EventArgs e)
        {
            Find.PerformClick();

        }

        private void DG_REPORT_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
        }

        private void cb_datserch_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_datserch.Checked)
            {
                Date_From.Enabled = true;
                Date_To.Enabled = true;
            }
            else
            {
                Date_From.Enabled = false;
                Date_To.Enabled = false;
            }
            
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DG_REPORT_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            if (e.ColumnIndex != 0 && e.ColumnIndex != 1 && e.ColumnIndex != 2 && e.ColumnIndex != 3)
            {
                e.CellStyle.Format = "N4";
            }
        }
    }
}
