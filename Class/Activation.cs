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
    class Activation
    {
       // private SqlConnection conn = new SqlConnection("");
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        public int ActivationId { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
        public string Keys { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }

        string query;

        public void InsertEnryptDate()
        {
            query = "insert into Tbl_Activation (Date,Value, Data, Status)Values(@Date,@Value, @Data, @Status) ";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@Date", Date);
            Parameter.Add("@Value", Value);
            Parameter.Add("@Data", Data);
            Parameter.Add("@Status", Status);
            DbFunctions.InsertUpdate(query, Parameter);            
        }

        public void UpdateActivation()
        {
            query = "Update Tbl_Activation set Date=@Date, Value=@Value ";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@Date", Date);
            Parameter.Add("@Value", Value);
            try
            {
                DbFunctions.InsertUpdate(query,Parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        public void ExtendActivation()
        {
            query = "Update Tbl_Activation set Date=@Date, Value=@Value,Status=@Status ";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@Date", Date);
            Parameter.Add("@Value", Value);
            Parameter.Add("@Status", Status);

            try
            {
                DbFunctions.InsertUpdate(query, Parameter);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        public DataTable GetDicrypt()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT * FROM Tbl_Activation";
                dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Getting Activation Data \n Error: "+ex.Message);
            }
            return dt;
        }

        public int DetachDB(string path)
        {
            int result=0;
            query = "EXEC sp_detach_db '"+ path+ "\\Data\\AIN_INVENTORY.mdf', 'true';";
            try
            {                
                result = DbFunctions.InsertUpdate(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
            return result;
        }

    }    
}
