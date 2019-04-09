using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Class
{
    class ChartOfAccount
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        public string UNDER { get; set; }
        string query;

        public DataTable SelectUnder()
        {
            DataTable dt=new DataTable();
            try
            {
                query = "Acc_Chart";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@UNDER", UNDER);
                Parameter.Add("@COMMAND", "S1");
                dt = DbFunctions.GetDataTableProcedure(query, Parameter);
            }
            catch (Exception ex)
            {
                
            }
            
            return dt;
        }

        public SqlDataReader SelecTop()
        {
            SqlDataReader dt=null ;
            try
            {
                query = "Acc_Chart";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();               
                Parameter.Add("@COMMAND", "S2");
                dt = DbFunctions.GetDataReaderProcedure(query, Parameter);               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }           
            return dt;
        }
    }
}
