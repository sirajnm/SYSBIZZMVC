using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Sys_Sols_Inventory.Model
{
    class clsCustType
    {
        private string code;
        private string id;
        private string desc_eng;
        private string desc_arb;
        private decimal creditLevel;
        private string priceType;
        private string discountType;

        public string DiscountType
        {
            get { return discountType; }
            set { discountType = value; }
        }
        public string PriceType
        {
            get { return priceType; }
            set { priceType = value; }
        }

        public decimal CreditLevel
        {
            get { return creditLevel; }
            set { creditLevel = value; }
        }
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Desc_arb
        {
            get { return desc_arb; }
            set { desc_arb = value; }
        }
        public string Desc_eng
        {
            get { return desc_eng; }
            set { desc_eng = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public DataTable GetCustomer()
        {
            string Query="SELECT CODE,DESC_ENG,DESC_ARB,CREDIT_LEVEL,PRICE_TYPE,DISCOUNT_TYPE FROM REC_CUSTOMER_TYPE";
            return DbFunctions.GetDataTable(Query);


        }
        public DataTable GetPriceType()
        {
            string Query = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM GEN_PRICE_TYPE";
            return DbFunctions.GetDataTable(Query);


        }
        public DataTable GetDiscoundType()
        {
            string Query = "SELECT CODE,DESC_ENG AS DESC_ENG FROM GEN_DISCOUNT_TYPE";
            return DbFunctions.GetDataTable(Query);


        }
        public void add()
        {
            string Query = "INSERT INTO REC_CUSTOMER_TYPE(CODE,DESC_ENG,DESC_ARB,CREDIT_LEVEL,PRICE_TYPE,DISCOUNT_TYPE) VALUES('" + code + "','" + desc_eng+ "','" + desc_arb + "','" + creditLevel + "','" + priceType + "','" + discountType + "')";
            DbFunctions.InsertUpdate(Query);
        }
        public void Update()
        {
            string Query = "UPDATE REC_CUSTOMER_TYPE SET CODE = '" + code + "', DESC_ENG = '" + desc_eng+ "',DESC_ARB = '" + desc_arb + "',CREDIT_LEVEL = '" + creditLevel + "',PRICE_TYPE = '" + priceType + "',DISCOUNT_TYPE = '" + discountType + "' where CODE='" + id + "'";
            DbFunctions.InsertUpdate(Query);
        }
    }
}
