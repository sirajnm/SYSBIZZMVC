using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Sys_Sols_Inventory.Model;
using System.Windows;

namespace Sys_Sols_Inventory.Class
{
    
    class DateSettings
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();        
        public DateTime Date { get; set; }
        public int days { get; set; }
        public string Type { get; set; }
        public string PeriodType { get; set; }
        public bool HasAccessLimit { get; set; }

        string query;

        public DataTable getdatdetails()
        {
            DataTable dt = new DataTable();
            try
            {                
                query = "Select * from tbl_DateLimit";
                dt=DbFunctions.GetDataTable(query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public void InsertDateDetails()
        {
            query = "Update tbl_DateLimit set Date=@Date,days=@days,Type=@Type,PeriodType=@PeriodType";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Date", Date);
            parameters.Add("@days", days);
            parameters.Add("@Type", Type);
            parameters.Add("@PeriodType", PeriodType);
            try
            {
                DbFunctions.InsertUpdate(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void updateAccessLimit()
        {
            query = "update SYS_SETUP set HasAccessLimit=@HasAccessLimit ";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@HasAccessLimit", HasAccessLimit);
            try
            {
                DbFunctions.InsertUpdate(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


}
