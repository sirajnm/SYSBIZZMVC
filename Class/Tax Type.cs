using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Class
{
    class Tax_Type
    {
        public int TaxId { get; set; }
        public string code { get; set; }
        public string DESC_ENG { get; set; }
        public string DESC_ARB{get;set;}
        public string TaxRate { get; set; }
        public string CALCULATION_PER {get;set;}
        public string DOC_TYPE { get; set; }

        public void InsertTax()
        {
            String query = "insert into GEN_TAX_MASTER(CODE,DESC_ENG,DESC_ARB,TaxRate)VALUES(@code,@DESC_ENG,@DESC_ARB,@TaxRate)";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@code", code);
            Parameters.Add("@DESC_ENG", DESC_ENG);
            Parameters.Add("@DESC_ARB", DESC_ARB);
            Parameters.Add("@TaxRate", TaxRate);
            DbFunctions.InsertUpdate(query, Parameters);
        }

        public void UpdateTax()
        {
            String query = "update  GEN_TAX_MASTER set CODE=@code,DESC_ENG=@DESC_ENG,DESC_ARB=@DESC_ARB,TaxRate=@TaxRate where TaxId=@taxId";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@code", code);
            Parameters.Add("@DESC_ENG", DESC_ENG);
            Parameters.Add("@DESC_ARB", DESC_ARB);
            Parameters.Add("@TaxRate", TaxRate);
            Parameters.Add("@TaxId", TaxId);
            DbFunctions.InsertUpdate(query, Parameters);
        }

        public void DeleteTax()
        {
            String query = "delete FROM GEN_TAX_MASTER where TaxId=@taxId";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@TaxId", TaxId);
            DbFunctions.InsertUpdate(query, Parameters);
        }


        public DataTable GetTax()
        {
            DataTable dt = new DataTable();
            String query = "SELECT * from GEN_TAX_MASTER";
            dt = DbFunctions.GetDataTable(query);
            return dt;
        }


        public DataTable GettaxNoArabic()
        {
            DataTable dt = new DataTable();
            String query = "SELECT     TaxId, CODE, DESC_ENG, TaxRate, CALCULATION_PER, DOC_TYPE FROM         GEN_TAX_MASTER";
            dt = DbFunctions.GetDataTable(query);
            return dt;
        }
    }

}
