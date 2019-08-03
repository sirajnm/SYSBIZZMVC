using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public static class General
    {
        public static int backup = 0;
        public static void OnlyFloat(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (sender is KryptonTextBox)
            {
                if (e.KeyChar == '.' && (sender as KryptonTextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
        }

        public static void CellOnlyFloat(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if ((sender as DataGridViewTextBoxEditingControl).Text == "" && e.KeyChar == '.')
            {
                (sender as DataGridViewTextBoxEditingControl).Text = "0.";
                (sender as DataGridViewTextBoxEditingControl).Select(2, 0);
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as DataGridViewTextBoxEditingControl).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        public static void OnlyInt(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        public static void NegFloat(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as KryptonTextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == '-' && (sender as KryptonTextBox).Text.Length > 0)
            {
                e.Handled = true;
            }
        }

        public static bool ItemExists(string code, string id, string table)
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);              
            //SqlCommand cmd = new SqlCommand();            
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT CODE FROM " + table + " WHERE CODE = '" + code + "'";
            //string name = Convert.ToString(cmd.ExecuteScalar());
            //conn.Close();
            string Query = "SELECT CODE FROM " + table + " WHERE CODE = '" + code + "'";
            string name = Convert.ToString(DbFunctions.GetAValue(Query));
            if (name != "" && code != id)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static string generateAccVoucherCode()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandType = CommandType.Text;

            //cmd.CommandText = "SELECT MAX(DOC_NO) FROM ACCOUNT_VOUCHER_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            //string id = Convert.ToString(cmd.ExecuteScalar());
            string value = DateTime.Today.ToString("ddMMyy");
            string testvalue = (Convert.ToInt64(value)).ToString();

            string Query = "SELECT MAX(DOC_NO) FROM ACCOUNT_VOUCHER_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            string id = Convert.ToString(DbFunctions.GetAValue(Query));
            
          
            if (id == "")
            {
                id = testvalue + "0001";
                //  id = DateTime.Today.ToString("ddMMyy") + "0001";
            }
            else
            {
                id = (Convert.ToInt64(id) + 1).ToString();
            }
            return id;

        }
        public static bool DocExists(string code, string id, string table)
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();   
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT DOC_NO FROM " + table + " WHERE DOC_NO = '" + code + "'";
            //string name = Convert.ToString(cmd.ExecuteScalar());
            //conn.Close();
            //if (name != "" && code != id)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}


            string Query = "SELECT DOC_NO FROM " + table + " WHERE DOC_NO = '" + code + "'";
            string name = Convert.ToString(DbFunctions.GetAValue(Query));
            if (name != "" && code != id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string getName(string code, string table)
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();   
            //string name = "";
            //try
            //{
            //    cmd.Connection = conn;
            //    conn.Open();
            //    cmd.CommandType = CommandType.Text;
            //    cmd.CommandText = "SELECT DESC_ENG FROM " + table + " WHERE CODE = '" + code + "'";
            //    name = Convert.ToString(cmd.ExecuteScalar());
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //}
            string name = "";
            try
            {
                string Query = "SELECT DESC_ENG FROM " + table + " WHERE CODE = '" + code + "'";
                name = Convert.ToString(DbFunctions.GetAValue(Query));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return name;
        }

        public static bool IsEnabled(Settings setting)
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();   
            //cmd.Connection = conn;
            string fieldName = "";
            string result = "";
            switch (setting)
            {
                case Settings.Tax:
                    fieldName = "TAX";
                    break;
                case Settings.Batch:
                    fieldName = "BATCH";
                    break;
                case Settings.Barcode:
                    fieldName = "BARCODE";
                    
                    break;
                case Settings.POSTerminal:
                    fieldName = "POSTerminal";
                    break;
                case Settings.Arabic:
                    fieldName = "Arabic";
                    break;
                case Settings.HasType:
                    fieldName = "HasType";
                    break;
                case Settings.HasCategory:
                    fieldName = "HasCategory";
                    break;
                case Settings.HasGroup:
                    fieldName = "HasGroup";
                    break;
                case Settings.HasTM:
                    fieldName = "HasTM";
                    break;
                case Settings.HasAccessLimit:
                    fieldName = "HasAccessLimit";
                    break;

                case Settings.MoveToPrice:
                    fieldName = "MoveToPrice";
                    break;
                case Settings.SelectLastPurchase:
                    fieldName = "SelectLastPurchase";
                    break;
                case Settings.HasRoundOff:
                    fieldName = "HasRoundOff";
                    break;
                case Settings.MoveToDisc:
                    fieldName = "MoveToDisc";
                    break;
                case Settings.ShowPurchase:
                    fieldName = "ShowPurchase";
                    break;
                case Settings.StockOut:
                    fieldName = "StockOut";
                    break;
                case Settings.Exclusive_tax:
                    fieldName = "Exclusive_tax";
                    break;
                case Settings.Pur_Exclusive_tax:
                    fieldName = "Pur_Exclusive_tax";
                    break;
                case Settings.priceBatch:
                    fieldName = "priceBatch";
                    break;
                default:
                    break;
            }
            try
            {
                //conn.Open();
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "SELECT "+fieldName+" FROM SYS_SETUP";
                //result = Convert.ToString(cmd.ExecuteScalar());
                string Query = "SELECT " + fieldName + " FROM SYS_SETUP";
                result = Convert.ToString(DbFunctions.GetAValue(Query));
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               // conn.Close();
            }

            if (result == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetStatelabelText()
        {
            string Query = "select DESC_ENG from GEN_COUNTRY WHERE CODE=(SELECT COUNTRY FROM Tbl_CompanySetup)";
            string state = DbFunctions.GetAValue(Query).ToString();
            return state;
        }
        public static DataTable GetCountryDetails()
        {
            string Query = "select DIVISION_ENG,TAX_UIN from GEN_COUNTRY WHERE CODE=(SELECT COUNTRY FROM Tbl_CompanySetup)";
         
            return DbFunctions.GetDataTable(Query);
        }
        public static string generateStockID()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();   
            //if (conn.State == ConnectionState.Open)
            //{
            //}
            //else
            //{

            //    conn.Open();
            //}
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = conn;
            //string value = DateTime.Today.ToString("ddMMyy");
            //string testvalue = (Convert.ToInt64(value)).ToString();
            //cmd.CommandText = "SELECT MAX(DOC_NO) FROM INV_STK_TRX_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            //string id = Convert.ToString(cmd.ExecuteScalar());
            //conn.Close();
            string value = DateTime.Today.ToString("ddMMyy");
            string testvalue = (Convert.ToInt64(value)).ToString();
            string Query = "SELECT MAX(DOC_NO) FROM INV_STK_TRX_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            string id = Convert.ToString(DbFunctions.GetAValue(Query));
            if (id == "")
            {
                id = testvalue + "0001";
               // id = DateTime.Today.ToString("ddMMyy") + "0001";
            }
            else
            {
                id = (Convert.ToInt64(id) + 1).ToString();
            }
            return id;
        }
        
        public static string generatePurchaseID()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();   
            //conn.Open();
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = conn;
           
          //  cmd.CommandText = "SELECT MAX(DOC_NO) FROM INV_PURCHASE_HDR WHERE DOC_NO LIKE '" +  DateTime.Today.ToString("ddMMyy") + "%'";
            //cmd.CommandText = "SELECT MAX(DOC_NO) FROM INV_PURCHASE_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            
            //string id = Convert.ToString(cmd.ExecuteScalar());
            //conn.Close();
            string value = DateTime.Today.ToString("ddMMyy");
            string testvalue = (Convert.ToInt64(value)).ToString();
            string Query = "SELECT MAX(DOC_NO) FROM INV_PURCHASE_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            string id = Convert.ToString(DbFunctions.GetAValue(Query));
            if (id == "")
            {
                id = testvalue + "0001";
            //    id = DateTime.Today.ToString("ddMMyy") + "0001";
            }
            else
            {
                id = (Convert.ToInt64(id) + 1).ToString();
            }
            return id;
        }

        public static string generateSalesID()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandType = CommandType.Text;          
           
            //cmd.CommandText = "SELECT MAX(DOC_NO) FROM INV_SALES_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            //string id = Convert.ToString(cmd.ExecuteScalar());
            //conn.Close();
            string value = DateTime.Today.ToString("ddMMyy");
            string testvalue = (Convert.ToInt64(value)).ToString();
            string Query = "SELECT MAX(DOC_NO) FROM INV_SALES_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            string id = Convert.ToString(DbFunctions.GetAValue(Query));
            if (id == "")
            {
                id = testvalue + "0001";
            }
            else
            {
                id = (Convert.ToInt64(id) + 1).ToString();
            }
            return id;
        }
        public static string generateSalesQtID()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandType = CommandType.Text;
           
            //cmd.CommandText = "SELECT MAX(DOC_NO) FROM INV_SAL_ORD_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            //string id = Convert.ToString(cmd.ExecuteScalar());
           // conn.Close();
            string value = DateTime.Today.ToString("ddMMyy");
            string testvalue = (Convert.ToInt64(value)).ToString();
            string Query = "SELECT MAX(DOC_NO) FROM INV_SAL_ORD_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            string id = Convert.ToString(DbFunctions.GetAValue(Query));
            if (id == "")
            {
                id = testvalue + "0001";
            }
            else
            {
                id = (Convert.ToInt64(id) + 1).ToString();
            }
            return id;
        }
        public static string generateItemCode()
        {
          //  SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
          //  SqlCommand cmd = new SqlCommand();   
          //  cmd.Connection = conn;
          //  conn.Open();
          //  cmd.CommandType = CommandType.Text;
          //  cmd.CommandText = "SELECT  ISNULL(MAX(convert(decimal(18,0),CODE)),0) as CODE FROM INV_ITEM_DIRECTORY";
          ////  Int64 id =Convert.ToInt64(cmd.ExecuteScalar());
          //  long id = Convert.ToInt64(cmd.ExecuteScalar());
            string Query = "SELECT  ISNULL(MAX(convert(decimal(18,0),CODE)),0) as CODE FROM INV_ITEM_DIRECTORY";
            long id = Convert.ToInt64(DbFunctions.GetAValue(Query));
            string id1 = "";
            if (id ==0)
            {
                id1 = "00001";
            }
            else
            {

                id1 = (Convert.ToUInt64(id) + 1).ToString();
                id += 1;
                if (id1.Length == 1)
                {
                    id1 = "0000" + id;
                }
                else if (id1.Length == 2)
                {
                    id1 = "000" + id;
                }
                else if (id1.Length == 3)
                {
                    id1 = "00" + id;
                }
                                              
                else if (id1.Length == 4)
                {
                    if (Convert.ToInt32(id) <= 9999)
                    {
                        id1 = "0" + id;
                    }
                }
            }
            //conn.Close();
            return id1;
        }

        public static string generatePayVoucherCode()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();   
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandType = CommandType.Text;
           
            //cmd.CommandText = "SELECT MAX(DOC_NO) FROM PAY_PAYMENT_VOUCHER_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            //string id = Convert.ToString(cmd.ExecuteScalar());
            //conn.Close();
            string value = DateTime.Today.ToString("ddMMyy");
            string testvalue = (Convert.ToInt64(value)).ToString();
            string Query = "SELECT MAX(DOC_NO) FROM PAY_PAYMENT_VOUCHER_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            string id = Convert.ToString(DbFunctions.GetAValue(Query));
            if (id == "")
            {
                id = testvalue + "0001";
              //  id = DateTime.Today.ToString("ddMMyy") + "0001";
            }
            else
            {
                id = (Convert.ToInt64(id) + 1).ToString();
            }
            return id;
        
        }

        public static string generateReceiptVoucherCode()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();   
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandType = CommandType.Text;
           
          //  cmd.CommandText = "SELECT MAX(DOC_NO) FROM REC_RECEIPTVOUCHER_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            //string id = Convert.ToString(cmd.ExecuteScalar());
         //   conn.Close();
         /*
            string value = DateTime.Today.ToString("ddMMyy");
            string testvalue = (Convert.ToInt64(value)).ToString();
            string Query = "SELECT MAX(DOC_NO) FROM REC_RECEIPTVOUCHER_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            string id = Convert.ToString(DbFunctions.GetAValue(Query));
            if (id == "")
            {
                id = testvalue + "0001";
                //  id = DateTime.Today.ToString("ddMMyy") + "0001";
            }
            else
            {
                id = (Convert.ToInt64(id) + 1).ToString();
            }
            return id;
            */
            string vouchertype = "ReceiptVoucher";

            string query = "Declare @MaxDocID as int, @NoSeriesSuffix as varchar(5) ";
            query += " Select @MaxDocID = case when Max(Doc_ID) is null then 0 else Max(Doc_ID) end + 1, @NoSeriesSuffix = max(f.NoSeriesSuffix) from REC_RECEIPTVOUCHER_HDR p right join tbl_FinancialYear f on Convert(Varchar, p.DOC_DATE_GRE, 111) between f.SDate and f.EDate ";
            query += " where f.CurrentFY = 1 ";
            query += " Select s.PRIFIX + @NoSeriesSuffix + Right(Replicate('0', s.SERIAL_LENGTH) + cast(@MaxDocID as varchar), s.SERIAL_LENGTH) DOCNo, @MaxDocID DocID from GEN_DOC_SERIAL s ";
            query += " where s.DOC_TYPE = '" + vouchertype + "' ";
            DataTable dt = DbFunctions.GetDataTable(query);
        if (dt.Rows.Count > 0)
            {
              return dt.Rows[0]["DOCNO"].ToString();
                
            }

            return "";

        }

        public static DataTable Product4mBarcode(string barcode)
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();  
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //DataTable table = new DataTable();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "PROD4MBARCODE";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@BARCODE",barcode);
            //adapter.Fill(table);
            string Query = "PROD4MBARCODE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@BARCODE", barcode);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }

        public static bool StateExists(string code)
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT DESC_ENG FROM tblStates WHERE CODE = '" + code + "'";
            //string name = Convert.ToString(cmd.ExecuteScalar());
            //conn.Close();
            string Query = "SELECT DESC_ENG FROM tblStates WHERE CODE = '" + code + "'";
            string name = Convert.ToString(DbFunctions.GetAValue(Query));
            if (name=="")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static string getCurrentDate()
        {
            string Query = "SELECT Date FROM tbl_CurrentDate";

            return Convert.ToString(DbFunctions.GetAValue(Query));
        }
        public static int hasGetCurrentDate()
        {
            string Query = "SELECT COUNT(Date) FROM tbl_CurrentDate";

            return Convert.ToInt32(DbFunctions.GetAValue(Query));
        }
        public static void insertCurrentDate(string date)
        {
            string Query = "INSERT INTO tbl_CurrentDate (Date)VALUES(@d1)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@d1", date);
            DbFunctions.InsertUpdate(Query,parameters);
        }
        public static void updateCurrentDate(string date)
        {
            string Query = "UPDATE tbl_CurrentDate SET Date =@d1";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@d1", date);
            DbFunctions.InsertUpdate(Query, parameters);
        }
    }
}
