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

    class Employee
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();



     public int empid { get; set; }
     public string empfname { get; set; }
     public string empmname { get; set; }
     public string emplname { get; set; }
     public string empaddress { get; set; }
     public DateTime dob { get; set; }
     public string sex { get; set; }
     public string email { get; set; }
     public string telephone { get; set; }
     public string mobile { get; set; }
     public string designation { get; set; }
     public string Emp_Branche { get; set; }
     public string LedgerId { get; set; }
     public string jbId { get; set; }
     public int setPriv { get; set; }
     public string JbTitle { get; set; }
     public string username { get; set; }
     public string password { get; set; }
     public int jobroleid   {get; set;}
     public string jobroletitle{get; set;}


     public DataTable GetJobId()
     {
         DataTable dt = new DataTable();
         string query = "SELECT JobRoleId from EMP_JOBROLE WHERE JobRoleTitle='" + JbTitle + "'";
         dt = DbFunctions.GetDataTable(query);
         return dt;
     }


        public void InsertEmployee()
        {
            string query = "insert into EMP_EMPLOYEES(Emp_Fname,Emp_Mname,Emp_Lname,Emp_Address,Emp_DOB,Emp_Sex,Emp_Desig,Emp_Email,Emp_Telephone,Emp_Mobile,Emp_Branch,LedgerId)values(@empfname,@empmname,@emplname,@empaddress,@dob,@sex,@designation,@email,@telephone,@mobile,@Emp_Branche,@LedgerId)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@empfname", empfname);
            parameters.Add("@empmname", empmname);
            parameters.Add("@emplname", emplname);
            parameters.Add("@empaddress", empaddress);
            parameters.Add("@dob", dob);
            parameters.Add("@sex", sex);
            parameters.Add("@email", email);
            parameters.Add("@telephone", telephone);
            parameters.Add("@mobile", mobile);
            parameters.Add("@designation", designation);
            parameters.Add("@Emp_Branche", Emp_Branche);
            parameters.Add("@LedgerId", LedgerId);
            DbFunctions.InsertUpdate(query,parameters);
            
        }

        public DataTable GetEmployees()
        {
            string query = "SELECT Empid,Emp_Fname+' '+Emp_Mname+' '+Emp_Lname as Name, Emp_Desig as Designation,LedgerId as LedgerId from EMP_EMPLOYEES where Deleted=0";
            return DbFunctions.GetDataTable(query);
             
        }

        public DataTable GetSalesman()
        {
            string query = "SELECT Empid,Emp_Fname+' '+Emp_Mname+' '+Emp_Lname as Name, Emp_Desig as Designation,LedgerId as LedgerId from EMP_EMPLOYEES where Deleted=0 AND emp_DESIG=21" ;
            return DbFunctions.GetDataTable(query);

        }


        public DataTable GetEmployee(int empid)
        {
                string query = "SELECT EMP_EMPLOYEES.*,EMP_JOBROLE.JobRoleTitle,Username,Password from EMP_EMPLOYEES LEFT OUTER JOIN EMP_JOBROLE ON JbId=JobRoleId LEFT OUTER JOIN LG_LOGIN ON LG_LOGIN.Empid = EMP_EMPLOYEES.Empid WHERE  EMP_EMPLOYEES.Empid=" + empid;
                return DbFunctions.GetDataTable(query);
        }



        public void UpdateEmployee(int empid)
        {

            string query = "update EMP_EMPLOYEES set Emp_Fname=@empfname,Emp_Mname=@empmname,Emp_Lname=@emplname,Emp_Address=@empaddress,Emp_DOB=@dob,Emp_Sex=@sex,Emp_Desig=@designation,Emp_Email=@email,Emp_Telephone=@telephone,Emp_Mobile=@mobile,Emp_Branch=@Emp_Branche where Empid=" + empid;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@empfname", empfname);
            parameters.Add("@empmname", empmname);
            parameters.Add("@emplname", emplname);
            parameters.Add("@empaddress", empaddress);
            parameters.Add("@dob", dob);
            parameters.Add("@sex", sex);
            parameters.Add("@email", email);
            parameters.Add("@telephone", telephone);
            parameters.Add("@mobile", mobile);
            parameters.Add("@designation", designation);
            parameters.Add("@Emp_Branche", Emp_Branche);

            DbFunctions.InsertUpdate(query,parameters);          

        }


        public void DeleteEmployee(int empid)
        {
            try
            {
                string query = "update EMP_EMPLOYEES set Deleted=1 WHERE Empid=" + empid;
                DbFunctions.InsertUpdate(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Deletelogin(int empid)
        {
            try
            {
                string query = "Delete from  LG_LOGIN WHERE  Empid=" + empid;
                DbFunctions.InsertUpdate(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }
        public int getusername(string username)
        {
            int flag = 0;
            try
            {
                string query = "select count(Username) as count from LG_LOGIN WHERE Username='" + username + "'";
                SqlDataReader rdr = DbFunctions.GetDataReader(query);                
                if (rdr.Read())
                {
                    flag = Convert.ToInt32(rdr["count"]);
                    DbFunctions.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return flag;
        }
        public DataTable GetMaxId()
        {          
            string query = "SELECT MAX(Empid) from EMP_EMPLOYEES";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable GetMaxEmpId()
        {
            string query = "SELECT MAX(Empid) FROM EMP_EMPLOYEES";
            return DbFunctions.GetDataTable(query);

        }
        public void UpdateEmployee()
        {
            string query = "UPDATE EMP_EMPLOYEES SET Emp_Fname=@empfname,Emp_Mname=@empmname,Emp_Lname=@emplname,Emp_Address=@empaddress,Emp_DOB=@dob,Emp_Sex=@sex,Emp_Desig=@designation,Emp_Email=@email,Emp_Telephone=@telephone,Emp_Mobile=@mobile,Emp_Branch=@Emp_Branche,JbId=@jbId WHERE Empid=@empId";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@empId", empid);
            parameters.Add("@empfname", empfname);
            parameters.Add("@empmname", empmname);
            parameters.Add("@emplname", emplname);
            parameters.Add("@empaddress", empaddress);
            parameters.Add("@dob", dob);
            parameters.Add("@sex", sex);
            parameters.Add("@email", email);
            parameters.Add("@telephone", telephone);
            parameters.Add("@mobile", mobile);
            parameters.Add("@designation", designation);
            parameters.Add("@Emp_Branche", Emp_Branche);
            parameters.Add("@jbId", jbId);
            DbFunctions.InsertUpdate(query,parameters);

        }
        public void UpdateLogin()
        {
            string query = "UPDATE LG_LOGIN SET Username=@username,Password=@password WHERE Empid=@empId";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@empId", empid);
            parameters.Add("@username", username);
            parameters.Add("@password", password);
            DbFunctions.InsertUpdate(query ,parameters);

        }
        public void UpdateJobId()
        {
            string query = "UPDATE EMP_EMPLOYEES SET JbId=NULL WHERE JbId=@jbId";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@jbId", jbId);
            DbFunctions.InsertUpdate(query,parameter);
        }
        public DataTable getFMName()
        {
            string query = "SELECT     Emp_Fname + ' ' + Emp_Mname + ' ' + Emp_Lname AS Expr1, Empid FROM         EMP_EMPLOYEES";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getJobDetail()
        {
            string query = "SELECT jobroleid,jobroletitle from EMP_JOBROLE";
            return DbFunctions.GetDataTable(query);
        }

     }
}

    

