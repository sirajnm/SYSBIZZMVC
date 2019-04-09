using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Sys_Sols_Inventory.Model
{
    class clsState
    {
        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        private string descName;

        public string DescName
        {
            get { return descName; }
            set { descName = value; }
        }
        public DataTable select()
        {
            string Query = "SELECT * FROM tblStates";
            return DbFunctions.GetDataTable(Query);
        }
        public int insert()
        {
            string Query = "insert into tblStates (CODE,DESC_ENG)values(@code,@state)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@code", code);
            parameters.Add("@state",descName);
            return DbFunctions.InsertUpdate(Query,parameters);
        }
        public int update()
        {
            string Query = "Update tblStates Set DESC_ENG=@state where CODE=@Code";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@code", code);
            parameters.Add("@state", descName);
            return DbFunctions.InsertUpdate(Query, parameters);
        }
    }
}
