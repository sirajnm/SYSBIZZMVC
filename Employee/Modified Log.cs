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
    public partial class Modified_Log : Form
    {

        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        public Modified_Log()
        {
            InitializeComponent();
        }

        private void Modified_Log_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ModLog.ModifiedLog' table. You can move, or remove it, as needed.
        //    this.ModifiedLogTableAdapter.Fill(this.ModLog.ModifiedLog);


            Getemployee();
        }

        public void Getemployee()
        {
            try
            {
                //conn.Open();
                //cmd.Connection = conn;
                //adapter.SelectCommand = cmd;
                string query = "SELECT     Emp_Fname + ' ' + Emp_Mname + ' ' + Emp_Lname AS Expr1, Empid FROM         EMP_EMPLOYEES";
                DataTable dt = new DataTable();
                //adapter.Fill(dt);
                dt = Model.DbFunctions.GetDataTable(query);
                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
              
                Employee.DisplayMember = "Expr1";
                Employee.ValueMember = "EmpId";
                Employee.DataSource = dt;
                //conn.Close();
            }
            catch
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            //string conection = Properties.Settings.Default.ConnectionStrings.ToString();
            //ModifiedLogTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(conection);
            ModifiedLogTableAdapter.Connection = Model.DbFunctions.GetConnection();

            if (Chk.Checked == false)
            {
                string employee = "";
                if (Employee.Text == "")
                {
                    employee = "%%";
                }
                else
                {
                    employee = Employee.SelectedValue.ToString();
                }

                this.ModifiedLogTableAdapter.FillBy(this.ModLog.ModifiedLog, EditMode.Text, employee);
                this.reportViewer1.RefreshReport();
            }
            else
            {
                string employee="";
                if (Employee.Text == "")
                {
                    employee = "%%";
                }
                else
                {
                    employee = Employee.SelectedValue.ToString() ;
                }

                
                DateTime start = StartDate.Value;
                DateTime end = EndDate.Value;
                this.ModifiedLogTableAdapter.Fill(this.ModLog.ModifiedLog, start, end, EditMode.Text, employee);
                this.reportViewer1.RefreshReport();
            }
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk.Checked == true)
            {
                StartDate.Enabled = true;
                EndDate.Enabled = true;

            }
            else
            {
                StartDate.Enabled = false;
                EndDate.Enabled = false;
            }
        }
    }
}
