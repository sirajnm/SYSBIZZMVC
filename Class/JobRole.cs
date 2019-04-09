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
    class JobRole
    {

        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        public int JobRoleid { get; set; }
        public string JobRoleTitle { get; set; }

        string query;

        public void InsertJobRole()
        {
            try
            {
                query = "insert into EMP_JOBROLE(JobRoleTitle)values(@JobRleTitle)";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@JobRleTitle", JobRoleTitle);
                try
                {
                    DbFunctions.InsertUpdate(query,Parameter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public DataTable BindJobRole()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT * from EMP_JOBROLE";
                dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public DataTable bindJobroleName()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT JobRoleTitle from EMP_JOBROLE";
                dt = DbFunctions.GetDataTable(query);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }


        public void UpdateJobRole()
        {
            try
            {
                query = "Update EMP_JOBROLE set JobRoleTitle=@JobRleTitle where JobRoleId=@JobRoleid";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@JobRleTitle", JobRoleTitle);
                Parameter.Add("@JobRoleid", JobRoleid);
                try
                {
                    DbFunctions.InsertUpdate(query, Parameter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteJobRole()
        {
            try
            {
                query = "delete EMP_JOBROLE  where JobRoleId=@JobRoleid";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@JobRoleid", JobRoleid);
                try
                {
                    DbFunctions.InsertUpdate(query, Parameter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public DataTable view_particular_jobrole()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT   JobRoleTitle FROM   EMP_JOBROLE where JobRoleId=@JobRoleid";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@JobRoleid", JobRoleid);
                dt = DbFunctions.GetDataTable(query, Parameter);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public DataTable View_JobRole_JobRoleName()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT JobRoleTitle FROM EMP_JOBROLE WHERE JobRoleId NOT IN(SELECT DISTINCT JbPriv_JobId FROM EMP_PRIVILAGE)";
                dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public DataTable getmaxidjob()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT Max(JobRoleId) FROM EMP_JOBROLE ";
                dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }


        public DataTable View_JobId()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT JobRoleId  FROM EMP_JOBROLE WHERE JobRoleTitle=@JobRoleTitle";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@JobRoleTitle", JobRoleTitle);
                dt = DbFunctions.GetDataTable(query, Parameter);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }


        public DataTable View_distinct_JobId()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT Distinct JbPriv_JobId  FROM EMP_PRIVILAGE";
                dt = DbFunctions.GetDataTable(query);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public DataTable GetMaxJobId()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT MAX(JobRoleId) FROM EMP_JOBROLE";
                dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
    }
}