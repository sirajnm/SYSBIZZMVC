using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sys_Sols_Inventory.Model;
using System.Data;

namespace Sys_Sols_Inventory.Model
{
    class ProjectDB
    {
        int id, customer;

        public int Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public String State
        {
            get { return state; }
            set { state = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        string descEng, status, location, siteAddress,state;

        public string SiteAddress
        {
            get { return siteAddress; }
            set { siteAddress = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string DescEng
        {
            get { return descEng; }
            set { descEng = value; }
        }
        decimal estimateAmt;

        public decimal EstimateAmt
        {
            get { return estimateAmt; }
            set { estimateAmt = value; }
        }

        bool active;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        DateTime entryDate, lastUpdated;

        public DateTime LastUpdated
        {
            get { return lastUpdated; }
            set { lastUpdated = value; }
        }

        public DateTime EntryDate
        {
            get { return entryDate; }
            set { entryDate = value; }
        }

        public int Insert_Project()
        {
            string query = "INSERT INTO Tbl_Project(Id,DESC_ENG,Customer ,Location, State,[Estimated Budget],Active,Status,SiteAddress,EntryDate,LastUpdated) VALUES('" + id + "','" + descEng + "','" + customer + "','" + location + "','" + state + "','" + estimateAmt + "','" + active + "','" + status + "','" + siteAddress + "','" + entryDate + "','" + lastUpdated + "' )";
            return DbFunctions.InsertUpdate(query);
        }

        public int Update_Project()
        {
            string query = "update Tbl_Project set DESC_ENG=@Name,Customer=@cid,Location=@location,State=@sid,[Estimated Budget]=@estbud,Active=@active,Status=@sts,SiteAddress=@siteadd,LastUpdated=@lastup WHERE Id='" + id + "'";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Name", descEng);
            Parameters.Add("@cid", customer);
            Parameters.Add("@location", location);
            Parameters.Add("@sid", state);
            Parameters.Add("@estbud", estimateAmt);
            Parameters.Add("@active", active);
            Parameters.Add("@sts", status);
            Parameters.Add("@siteadd", siteAddress);
            Parameters.Add("@lastup", lastUpdated);
            return DbFunctions.InsertUpdate(query, Parameters);
        }

        public int maxId()
        {
            string query = "Select MAX(Id) From Tbl_Project";
            try
            {
                return (Convert.ToInt32(DbFunctions.GetAValue(query))+1);
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public bool CheckProjectExist()
        {
            string query = "Select * From Tbl_Project where Id='"+id+"'";
            DataTable dt=DbFunctions.GetDataTable(query);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }


        public DataTable AllData()
        {
            string query = "SELECT Tbl_Project.Id,Tbl_Project.DESC_ENG AS [PROJECT NAME],Tbl_Project.[Customer ] AS [CUST CODE], REC_CUSTOMER.DESC_ENG AS [CUSTOMER NAME],TBL_PROJECT.SiteAddress,TBL_PROJECT.State,TBL_PROJECT.Location, tblStates.DESC_ENG AS [STATE NAME],Tbl_Project.Status,Tbl_Project.ACTIVE,Tbl_Project.[Estimated Budget] FROM TBL_PROJECT INNER JOIN REC_CUSTOMER ON Tbl_Project.[Customer ]=REC_CUSTOMER.CODE INNER JOIN TBLSTATES ON TBL_PROJECT.STATE=tblStates.CODE;";
            DataTable dt = DbFunctions.GetDataTable(query);
            return dt;
        }

        public DataTable ProjectForCombo()
        {
            string query = "Select Id,DESC_ENG From Tbl_Project";
            DataTable dt = DbFunctions.GetDataTable(query);
            DataRow row = dt.NewRow();
            row[1] = "------SELECT------";
            row[0] = "0";
            //dt.Rows[0].Add(0, "------SELECT------");
            dt.Rows.InsertAt(row, 0);
            return dt;
        }

        public bool DeleteProject()
        {
            string query = "Delete From Tbl_Project where Id='" + id + "'";
            int row = DbFunctions.InsertUpdate(query);
            if (row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UsedProjects()
        {
            string query = "Select * From tb_Transactions where PROJECTID='" + id + "'";
            DataTable dt = DbFunctions.GetDataTable(query);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public DataTable ProjectForComboCustomer()
        {
            string query = "Select Id,DESC_ENG From Tbl_Project where Customer='"+customer+"' ";
            DataTable dt = DbFunctions.GetDataTable(query);
            DataRow row = dt.NewRow();
            row[1] = "------SELECT------";
            row[0] = "0";
            //dt.Rows[0].Add(0, "------SELECT------");
            dt.Rows.InsertAt(row, 0);
            return dt;
        }
    }
}
