using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Sys_Sols_Inventory.Model
{
    class POS_MenuLineRepo
    {
        public static List<POS_MenuLine> POS_MenuLines()
        {
            string query = "Select * from POS_Menu";
            DataTable dt = DbFunctions.GetDataTable(query);
            List<POS_MenuLine> MenuLines = new List<POS_MenuLine>();
            foreach (DataRow dr in dt.Rows)
            {
                POS_MenuLine menuline = new POS_MenuLine();
                menuline.MenuHeader = dr["MenuHeader"].ToString();
                menuline.MenuCommand = dr["MenuCommand"].ToString();
                menuline.MenuDescription = dr["MenuDescription"].ToString();
                menuline.MenuColor = dr["MenuColor"].ToString();
                menuline.MenuSortOrder = Convert.ToInt16(dr["MenuSortOrder"]);
                menuline.MenuLineSize = Convert.ToInt16(dr["MenuLineSize"]);
                MenuLines.Add(menuline);
            }
            return MenuLines;
        }

        public static List<POS_MenuLine> POS_MenuLinesSales()
        {
            string query = "Select * from POS_Menu where MenuHeader = 'Sales'";
            DataTable dt = DbFunctions.GetDataTable(query);
            List<POS_MenuLine> MenuLines = new List<POS_MenuLine>();
            foreach (DataRow dr in dt.Rows)
            {
                POS_MenuLine menuline = new POS_MenuLine();
                menuline.MenuHeader = dr["MenuHeader"].ToString();
                menuline.MenuCommand = dr["MenuCommand"].ToString();
                menuline.MenuDescription = dr["MenuDescription"].ToString();
                menuline.MenuColor = dr["MenuColor"].ToString();
                menuline.MenuSortOrder = Convert.ToInt16(dr["MenuSortOrder"]);
                menuline.MenuLineSize = Convert.ToInt16(dr["MenuLineSize"]);
                MenuLines.Add(menuline);
            }
            return MenuLines;
        }

    }
}
