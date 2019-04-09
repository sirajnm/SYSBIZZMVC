using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sys_Sols_Inventory.Model;
using System.Windows;

namespace Sys_Sols_Inventory.Class
{
    class EmpLog
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();

        public string Emp_Id { get; set; }
        public string Id { get; set; }
        public DateTime LogoutTime { get; set; }
        public string Branch { get; set; }
        string query;
        public void InsertEmplog()
        {
            query = "insert into Lg_EmpLog(Emp_Id) values(@Emp_Id)";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@Emp_Id", Emp_Id);
            DbFunctions.InsertUpdate(query, Parameter);
        }

        public string GetUpdatelogId()
        {
            query = " SELECT MAX(Id) AS Id FROM Lg_EmpLog WHERE Emp_Id = @Emp_Id";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@Emp_Id", Emp_Id);
            string id = Convert.ToString(DbFunctions.GetAValue(query,Parameter));
            return id;
        }

        public DateTime getlogintime()
        {

            DateTime date=DateTime.Now;
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
               
                query = " SELECT LoginTime AS Id FROM Lg_EmpLog WHERE Id = @Id";
                Parameter.Add("@Id", Id);
                
                date = Convert.ToDateTime(DbFunctions.GetAValue(query,Parameter));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
            return date;
        }

        public void UpdateEmplog()
        {
            try
            {
                query = "update Lg_EmpLog set LogoutTime=@LogoutTime where Id=@Id";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@Id", Id);
                Parameter.Add("@LogoutTime", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                DbFunctions.InsertUpdate(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
    }
}
