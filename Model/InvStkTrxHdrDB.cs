using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sys_Sols_Inventory.Model;
using System.Data;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory.Model
{
    class InvStkTrxHdrDB
    {
        private string branch, store, docNo, docType, docDateHij, docReference, branchOther, customerCode, notes;
        private string cancelFlag, printFlag, editFlag, posted, addedBy, id;

       
        private DateTime docDateGre;
        private decimal taxAmount, totalAmount;
        private int docId;
        string taxId;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string TaxId
        {
            get { return taxId; }
            set { taxId = value; }
        }
        public decimal TaxAmount
        {
            get { return taxAmount; }
            set { taxAmount = value; }
        }

        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value;}
        }

        public int DocId
        {
            get { return docId; }
            set { docId = value; }
        }

        public DateTime DocDateGre
        {
            get { return docDateGre; }
            set { docDateGre = value; }
        }
        
        public string AddedBy
        {
            get { return addedBy; }
            set { addedBy = value; }
        }

        public string Posted
        {
            get { return posted; }
            set { posted = value; }
        }

        public string EditFlag
        {
            get { return editFlag; }
            set { editFlag = value; }
        }

        public string PrintFlag
        {
            get { return printFlag; }
            set { printFlag = value; }
        }

        public string CancelFlag
        {
            get { return cancelFlag; }
            set { cancelFlag = value; }
        }
        
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string CustomerCode
        {
            get { return customerCode; }
            set { customerCode = value; }
        }

        public string BranchOther
        {
            get { return branchOther; }
            set { branchOther = value; }
        }

        public string DocReference
        {
            get { return docReference; }
            set { docReference = value; }
        }

        public string DocDateHij
        {
            get { return docDateHij; }
            set { docDateHij = value; }
        }

        public string DocType
        {
            get { return docType; }
            set { docType = value; }
        }

        public string DocNo
        {
            get { return docNo; }
            set { docNo = value; }
        }

        public string Store
        {
            get { return store; }
            set { store = value; }
        }

        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }

        public void Insert()
        {
            string query = "INSERT INTO INV_STK_TRX_HDR(DOC_NO,DOC_DATE_GRE,DOC_TYPE,BRANCH) VALUES('" + docNo + "',@date,'"+docType+"','" + branch + "');";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@date", docDateGre);
            DbFunctions.InsertUpdate(query,Parameters);
        }

        public void Insert_Bulk()
        {
            string query = "INSERT INTO INV_STK_TRX_HDR(BRANCH,DOC_NO,DOC_DATE_GRE,DOC_TYPE,AddedBy) VALUES('" + branch + "','" + docNo + "',@date,'"+docType+"','" + addedBy + "')";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@date", docDateGre);
            DbFunctions.InsertUpdate(query, Parameters);
        }
        public SqlDataReader selecrGenPriceType()
        {
            string query = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";
            
           return DbFunctions.GetDataReader(query);
        }
        public DataTable genPriceType()
        {
            string query = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";

            return DbFunctions.GetDataTable(query);
        }
        public SqlDataReader selectTaxRate()
        {
            string query = "SELECT TaxRate from GEN_TAX_MASTER where TaxId='"+ taxId+"'";

            return DbFunctions.GetDataReader(query);
        }
        public DataTable selectTaxRateDt()
        {
            string query = "SELECT TaxRate from GEN_TAX_MASTER where TaxId='" + taxId + "'";

            return DbFunctions.GetDataTable(query);
        }
        public void InsertFromOpeningEntry()
        {
            string query = "INSERT INTO INV_STK_TRX_HDR(DOC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,NOTES,TAX_AMOUNT,TOTAL_AMOUNT,DOC_TYPE,BRANCH) VALUES('" + docNo + "','" + docDateGre.ToString("MM/dd/yyyy") + "','" + docDateHij + "','" + docReference + "','" + notes + "','" + taxAmount + "','" + totalAmount + "','" + docType + "','" + branch + "')";
            
            DbFunctions.InsertUpdate(query);
        }
        public void updateFromOpeningEntry()
        {
            string query = "UPDATE INV_STK_TRX_HDR SET DOC_NO = '" + docNo + "',DOC_DATE_GRE = '" + docDateGre.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + docDateHij + "',DOC_REFERENCE = '" + docReference + "',NOTES = '" + notes + "',TAX_AMOUNT = '" + taxAmount+ "',TOTAL_AMOUNT = '" + totalAmount + "' WHERE DOC_NO = '" + id + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + id + "'";

            DbFunctions.InsertUpdate(query);
        }
        public void deleteFromOpeningEntry()
        {
            string query = "DELETE FROM INV_STK_TRX_HDR WHERE DOC_NO = '" + docNo + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_NO = '" +docNo + "'";

            DbFunctions.InsertUpdate(query);
        }
        public SqlDataReader selectPreData()
        {
            string command = "PREV_OPEN_STOCK_INV";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@ID", id);
            return DbFunctions.GetDataReaderProcedure(command, Parameters);
        }
        public DataTable getAllItemBatches()
        {
            string command = "itemSuggestion_test";
           
            return DbFunctions.GetDataTableProcedure(command);
        }
        public DataTable getItemDetailsToStockTrx()
        {
            string command = "SELECT INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],  INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE,GEN_TAX_MASTER.TaxRate as 'Tax Rate(%)' FROM            INV_ITEM_PRICE INNER JOIN   INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE RIGHT OUTER JOIN  INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE left outer join GEN_TAX_MASTER on INV_ITEM_DIRECTORY.TaxId=GEN_TAX_MASTER.TaxId WHERE        (INV_ITEM_PRICE.SAL_TYPE = 'RTL')";
            return DbFunctions.GetDataTable(command);
        }
        public SqlDataReader getDocNoByDocRef()
        {
            string command = "SELECT DOC_NO FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + docNo + "'";
            return DbFunctions.GetDataReader(command);
        }

    }
}
