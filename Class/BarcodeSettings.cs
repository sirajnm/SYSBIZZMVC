using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory.Class
{
  public  class BarcodeSettings
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand(); 
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private string itemCode;

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        public int IsMRP { get; set; }
        public int IsProductName { get; set; }
        public int IsProductCode { get; set; }
        public int IsCompanyName { get; set; }
        public string CompanyName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int IsBarcodeValue { get; set; }
        public string PriceType { get; set; }
        public bool isBoarder { get; set; }
        public float font { get; set; }
        public float cellheight { get; set; }
        public float topMargine { get; set; }
        public int Length { get; set; }

        string query;

        public void InsertBarcodeSettings()
        {
            try
            {
                string Query = "insert into tbl_BarcodeSettings (IsMRP,IsProductCode,IsCompanyName,CompanyName,Width,Height,IsBarcodeValue,PriceType)values(@IsMRP,@IsProductCode,@IsCompanyName,@CompanyName,@Width,@Height,@IsBarcodeValue,@PriceType)";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@IsMRP", IsMRP);
                parameters.Add("@IsProductCode", IsProductCode);
                parameters.Add("@IsCompanyName", IsCompanyName);
                parameters.Add("@CompanyName", CompanyName);
                parameters.Add("@Width", Width);
                parameters.Add("@Height", Height);
                parameters.Add("@IsBarcodeValue", IsBarcodeValue);
                parameters.Add("@PriceType", PriceType);
               
                try
                {                    
                    DbFunctions.InsertUpdate(Query, parameters);
                    MessageBox.Show("Barcode Settings Added Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString()+"Error Occured Please Try Again");
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       public void SaveManual(bool value)
        {
            try
            {
                
                string Query = "UPDATE tbl_BarcodeSettings SET ISMANUAL=@value";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@value", value);
                DbFunctions.InsertUpdate(Query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       public bool getManualSettings()
       {
           try
           {               
               string Query = "SELECT ISMANUAL FROM tbl_BarcodeSettings";
               return Convert.ToBoolean(DbFunctions.GetAValue(Query));
           }
           catch
           {
           
               return true;              
           }
       }

        public void UPdateBarcodeSettings()
        {
            try
            {

                string Query = "Update tbl_BarcodeSettings set IsMRP=@IsMRP,IsProductCode=@IsProductCode,IsCompanyName=@IsCompanyName,CompanyName=@CompanyName,Width=@Width,Height=@Height,IsBarcodeValue=@IsBarcodeValue,PriceType=@PriceType,FontSize=@font,CellHeight=@cellheight,IsBoarder=@boarder,TopMrgine=@margine,ItemLength=@length   where Id=3";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@IsMRP", IsMRP);
                parameters.Add("@IsProductCode", IsProductCode);
                parameters.Add("@IsCompanyName", IsCompanyName);
                parameters.Add("@CompanyName", CompanyName);
                parameters.Add("@Width", Width);
                parameters.Add("@Height", Height);
                parameters.Add("@IsBarcodeValue", IsBarcodeValue);
                parameters.Add("@PriceType", PriceType);
                parameters.Add("@font", font);
                parameters.Add("@margine", topMargine);
                parameters.Add("@cellheight", cellheight);
                parameters.Add("@boarder", isBoarder);
                parameters.Add("@length", Length);
                try
                {
                    
                    DbFunctions.InsertUpdate(Query, parameters);
                    MessageBox.Show("Barcode Settings Updated Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "Error Occured Please Try Again");
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public DataTable GetSettings()
        {
            string Query = "";
            try
            {
               Query = "Select * from tbl_BarcodeSettings";                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
            return DbFunctions.GetDataTable(Query);           
        }

      public DataTable GetPricetype()
        {
            string Query = "";
            try
            {
                Query = "SELECT CODE AS [key],CODE+' - '+DESC_ENG AS value FROM GEN_PRICE_TYPE";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
            return DbFunctions.GetDataTable(Query);
           
        }

      public SqlDataReader selectBarcode()
      {
          string Query = "SELECT UNIT_CODE,PACK_SIZE,BARCODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + itemCode + "'";
          return DbFunctions.GetDataReader(Query);
      }
      public SqlDataReader selectPriceList()
      {
          string Query = "SELECT UNIT_CODE,SAL_TYPE,PRICE FROM INV_ITEM_PRICE WHERE ITEM_CODE = '" + itemCode + "'";
          return DbFunctions.GetDataReader(Query);
      }
      public DataTable selectUnits()
      {
          string Query = "SELECT CODE FROM INV_UNIT";
          return DbFunctions.GetDataTable(Query);
      }
      public SqlDataReader selectPriceType()
      {
          string Query = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";               
          return DbFunctions.GetDataReader(Query);
      }
      public DataTable selectItemDetails()
      {
          string Query = "SELECT INV_ITEM_DIRECTORY.CODE AS [ItemCode],INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],INV_ITEM_DIRECTORY.DESC_ARB as [Arab Name],  RateChange.R_Id AS rid,INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_DIRECTORY.CODE AS [BarCode],RateChange.Sale_Price AS [Rate],RateChange.Price   FROM  INV_ITEM_DIRECTORY LEFT OUTER JOIN  RateChange ON INV_ITEM_DIRECTORY.CODE = RateChange.Item_code left outer join INV_ITEM_DIRECTORY_UNITS   ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE";
          return DbFunctions.GetDataTable(Query);
      }
      public string selectItemBarcode()
      {
          string Query = "select barcode from INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE=" + itemCode ;
          return DbFunctions.GetAValue(Query).ToString();
      }
      public string selectCurrencyCode()
      {
          string Query = "select DEFAULT_CURRENCY_CODE from gen_branch";
          return DbFunctions.GetAValue(Query).ToString();
      }
      public DataTable selectAllBatches()
      {
          string Query = "itemSuggestion_test";
          return DbFunctions.GetDataTableProcedure(Query);
      }
    }
}
