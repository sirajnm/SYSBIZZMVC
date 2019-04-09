using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
namespace Sys_Sols_Inventory.Model
{
   public class ClsTemplate
    {
      // static SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
     //  SqlCommand cmd = new SqlCommand();
       string query = "";
       public DataTable getTemplate()
       {
          // DataTable dt = new DataTable();
          // conn.Open();
           query = "SELECT TEMPLATE FROM INVOICE_A4_GENERAL";
          // SqlDataAdapter ad = new SqlDataAdapter(cmd);
         //  ad.Fill(dt);
         //  conn.Close();
           return DbFunctions.GetDataTable(query);
            
       }
       public DataTable getColumnHDR()
       {
           //DataTable dt = new DataTable();
           //conn.Open();
          query= "select DISTINCT COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='INV_SALES_HDR'";
          return DbFunctions.GetDataTable(query);
           //SqlDataAdapter ad = new SqlDataAdapter(cmd);
           //ad.Fill(dt);
           //conn.Close();
           //return dt;
       }
       public DataTable getColumnDTL()
       {
           //DataTable dt = new DataTable();
           //conn.Open();
           query = "select DISTINCT COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='INV_SALES_DTL'";
           //SqlDataAdapter ad = new SqlDataAdapter(cmd);
           //ad.Fill(dt);
           //conn.Close();
           //return dt;
           return DbFunctions.GetDataTable(query);
       }
      public bool IsDynamic()
       {
           //conn.Open();
           query = "SELECT IsDynamic FROM INVOICE_SETTINGS";
           //bool value = Convert.ToBoolean(cmd.ExecuteScalar());

          // bool value = Convert.ToBoolean(DbFunctions.GetAValue(query));

           bool value = Convert.ToBoolean(DbFunctions.GetAValue(query));

           // conn.Close();
           return value;
       }
     public int getWidth(string template)
      {
         // conn.Open();
          query ="select PAPER_WIDTH from  INVOICE_a4_general where template='" + template + "'";
          int value = Convert.ToInt32(DbFunctions.GetAValue(query));
         // conn.Close();
          return value;
      }
     public int getHeight(string template)
      {
          //conn.Open();
          query ="select PAPER_HEIGHT from  INVOICE_a4_general where template='" + template + "'";
          int value = Convert.ToInt32(DbFunctions.GetAValue(query));
          //conn.Close();
          return value;
      }
     public int getHeightThermal(string template, DataGridView dgItems)
     {
        // conn.Open();
         query ="select * from  INVOICE_a4_general where template='" + template + "'";
         DataTable templateDesign = new DataTable();
         //SqlDataAdapter ad = new SqlDataAdapter(cmd);
         //ad.Fill(templateDesign);
         //conn.Close();
         templateDesign = DbFunctions.GetDataTable(query);
         int TotalHeight = 0;
         double Header = Convert.ToDouble(templateDesign.Rows[0]["TOP"]);
         double Footer = Convert.ToDouble(templateDesign.Rows[0]["FORM_HEIGHT"]) - Convert.ToDouble(templateDesign.Rows[0]["BOTTOM"]);
         double Rowheight = Convert.ToDouble(templateDesign.Rows[0]["Row_Height"]);
         int GridHeight = Convert.ToInt32(templateDesign.Rows[0]["height"]);
         int gridcount = getGridHeight(Convert.ToDouble(templateDesign.Rows[0]["paper_width"]) / Convert.ToDouble(templateDesign.Rows[0]["form_width"]), Convert.ToDouble(templateDesign.Rows[0]["right"]),dgItems,template);
         TotalHeight = Convert.ToInt32((gridcount * (Rowheight + 10)) + Convert.ToDouble(templateDesign.Rows[0]["rowheader_height"]) + 5 + Header + Footer);
         //TotalHeight += Rowheight;
         return TotalHeight;
     }
     int getGridHeight(double width, double right, DataGridView dgItems, string template)
    {
        int RowsCount = dgItems.RowCount;
        int count = 0;
        double startx = getStartColumn(template);
        Font detailsfont = getFont(template);
        DataTable ClmDetails = ColumnDetails(template);
        for (int i = 0; i < RowsCount; i++)
        {
            string printname = dgItems.Rows[i].Cells["cName"].Value.ToString();
            int TotalStringwidth = TextRenderer.MeasureText(printname, detailsfont).Width;
            int stringLength = printname.Length;
            if (TotalStringwidth <= Convert.ToInt32(ClmDetails.Rows[0]["width"]) * width)
            {
                count++;
            }
            else
            {

                double TotalColumn = (right * width) - (startx * width);
                int getLength = Convert.ToInt32(stringLength * TotalColumn / TotalStringwidth);

                if (getLength < stringLength)
                {
                    count = count++;
                }
                else
                {
                    count = count + 2;
                }

            }

        }
        return count;
    }
   public Font getFont(string template)
    {
       // conn.Open();
        query ="SELECT * from INVOICE_A4_GENERAL WHERE TEMPLATE='" + template + "'";
        DataTable getFont = new DataTable();
       // SqlDataAdapter adptr = new SqlDataAdapter(cmd);
       // adptr.Fill(getFont);
       // conn.Close();
        getFont = DbFunctions.GetDataTable(query);
        FontStyle fs1 = new FontStyle();
        Font detailsfont = new Font(getFont.Rows[0]["details_fontname"].ToString(),
                    float.Parse(getFont.Rows[0]["details_fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
        return detailsfont;
    }
   public DataTable ColumnDetails(string template)
    {
        //conn.Open();
        query ="SELECT * from INVOICE_A4columns WHERE TEMPLATE='" + template + "' and NAME='ITEM_DESC_ENG'";
       // DataTable getData = new DataTable();
       // SqlDataAdapter adptr = new SqlDataAdapter(cmd);
       // adptr.Fill(getData);
       // conn.Close();
       
        return DbFunctions .GetDataTable(query);
    }
   public double getStartColumn(string template)
    {
       // conn.Open();
        query ="SELECT startx from INVOICE_A4columns WHERE TEMPLATE='" + template + "' and NAME='ITEM_DESC_ENG'";
        double val = Convert.ToDouble(DbFunctions.GetAValue(query));
       // conn.Close();
        return val;
    }
    }
}
