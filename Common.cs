using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    class Common
    {
        public static void preventDingSound(KeyEventArgs e)
        {
            e.Handled = e.SuppressKeyPress = true;
        }
        public static DataTable getSaleTypes()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            string Query = "SELECT CODE AS [key], DESC_ENG AS value FROM GEN_SALE_TYPE";
            DataTable dt = DbFunctions.GetDataTable(Query);
           
          //  cmd.CommandType = CommandType.Text;
          //  SqlDataAdapter adapter = new SqlDataAdapter(cmd);
           // adapter.Fill(dt);
            DataRow row = dt.NewRow();
            row[1] = "------SELECT------";
            dt.Rows.InsertAt(row, 0);
            return dt;
        }
        public static DataTable getSaleTypes_rpt()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            string Query = "SELECT CODE AS [key], DESC_ENG AS value FROM GEN_SALE_TYPE";
            DataTable dt = DbFunctions.GetDataTable(Query);
           // cmd.CommandText = "SELECT CODE AS [key], DESC_ENG AS value FROM GEN_SALE_TYPE";
           // cmd.CommandType = CommandType.Text;
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
          //  adapter.Fill(dt);
            DataRow row = dt.NewRow();
            row[1] = "";
            dt.Rows.InsertAt(row, 0);
            return dt;
        }
        public static DataTable getVouchTypes()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            string Query = "SELECT CODE AS [key], DESC_ENG AS value FROM GEN_VOUCH_TYPE";
            DataTable dt = DbFunctions.GetDataTable(Query);
            //cmd.CommandText = "SELECT CODE AS [key], DESC_ENG AS value FROM GEN_VOUCH_TYPE";
            //cmd.CommandType = CommandType.Text;
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //adapter.Fill(dt);
            DataRow row = dt.NewRow();
            row[1] = "------ALL------";
            dt.Rows.InsertAt(row, 0);
            return dt;
        }
        public static String getDecimalFormat()
        {
            int decimalCount = 0;
            String decimalFormat = "0.0";
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            string Query = "SELECT Dec_qty FROM SYS_SETUP";
            //cmd.CommandText = "SELECT Dec_qty FROM SYS_SETUP";
            //cmd.Connection = conn;
            //conn.Open();
            String points = Convert.ToString(DbFunctions.GetAValue(Query));
           /// conn.Close();
            if (!points.Equals(""))
            {
                decimalCount = Convert.ToInt32(points);
            }

            for (int i = 1; i < decimalCount; i++)
            {
                decimalFormat += "0";
            }
            return decimalFormat;
        }

        public static int getDecimalFormatCount()
        {
            int decimalCount = 0;
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "SELECT Dec_qty FROM SYS_SETUP";
            //cmd.Connection = conn;
            //conn.Open();
            string Query = "SELECT Dec_qty FROM SYS_SETUP";
            try
            {
                decimalCount = Convert.ToInt32(DbFunctions.GetAValue(Query));
            }
            catch{}
           // conn.Close();
            return decimalCount;
        }

        public static decimal[] getRoundOffValues(decimal value)
        {
            int wholeNettAmount = (int)Math.Floor(value);
            decimal realRoundOffValue = value - wholeNettAmount;
            if (realRoundOffValue > (decimal)0.50)
            {
                wholeNettAmount = (wholeNettAmount + 1);
                realRoundOffValue = wholeNettAmount - value;
            }
            //the first value is roundoff amount the second value is removed or added round off decimal value.
            return new decimal[] { wholeNettAmount, realRoundOffValue };
        }

        public static Hashtable getSettings()
        {
            Hashtable settings = new Hashtable();
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
          //  cmd.CommandText = "SELECT * FROM SYS_SETUP";
           // conn.Open();
            string Query = "SELECT * FROM SYS_SETUP";
            SqlDataReader r = DbFunctions.GetDataReader(Query);
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    settings.Add(r.GetName(i), r[i]);
                }
            }

           // conn.Close();
            DbFunctions.CloseConnection();
            return settings;
        }

        public static DataTable loadStates()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //adapter.SelectCommand = cmd;
            //cmd.CommandText = "SELECT CODE, CODE+ ' - ' +DESC_ENG as NAME FROM tblStates ORDER BY DESC_ENG ASC";
            
            //DataTable table = new DataTable();
            //adapter.Fill(table);
            string Query = "SELECT CODE, CODE+ ' - ' +DESC_ENG as NAME FROM tblStates ORDER BY DESC_ENG ASC";
            return DbFunctions.GetDataTable(Query);
        }

        public static DataTable getCustomer(String customerCode)
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //adapter.SelectCommand = cmd;
           // cmd.CommandText = "SELECT * FROM REC_CUSTOMER WHERE CODE = @code";
            
            //cmd.Parameters.AddWithValue("@code", customerCode);
            //DataTable table = new DataTable();
            //adapter.Fill(table);
            //cmd.Parameters.Clear();
            string Query = "SELECT * FROM REC_CUSTOMER WHERE CODE ="+customerCode;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@code", customerCode);
            return DbFunctions.GetDataTable(Query);
         }

        public static DataTable getSupplyer(String customerCode)
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //adapter.SelectCommand = cmd;
            // cmd.CommandText = "SELECT * FROM REC_CUSTOMER WHERE CODE = @code";

            //cmd.Parameters.AddWithValue("@code", customerCode);
            //DataTable table = new DataTable();
            //adapter.Fill(table);
            //cmd.Parameters.Clear();
            string Query = "SELECT * FROM PAY_SUPPLIER WHERE CODE =" + customerCode;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@code", customerCode);
            return DbFunctions.GetDataTable(Query);
        }

        public static Company getCompany()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT TOP 1 * FROM Tbl_CompanySetup";
            //conn.Open();
            Company company = new Company();
            string Query = "SELECT TOP 1 * FROM Tbl_CompanySetup";
            SqlDataReader r = DbFunctions.GetDataReader(Query);
            while (r.Read())
	        {
	            company.ID = Convert.ToString(r["Id"]);
                company.Name = Convert.ToString(r["Company_Name"]);
                company.Address = Convert.ToString(r["Address"]);
                company.Phone = Convert.ToString(r["Phone"]);
                company.Email = Convert.ToString(r["Email"]);
                company.Fax = Convert.ToString(r["Fax"]);
                company.Logo = Convert.ToString(r["Logo"]);
                company.Other_details = Convert.ToString(r["Other_details"]);
                company.TIN_No = Convert.ToString(r["TIN_No"]);
                company.PAN_No = Convert.ToString(r["PAN_No"]);
                company.CST_No = Convert.ToString(r["CST_No"]);
                company.WebSite = Convert.ToString(r["WebSite"]);
                company.ARBCompany_Name = Convert.ToString(r["ARBCompany_Name"]);
                company.ARBAddress = Convert.ToString(r["ARBAddress"]);
                company.ARBPhone = Convert.ToString(r["ARBPhone"]);
                company.ARBEmail = Convert.ToString(r["ARBEmail"]);
                company.ARBFax = Convert.ToString(r["ARBFax"]);
                company.ARBWebSite = Convert.ToString(r["ARBWebSite"]);
                try
                {
                    company.Acc_no = Convert.ToString(r["ACC_NO"]);
                    company.ifsc = Convert.ToString(r["IFSC"]);
                }
                catch { }
	        }
            //conn.Close();
            DbFunctions.CloseConnection();
            return company;
        }

        public static String sqlEscape(String value)
        {
            if (!value.Equals(""))
            {
                return value.Replace("'", "''");
            }
            return value;
        }

        public static string getcurrency()
        {
            string query = "select DEFAULT_CURRENCY_CODE from gen_branch";
            string cur = DbFunctions.GetAValue(query).ToString();           
            return cur;
        }
        public static string CompanyNamebarcode()
        {
            string query = "select companyname from tbl_barcodesettings";
            string cur = DbFunctions.GetAValue(query).ToString();
            return cur;
        }
    }
}

class Company
{
    public String ID { get; set; }
    public String Name { get; set; }
    public String Address { get; set; }
    public String Phone { get; set; }
    public String Email { get; set; }
    public String Fax { get; set; }
    public String Logo { get; set; }
    public String Other_details { get; set; }
    public String TIN_No { get; set; }
    public String PAN_No { get; set; }
    public String CST_No { get; set; }
    public String WebSite { get; set; }
    public String ARBCompany_Name { get; set; }
    public String ARBAddress { get; set; }
    public String ARBPhone { get; set; }
    public String ARBEmail { get; set; }
    public String ARBFax { get; set; }
    public String ARBWebSite { get; set; }
    public String Acc_no { get; set; }
    public String ifsc { get; set; }
}
