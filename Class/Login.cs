using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Class
{
    class Login
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        public int Userid;
        public int Userid1
        {
            get { return Userid; }
            set { Userid = value; }
        }
        private int Empid;
        public int Empid1
        {
            get { return Empid; }
            set { Empid = value; }
        }
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string usertype;
        public string Usertype
        {
            get { return usertype; }
            set { usertype = value; }
        }
        public int Theam { get; set; }
        public string Branch { get; set; }

        string query;
        public void InsertLogin()
        {
            query = "insert into LG_LOGIN (Empid,UserType,Username,Password,Logstatus,Theam,Branch)values(@Empid,@UserType,@Username,@Password,@log,@Theam,@Branch)";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@UserType", Usertype);
            Parameter.Add("@Empid", Empid);
            Parameter.Add("@Username", username);
            Parameter.Add("Password", password);
            Parameter.Add("@log", 0);
            Parameter.Add("@Theam", 1);
            Parameter.Add("@Branch", Branch);
            DbFunctions.InsertUpdate(query, Parameter);
        }

        public DataTable GetUsernamePassword()
        {
            DataTable dt = new DataTable();
            query = " SELECT     LG_LOGIN.* FROM       LG_LOGIN where Username=@Username and Password=@password";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@Username", username);
            Parameter.Add("@password", password);
            dt=DbFunctions.GetDataTable(query, Parameter);
            return dt;
        }

        public Boolean IsAuthorized(string username, string password)
        {
            query = "Select count (*) from Lg_login where Username=@Username and Password=@password";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@Username", username);
            Parameter.Add("@password", password);
            object a = DbFunctions.GetAValue(query, Parameter);
            if (a == null || (int)a == 0)
            {
                return false;
            }
            return true;

        }



        public void UpdateTheam()
        {
            query = "update LG_LOGIN set Theam=@Theam where EmpId=@Empid";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@Theam", Theam);
            Parameter.Add("@Empid", Empid1);
            DbFunctions.InsertUpdate(query, Parameter);
        }

    }
}
