using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Employee
{
    public partial class EmployeeSalesLog : Form
    {
        Login log = (Login)Application.OpenForms["Login"];
        Class.EmpLog emplog = new Class.EmpLog();
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
       
        string salesman;
        public EmployeeSalesLog()
        {
            InitializeComponent();
        }

        private void EmployeeSalesLog_Load(object sender, EventArgs e)
        {

            GetSalesMan();
        }


        public void GetSalesMan()
        {
            try
            {
              
               
                //conn.Open();
                //cmd.Connection = conn;
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "GetSalesMan";
                //cmd.Parameters.Clear();
                //cmd.Parameters.AddWithValue("@Empid", log.EmpId);
                //salesman = Convert.ToString(cmd.ExecuteScalar());
                string cmd = "GetSalesMan";
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                parameter.Add("@Empid", log.EmpId);
                salesman = Convert.ToString(Model.DbFunctions.GetAValueProcedure(cmd,parameter));
                if (salesman == "")
                    salesman = "Admin";
                //conn.Close();

            }
            catch
            {
            }

        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            //string conection = Properties.Settings.Default.ConnectionStrings.ToString();
            //Sp_EmpStatementTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(conection);
            Sp_EmpStatementTableAdapter.Connection = Model.DbFunctions.GetConnection();
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("Name", salesman);
            parameters[1] = new ReportParameter("Date", StartDate.Value.ToString() +" to "+EndDate.Value.ToString());
            this.reportViewer1.LocalReport.SetParameters(parameters);
            this.Sp_EmpStatementTableAdapter.Fill(this.EmpStatement.Sp_EmpStatement,StartDate.Value,EndDate.Value,log.EmpId);

            this.reportViewer1.RefreshReport();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                emplog.Id = log.UpdateLog;
                DateTime date = emplog.getlogintime();
                StartDate.Value = date;
                EndDate.Value = DateTime.Now;
                //string conection = Properties.Settings.Default.ConnectionStrings.ToString();
                //Sp_EmpStatementTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(conection);
                Sp_EmpStatementTableAdapter.Connection = Model.DbFunctions.GetConnection();

                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("Name", salesman);
                parameters[1] = new ReportParameter("Date", StartDate.Value.ToString() + " to " + EndDate.Value.ToString());
                this.reportViewer1.LocalReport.SetParameters(parameters);

                this.Sp_EmpStatementTableAdapter.Fill(this.EmpStatement.Sp_EmpStatement, date, DateTime.Now, log.EmpId);

                this.reportViewer1.RefreshReport();
            }
            catch
            { }
        }
    }
}
