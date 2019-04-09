using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Sys_Sols_Inventory.Model;
using System.Windows;

namespace Sys_Sols_Inventory.Class
{
    class Privilage
    {
        private string query;

        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();

        public string  Module { get; set; }
        public int Module_Id { get; set; }
        public int Job_Id { get; set; }
        public string JobRole { get; set; }
        public string jbId { get; set; }

        public void insertmodule()
        {
            query = "insert into Tbl_Module( DESC_ENG) values(@modname)";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@modname", Module);

            try
            {
                DbFunctions.InsertUpdate(query, Parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        public int getpriv_id(string memid)
        {
            query = "select Tbl_EmpPrivilege.Priv_EmpId as id from Tbl_EmpPrivilege inner join LG_LOGIN on Tbl_EmpPrivilege.Priv_EmpId=LG_LOGIN.Empid where LG_LOGIN.Empid='" + memid + "'";
            int flag = 0;
            //SqlDataReader rdr = DbFunctions.GetDataReader(query);
            DataTable rdr = new DataTable();
            rdr=DbFunctions.GetDataTable(query);
            for (int i = 0; i < rdr.Rows.Count;i++ )
            {
                flag = Convert.ToInt32(rdr.Rows[i]["id"]);
                break;
            }
            //DbFunctions.CloseConnection();
            return flag;

        }
        public DataTable getmoduleid()
        {
            DataTable dt = new DataTable();
            try
            {               
                query = "SELECT Mod_Id from Tbl_Module where DESC_ENG=@modname";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@modname", Module);
                dt = DbFunctions.GetDataTable(query, Parameter);
                return dt;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        
        }

        public void deleteJobRight()
        {
            query = "Delete from EMP_PRIVILAGE where JbPriv_JobId=@jobid and JbPriv_ModuleId=@modid";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@modid", Module_Id);
            Parameter.Add("@jobid", Job_Id);
            try
            {
                DbFunctions.InsertUpdate(query, Parameter);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        public void deleteAlljobright()
        {

            query = "Delete from EMP_PRIVILAGE where JbPriv_JobId=@jobid";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@jobid", Job_Id);
            try
            {
                DbFunctions.InsertUpdate(query, Parameter);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable view_jobrole_privilage()
        {

            DataTable dt = new DataTable();
            try
            {
                query = "select Tbl_Module.DESC_ENG FROM Tbl_Module INNER JOIN EMP_PRIVILAGE ON Tbl_Module.Mod_Id = EMP_PRIVILAGE.JbPriv_ModuleId INNER JOIN EMP_JOBROLE ON EMP_PRIVILAGE.JbPriv_JobId = EMP_JOBROLE.JobRoleId where EMP_JOBROLE.JobRoleTitle=@jobroletitle";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@jobroletitle", JobRole);
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
            return dt;
        }


        public void insertjobright()
        {
            query = "insert into EMP_PRIVILAGE(JbPriv_JobId, JbPriv_ModuleId) values(@JobId,@ModId)";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@ModId", Module_Id);
            Parameter.Add("@JobId", Job_Id);

            try
            {
                DbFunctions.InsertUpdate(query, Parameter);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        
        }


        public void deleteprivilage()
        {
            query = "delete from  EMP_PRIVILAGE where  JbPriv_JobId=@jobid";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@jobid", Job_Id);
            try
            {
                DbFunctions.InsertUpdate(query, Parameter);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        public DataTable GetModuleName()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT DESC_ENG FROM Tbl_Module,EMP_PRIVILAGE WHERE JbPriv_ModuleId=Mod_Id AND JbPriv_JobId=@jbId";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@jbId", jbId);
                dt = DbFunctions.GetDataTable(query, Parameter);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;

        }
    }
}
