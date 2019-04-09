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
    class EmployeePrivilage
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        public int JbPriv_JobId { get; set; }
        public int JbPriv_Id { get; set; }
        public int JbPriv_ModuleId { get; set; }
        public int EmpId { get; set; }
        public int mod_Id { get; set; }
        public string modName { get; set; }
        public string emp_fname { get; set; }
        public string modId { get; set; }

        string query;

        public DataTable View_Mod_ModuleId()
        {
            DataTable dt = new DataTable();
            query = "SELECT Mod_Id FROM vw_Mod_ModName WHERE Mod_Name = @modname";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@modName", modName);
            dt = DbFunctions.GetDataTable(query, Parameter);
            return dt;
        }

        public void DeleteEmpPrivilege()
        {
            query = "Delete from Tbl_EmpPrivilege where Priv_EmpId=@EmpId and  Priv_ModId=@ModId";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@EmpId", EmpId);
            Parameter.Add("@ModId", mod_Id);
            DbFunctions.InsertUpdate(query, Parameter);
        }

        public DataTable SelectEmpDetails()
        {
            DataTable dt = new DataTable();
            query = " SELECT  EMP_EMPLOYEES.Empid,   EMP_EMPLOYEES.Emp_Fname,  EMP_EMPLOYEES.Emp_Lname,LG_LOGIN.Empid,  LG_LOGIN.Username,   LG_LOGIN.Logstatus  FROM         EMP_EMPLOYEES inner JOIN LG_LOGIN ON EMP_EMPLOYEES.Empid = LG_LOGIN.Empid and EMP_EMPLOYEES.Deleted=0 ";
            dt = DbFunctions.GetDataTable(query);
            return dt;
        }


        public DataTable getmodulesOfemployee()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            query = " SELECT   Tbl_Module.DESC_ENG FROM  Tbl_EmpPrivilege INNER JOIN EMP_EMPLOYEES ON Tbl_EmpPrivilege.Priv_EmpId = EMP_EMPLOYEES.Empid INNER JOIN Tbl_Module ON Tbl_EmpPrivilege.Priv_ModId = Tbl_Module.Mod_Id WHERE     (EMP_EMPLOYEES.Empid = @EmpId)";
            Parameter.Add("@EmpId", EmpId);
            dt = DbFunctions.GetDataTable(query, Parameter);
            return dt;
        }


        public DataTable get_distinct_empid()
        {
            DataTable dt = new DataTable();
            query = " SELECT distinct Priv_EmpId FROM Tbl_EmpPrivilege";
            dt = DbFunctions.GetDataTable(query);
            return dt;
        }

        public void Updatelogintableinfo()
        {
            query = "Update LG_LOGIN set  Logstatus=1 where Empid=@EmpId";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@EmpId", EmpId);
            DbFunctions.InsertUpdate(query, Parameter);
        }

        public DataTable View_Modules_In_employeeprivilage()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            query = "SELECT     Tbl_Module.DESC_ENG FROM Tbl_Module INNER JOIN Tbl_EmpPrivilege ON Tbl_Module.Mod_Id = Tbl_EmpPrivilege.Priv_ModId and Tbl_EmpPrivilege.Priv_EmpId=@EmpId";
            Parameter.Add("@EmpId", EmpId);
            dt = DbFunctions.GetDataTable(query, Parameter);
            return dt;
        }

        public void UpdatelogintabletoZero()
        {
            query = "Update LG_LOGIN set  Logstatus=0 where Empid=@EmpId";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@EmpId", EmpId);
            DbFunctions.InsertUpdate(query, Parameter);
        }

        public void InsertPrivilage()
        {
            query = "Insert into Tbl_EmpPrivilege values(@EmpId,@ModId)";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@EmpId", EmpId);
            Parameter.Add("@ModId", mod_Id);
            DbFunctions.InsertUpdate(query, Parameter);
        }
        public void DeleteEmpPriv()
        {
            try
            {
                query = "DELETE FROM Tbl_EmpPrivilege WHERE Priv_EmpId=@empId";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@empId", EmpId);
                DbFunctions.InsertUpdate(query,Parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public DataTable View_Distinct_EmpId()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT DISTINCT Priv_EmpId  FROM Tbl_EmpPrivilege";
                dt = DbFunctions.GetDataTable(query);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
        public DataTable GetModuleName()
        {
            DataTable dt = new DataTable();
            try
            {                
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                query = "SELECT DESC_ENG FROM Tbl_Module,Tbl_EmpPrivilege WHERE Priv_ModId=Mod_Id AND Priv_EmpId=@empId";
                Parameter.Add("@empId", EmpId);
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