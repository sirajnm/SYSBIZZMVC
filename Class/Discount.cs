using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory.Class
{
    class Discount
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();

        public string Disc_Id { get; set; }
        public string Discount_Name { get; set; }
        public string Discount_Arb { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Orders { get; set; }
        public bool Isactive { get; set; }

        public int id { get; set; }
        public string Product_Id { get; set; }
        public string Uom { get; set; }
        public int Discount_Id { get; set; }
        public string Discount_Type { get; set; }
        public string Value { get; set; }
        public string SalType { get; set; }
        public string Date { get; set; }
        public string RateCode { get; set; }

        public void InsertDiscountType()
        {
            string Query = "insert into Tbl_Discount values( @Discount_Name, @Discount_Arb, @Start_Date, @End_Date, @Orders, @Isactive)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Discount_Name", Discount_Name);
            parameters.Add("@Discount_Arb", Discount_Arb);
            parameters.Add("@Start_Date", Start_Date);
            parameters.Add("@End_Date", End_Date);
            parameters.Add("@Orders", Orders);
            parameters.Add("@Isactive", Isactive);
            DbFunctions.InsertUpdate(Query, parameters);
        }
        public void UpdateDiscountType()
        {
            string Query = "update Tbl_Discount set Discount_Name=@Discount_Name, Discount_Arb=@Discount_Arb,Start_Date =@Start_Date,End_Date=@End_Date,Orders=@Orders,Isactive=@Isactive where Disc_Id=@Disc_Id ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Discount_Name", Discount_Name);
            parameters.Add("@Discount_Arb", Discount_Arb);
            parameters.Add("@Start_Date", Start_Date);
            parameters.Add("@End_Date", End_Date);
            parameters.Add("@Orders", Orders);
            parameters.Add("@Isactive", Isactive);
            parameters.Add("@Disc_Id", Disc_Id);
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void insertproductDiscount()
        {
            string Query = "insert into Tbl_ProductDiscount values( @Product_Id, @Uom, @Discount_Id, @Discount_Type, @Value ,@SalType)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Product_Id", Product_Id);
            parameters.Add("@Uom", Uom);
            parameters.Add("@Discount_Id", Discount_Id);
            parameters.Add("@Discount_Type", Discount_Type);
            parameters.Add("@Value", Value);
            parameters.Add("@SalType", SalType);           
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void UpdateProductDiscount()
        {
            string Query = "update Tbl_ProductDiscount set Product_Id=Product_Id,Discount_Id=Discount_Id,Uom=Uom,Discount_Type=Discount_Type,Value=Value where id=id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Product_Id", Product_Id);
            parameters.Add("@Uom", Uom);
            parameters.Add("@Discount_Id", Discount_Id);
            parameters.Add("@Discount_Type", Discount_Type);
            parameters.Add("@Value", Value);          
            parameters.Add("@id", id);
            DbFunctions.InsertUpdate(Query, parameters);
        }
        public void deleteProDiscount()
        {
            string Query = "delete from Tbl_ProductDiscount where Discount_Id='" + Disc_Id + "'";
            DbFunctions.InsertUpdate(Query);
        }
        public DataTable getproductDiscountDetails()
        {
            string Query = "Select * from Tbl_ProductDiscount where Discount_Id=@Discount_Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Discount_Id", Discount_Id);
            return DbFunctions.GetDataTable(Query, parameters);
        }



        public DataTable getproductDiscountDetailsMasterHelp()
        {
            string Query = "Select * from Tbl_Discount order by Disc_Id desc ";
            return DbFunctions.GetDataTable(Query);
        }


        public DataTable GetProductDiscount()
        {
            string Query = "SELECT        Tbl_ProductDiscount.Discount_Type, Tbl_ProductDiscount.Value FROM            Tbl_ProductDiscount INNER JOIN   Tbl_Discount ON Tbl_ProductDiscount.Discount_Id = Tbl_Discount.Disc_Id WHERE        (Tbl_ProductDiscount.Product_Id = @CODE) AND (Tbl_ProductDiscount.Uom = @UOM) AND (Tbl_Discount.Start_Date <= @DATE) AND   (Tbl_Discount.End_Date >= @DATE) AND (Tbl_ProductDiscount.SalType = @SALTYPE) AND (Tbl_Discount.Isactive = 'True') ORDER BY Tbl_Discount.Orders";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@SALTYPE", SalType);
            parameters.Add("@DATE", Date);
            parameters.Add("@CODE", Product_Id);
            parameters.Add("@UOM", Uom);
            return DbFunctions.GetDataTable(Query, parameters);
        }

        public string getmaxid()
        {
            string Query = "Select Max(Disc_Id) from Tbl_Discount  ";
            DataTable dt = DbFunctions.GetDataTable(Query);
            return dt.Rows[0][0].ToString();

        }


        public DataTable DeleteDiscountTypes()
        {
            string Query = "delete * from Tbl_Discount where Disc_Id=@Discount_Id;delete * from Tbl_ProductDiscount where Discount_Id=@Discount_Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Discount_Id", Discount_Id);
            return DbFunctions.GetDataTable(Query, parameters);
        }
        public DataTable SelectPriceType()
        {
            string Query = "SELECT CODE AS [key],CODE+' - '+DESC_ENG AS value FROM GEN_PRICE_TYPE";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable getItem()
        {
            string Query = "SELECT        INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],  INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE FROM            INV_ITEM_PRICE INNER JOIN   INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE RIGHT OUTER JOIN  INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE WHERE        (INV_ITEM_PRICE.SAL_TYPE = '" +RateCode + "')";
            return DbFunctions.GetDataTable(Query);
        }
        public SqlDataReader GetProductDiscountSingleItem()
        {
            string Query = "SELECT     Tbl_ProductDiscount.Product_Id as id, INV_ITEM_DIRECTORY.DESC_ENG AS name, Tbl_ProductDiscount.Uom AS uom, Tbl_ProductDiscount.Discount_Type AS disctype, Tbl_ProductDiscount.Value AS value, Tbl_ProductDiscount.SalType AS saltype FROM         Tbl_ProductDiscount INNER JOIN INV_ITEM_DIRECTORY ON Tbl_ProductDiscount.Product_Id = INV_ITEM_DIRECTORY.CODE WHERE     (Tbl_ProductDiscount.Discount_Id = '" + Disc_Id + "')";
            return DbFunctions.GetDataReader(Query);
        }
    }
}
