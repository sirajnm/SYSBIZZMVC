using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Class
{
    class Daybook
    {
        public string LedgerId { get; set; }
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();

        public DateTime DATED1 { get; set; }
        public DateTime DATED2 { get; set; }
        public string VOUCHERTYPE { get; set; }
        public string UNDER { get; set; }


        string query;

        public DataTable SelectDayBook()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "Acc_DayBook";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@DATED1", DATED1);
                Parameter.Add("@DATED2", DATED2);
                Parameter.Add("@VOUCHERTYPE", VOUCHERTYPE);
                Parameter.Add("@LEDGERID", LedgerId);
                Parameter.Add("@COMMAND", "S6");
                dt = DbFunctions.GetDataTableProcedure(query, Parameter);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dt;

        }

        public DataTable SelectCashBook()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "Acc_DayBook";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@DATED1", DATED1);
                Parameter.Add("@DATED2", DATED2);
                Parameter.Add("@LEDGERID", LedgerId);
                Parameter.Add("@UNDER", UNDER);
                Parameter.Add("@COMMAND", "S5");
                dt = DbFunctions.GetDataTableProcedure(query, Parameter);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
            return dt;


        }


        public DataTable SelectDistinctVoucherType()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "Acc_DayBook";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@COMMAND", "S2");
                dt = DbFunctions.GetDataTableProcedure(query, Parameter);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dt;
        }

        public DataTable GetFinancialYearStart()
        {
            DataTable dt = new DataTable();
            try
            {               
                query = "Acc_FinancialStartDate";
                dt = DbFunctions.GetDataTableProcedure(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dt;

        }
        public string selectOpeningbalance()
        {
            DataTable dt = new DataTable();
            try
            {               
                query = "Acc_DayBook";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@DATED1", DATED1);
                Parameter.Add("@DATED2", DATED2);
                Parameter.Add("@COMMAND", "S3");
                dt = DbFunctions.GetDataTableProcedure(query, Parameter);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dt.Rows[0][0].ToString();
        }

    }
}
