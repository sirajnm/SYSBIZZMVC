using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Sys_Sols_Inventory.Model;
using System.Windows;

namespace Sys_Sols_Inventory.Class
{
    class cls_backup
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();

        public int id { get; set; }
        public string data_source { get; set; }
        public string user_id { get; set; }
        public string password { get; set; }
        public string db { get; set; }
        public string path { get; set; }

        string query;

        public void Update_backup(int id)
        {

            query = "update tbl_backup set data_source=@data_source,user_id=@user_id,pass=@password,db=@db,path=@path where id=@id";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@id", id);
            Parameter.Add("@data_source", data_source);
            Parameter.Add("@user_id", user_id);
            Parameter.Add("@password", password);
            Parameter.Add("@db",db);
            Parameter.Add("@path", path);
            try
            {
                DbFunctions.InsertUpdate(query, Parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }   
    }
}
