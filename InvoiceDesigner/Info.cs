using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Sys_Sols_Inventory
{
    public class Info
    {
        SqlCommand cmd = null;

        SqlDataAdapter sda = new SqlDataAdapter();
        //common
        public static int currentdesignation { get; set; }
        public static int currentuser{ get; set; }
        public static int T_id { get; set; }
        public static int check_trnid { get; set; }
        public static int tr_invoice { get; set; }
        public static int clr_invoice { get; set; }
        public static int scheduleid { get; set; }
        public static int taxid { get; set; }
        public static decimal staxp { get; set; }
        public static string GENITEMCODE { get; set; }
        public static string GENITEBARCODE { get; set; }
        public static DateTime Mindate { get; set; }
        public static DateTime Maxdate { get; set; }
        public static string  companyName { get; set; }
        public static string Address { get; set; }
        public static string Cmobno { get; set; }
        public static string VATNO { get; set; }
        public static string companyArabicName { get; set; }
        public static string GenInvoice { get; set; }
        public static string GenVoucher { get; set; }
        public static string Currentprinter { get; set; }
       
        //schedule
        public int sid { get; set; }
        public int pid { get; set; }
        public int srid { get; set; }
        public int prid { get; set; }
        public int inp { get; set; }
        public int outp { get; set; }

        public static bool server{get;set;}
       public decimal trate { get; set; }

        public string upcean { get; set; }
        public string description { get; set; }
        public string partno { get; set; }


        public string tax_type { get; set; }
       public string rate { get; set; }

       public string pname { get; set; }
       public string pret { get; set; }
       public string sname { get; set; }
       public string sret { get; set; }
       public string in_name { get; set; }
       public string op_name { get; set; }

       public int CGST_s { get; set; }
       public int CGST_p { get; set; }
       public int CGST_sr { get; set; }
       public int CGST_pr { get; set; }
       public int CGST_input { get; set; }
       public int CGST_output { get; set; }

       public int SGST_s { get; set; }
       public int SGST_p { get; set; }
       public int SGST_sr { get; set; }
       public int SGST_pr { get; set; }
       public int SGST_input { get; set; }
       public int SGST_output { get; set; }


       public decimal cgst { get; set; }
       public decimal sgst { get; set; }

       public int d_accid { get; set; }
       public int c_accid { get; set; } 


/// <summary>
/// /
/// </summary>
      
      public int   d_under { get; set; }
      public int c_under { get; set; } 
        public int id { get; set; }
        public int unicode { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public string CODE { get; set; }
        public string invoice_no { get; set; }
      
        public string status2 { get; set; }
  public   Decimal one_color { get; set; }
  public  Decimal two_color { get; set; }
  public Decimal one_black { get; set; }
  public Decimal two_black { get; set; }
      
       public Decimal  d_amt { get; set; }
       public Decimal c_amt { get; set; }
  public decimal blank { get; set; }


       //ledger 
        public int LEDGERID { get; set; }
        public int under { get; set; }
        public string bank_name { get; set; }
        public string bank_branch { get; set; }
        public string bank_accno { get; set; }
        public string bank_ifc { get; set; }
        public decimal open_balance { get; set; }
        public int credit_period { get; set; }
        public decimal credit_limit { get; set; }
        public int client { get; set; }
        public string Container_No { get; set; }
        //supp and cust

        public string code { get; set; }
        public string ad1 { get; set; }
        public string ad2 { get; set; }
        public string tele { get; set; }
        public string mob { get; set; }
        public string fax { get; set; }
       // public string city { get; set; }
        public string city { get; set; }
       // public string country { get; set; }
      //  public string state { get; set; }
        public byte[] img { get; set; }
        public decimal GROSSTOTAL { get; set; }
        public decimal GRANDTOTAL { get; set; }

        public decimal RENT { get; set; }
        public decimal WAITING { get; set; }
        public int schedule { get; set; }
        public decimal TAXAMT { get; set; }
        public string weight { get; set; }
        public decimal TAXPERC { get; set; }
        public decimal rperc { get; set; }
        public decimal wperc { get; set; }
        public decimal mperc { get; set; }
        public string DESTINATION { get; set; }
        //employee
        public int empid { get; set; }
        public string empfname { get; set; }
        public string maritalstatus { get; set; }
        public string emplname { get; set; }
        public string empaddress { get; set; }
        public DateTime dob { get; set; }
        public string sex { get; set; }
        public string telephone { get; set; }
        public string mobile { get; set; }
        public string job_title { get; set; }
        public string Emp_Branche { get; set; }
        public string LedgerId { get; set; }
        public string bloodgp { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string department { get; set; }
        public DateTime join_date { get; set; }
        public decimal wrk_timestart { get; set; }
        public decimal wrk_timeend { get; set; }
        public string adhar { get; set; }
        public string emp_email { get; set; }
        public string salary { get; set; }
        public string acc_no { get; set; }
        public string b_name { get; set; }
        public string b_branch { get; set; }
        public byte[] emp_img { get; set; }
        public int designat{ get; set; }
        public string ArabicName { get; set; }

        //  payroll
        public string first_unti { get; set; }
        public string sec_unit { get; set; }
        public int conversion { get; set; }
        public string alias { get; set; }
        public string at_type { get; set; }
        public string p_under { get; set; }
        public int p_tp_under { get; set; }

        public string p_type { get; set; }
        public int p_tp { get; set; }
        public string designation { get; set; }
        public Boolean affect_salary { get; set; }
        public Boolean use_for_gratuity { get; set; }
        public string calc_type { get; set; }
        public string att_leav_pay { get; set; }
        public string calc_prd { get; set; }
        public string perday_calc { get; set; }

       //LOGIN
        public int Userid;
        private int Empid;
        private string username;
        private string password;
        private string usertype;
        public int Theam { get; set; }
        public int Userid1 { get { return Userid; } set { Userid = value; } }
        public int Empid1 { get { return Empid; } set { Empid = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Usertype { get { return usertype; } set { usertype = value; } }

       //Transactions

        public int transactionid { get; set; }
        public DateTime dated { get; set; }
        public DateTime RELEASDATE { get; set; }
        public DateTime RETURNDATE { get; set; }
        public string accid { get; set; }
        public decimal amt { get; set; }
        public string remarks { get; set; }

        public string voucher_type { get; set; }
        //public int transactionid { get; set; }
      //  public int d_accid { get; set; }
     //   public int c_accid { get; set; }
      //  public int d_under { get; set; }
      //  public int c_under { get; set; }
       // public DateTime dated { get; set; }
      //  public decimal d_amt { get; set; }
      //  public decimal c_amt { get; set; }
      //  public string remarks { get; set; }
        public string refn { get; set; }
        public string refernce { get; set; }
        public string remark_chq { get; set; }
        public decimal p_total { get; set; }
        public decimal p_bala { get; set; }

        SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionStrings.ToString());
        public void con_close()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();

            }
        }
        public void con_open()
        {
            connection = new SqlConnection(Properties.Settings.Default.ConnectionStrings.ToString());
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public string generalexecutescalar(SqlCommand cmd)
        {
            connection = new SqlConnection(Properties.Settings.Default.ConnectionStrings.ToString());
            decimal no;
            object name = null; string nm = "";
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                cmd.Connection = connection;
                
             
               name =cmd.ExecuteScalar();
               if (connection.State == ConnectionState.Open)
               {
                   connection.Close();
               }
               // count++;
                /// inv_id = inv_id + count;
              
               
            }
            catch
            {
               

            }
            try
            {

                nm = name.ToString();
            }
           
            catch
            {
                name = "";
            }
           
           
            return nm;
        }

        public int id_gen(string tabl_name)
        {
            con_open();
           SqlCommand cmd=new SqlCommand("select count(*) as max_id from "+tabl_name,connection);
           SqlDataReader rdr = cmd.ExecuteReader();
           if (rdr.Read())
           {
               id = Convert.ToInt32(rdr["max_id"]);
               id++;
           }
           else
           {
               id = 1;

           }
           cmd.Dispose();
           con_close();
           return id;
        }

        public DataTable  get_genaraldata(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            connection = new SqlConnection(Properties.Settings.Default.ConnectionStrings.ToString());
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                cmd.Connection = connection;
                sda = new SqlDataAdapter(cmd);
              
                sda.Fill(dt);
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch(Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return dt;
        }
        public string get_Scalar(SqlCommand cmd)
        {
            string dt = "";
            connection = new SqlConnection(Properties.Settings.Default.ConnectionStrings.ToString());
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                cmd.Connection = connection;
                dt = cmd.ExecuteScalar().ToString();

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return dt;
        }
        public void  get_genarlExecution(SqlCommand cmd)
        {
            connection = new SqlConnection(Properties.Settings.Default.ConnectionStrings.ToString());
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
               
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

           
        }

        public int newid(string field, string tabl_name)
        {
            con_open();
            SqlCommand cmd = new SqlCommand("select isnull(max("+field+"),0)  as max_id from "+ tabl_name, connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                id = Convert.ToInt32(rdr["max_id"]);
                id++;
            }
            else
            {
                id = 1;

            }
            cmd.Dispose();
            con_close();
            return id;
        }
        public int newid(string field, string tabl_name,int starting)
        {
            con_open();
            SqlCommand cmd = new SqlCommand("select isnull(max(" + field + "),"+starting+")  as max_id from " + tabl_name, connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                id = Convert.ToInt32(rdr["max_id"]);
                id++;
            }
            else
            {
                id = 1;

            }
            cmd.Dispose();
            con_close();
            return id;
        }
        //for item_master

        public int id1 { get; set; }

          //MOBI
        public string Name { get; set; }
      //  public int Id { get; set; }
      //  public string type { get; set; }
        public string category { get; set; }
       // public string Brand { get; set; }
        //public int min_qty { get; set; }
      
        //public int reorder_qty { get; set; }
       // public string unit { get; set; }
      //  public int qty { get; set; }
       
        //public decimal RTL_price { get; set; }
        //public decimal PUR_price { get; set; }
        //public decimal WHL_price { get; set; }
       //Item Sub
     
      
       //public string tax_type { get; set; }
    //  public string rate { get; set; }

        //purchASE
        public int Id { get; set; }
        public string Item_Code { get; set; }

        public string item_bar_code { get; set; }
        public string item_Name { get; set; }
        public string type { get; set; }
      //  public string category { get; set; }
        public string Brand { get; set; }
        public string Group { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int min_qty { get; set; }
        public int max_qty { get; set; }
        public int CUSTOMER { get; set; }
        public string package { get; set; }
        public int SUPPLIER{ get; set; }
        public int SL_NO { get; set; }
        public string TRANSPORT_NO { get; set; }
       // public string TRANSPORT_NO { get; set; }

        public int reorder_qty { get; set; }
        public int warranty { get; set; }
        public string Warranty_type { get; set; }
        public string unit { get; set; }
        public string location { get; set; }
        public decimal qty { get; set; }
      
        public bool serial_no { get; set; }
        public bool man_bar { get; set; }
        //tax
        public decimal tax_vat { get; set; }
        public decimal tax_sess { get; set; }
        public decimal tax_gst { get; set; }
        //price
        public decimal cost_price { get; set; }
        public decimal sales_price { get; set; }
        public decimal RTL_price { get; set; }
        public decimal PUR_price { get; set; }

        public decimal CTNPCS { get; set; }
        public decimal CNTDZN { get; set; }




        public decimal WHL_price { get; set; }
        public decimal MRP_price { get; set; }
        public decimal Promotional { get; set; }
       public bool s_inclusive{get;set;}
       public bool p_inclusive { get; set; }
        public decimal opening_qty { get; set; }
        
        public decimal PRD_disc { get; set; }
        public bool is_seriel { get; set; }
        
        public bool isbom { get; set; }
        public bool ismulti { get; set; }
        public string hsn { get; set; }


        Decimal design_charge { get; set; }
        public DateTime pur_date { get; set; }
        public string vou_no { get; set; }
        public string item_code { get; set; }
        public string pur_price { get; set; }
        public string pur_rtlPrice { get; set; }
        public string pur_Whlprice { get; set; }
        public string disc { get; set; }
        public string pur_qty { get; set; }
        public string pur_total { get; set; }
        public string pur_total_disc { get; set; }
        public string pur_status { get; set; }
        public string pur_nettotal { get; set; }
        public decimal TOTAL { get; set; }
        public decimal NET_TOTAL { get; set; }
        public decimal TOTAL_DISC { get; set; }
        public decimal balance { get; set; }
        //payment
        public string pay_type { get; set; }
        public string bank { get; set; }
        public string cheq_no { get; set; }
        public int paid_by { get; set; }
        public decimal total_amt { get; set; }
        public decimal paid_amt { get; set; }
        public DateTime cheq_date { get; set; }
        public int vch_no { get; set; }
        public decimal discount { get; set; }




        //invoice design
        public int startx { get; set; }
        public int starty { get; set; }
        public int end_x { get; set; }
        public int end_y { get; set; }
        public int top { get; set; }
        public int bottom { get; set; }
        public int left { get; set; }
        public int right { get; set; }
        public string header { get; set; }
        public int width { get; set; }
        public Boolean visible { get; set; }
        public int index { get; set; }
        public string font { get; set; }
        public string font_style { get; set; }
        public int font_size { get; set; }
        public string font_color { get; set; }
        public int height { get; set; }
       // emp_prvg
        public int dep_id1 { get; set; }

       //tax
        public string tax_id { get; set; }
        public string tax_name { get; set; }
        public double tax_rate { get; set; }




        #region service

        public string job_id { get; set; }
        public string job_name { get; set; }
        public  int  statusid { get; set; }

        public DateTime job_termination_date { get; set; }

        public decimal estimate_cost { get; set;}

        public string jobremarks { get; set; }
        public int job_type { get; set; }

        public int servicer_id { get; set; }

        public DateTime service_start_date { get; set; }
        public DateTime service_end_date { get; set; }

        public DateTime job_start_date { get; set; }
        public DateTime job_end_date { get; set; }
        public string service_note { get; set; }

        public int  log { get; set; }
        public int attender_id { get; set; }

        public string call_note { get; set; }



        public string call_loge { get; set; }



        public string ATTACHMENT { get; set; }
        public string entryno { get; set; }


        public DateTime call_date { get; set; }



        #endregion

        #region gridsettings

       
        public string column_name { get; set; }
        public string column_text { get; set; }
        public string  form_name { get; set; }
        public bool visibility { get; set; }
 #endregion

        # region serielno

           public bool serielvisibility { get; set; }
           public string  item_seriel{ get; set; }

           public string si { get; set; }
           public string pv { get; set; }

           public string sri { get; set; }
           public string prv { get; set; }

        //  public int sri { get; set; }
        // public int pvi { get; set; }
        #endregion
        #region multiunit

        public string  baseunit { get; set; }
        public string unit_id { get; set; }
        public decimal unit_qty { get; set; }
        public string unit_name { get; set; }

        public string multi_voucher { get; set; }
        public string multi_barcode { get; set; }
        public string multi_code { get; set; }
        public string multi_unit { get; set; }
        public decimal multi_whl_price { get; set; }
        public decimal multi_p_price { get; set; }
        public decimal multi_r_price { get; set; }
        public decimal multi_mrp { get; set; }

        public bool multi_visibility { get; set; }


        #endregion


    }
}
