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
    class AccountGroup
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();

        public int ACOUNTID { get; set; }
        public string DESC_ENG { get; set; }
        public string DESC_ARB { get; set; }
        public string UNDER { get; set; }
        public string ISBUILTIN { get; set; }
        string query;

        public void insertAccountGroup()
        {
            query = "insert into  tb_AccountGroup(DESC_ENG, DESC_ARB, UNDER, ISBUILTIN) values(@DESC_ENG, @DESC_ARB, @UNDER, @ISBUILTIN)";

            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@DESC_ENG", DESC_ENG);
            Parameter.Add("@DESC_ARB", DESC_ARB);
            Parameter.Add("@UNDER", UNDER);
            Parameter.Add("@ISBUILTIN", ISBUILTIN);

            try
            {
                DbFunctions.InsertUpdate(query, Parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public void UpdateAccountGroup()
        {
            query = "update  tb_AccountGroup set DESC_ENG=@DESC_ENG, DESC_ARB=@DESC_ARB, UNDER=@UNDER, ISBUILTIN=@ISBUILTIN where ACOUNTID=@ACOUNTID";

            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@DESC_ENG", DESC_ENG);
            Parameter.Add("@DESC_ARB", DESC_ARB);
            Parameter.Add("@UNDER", UNDER);
            Parameter.Add("@ISBUILTIN", ISBUILTIN);
            Parameter.Add("@ACOUNTID", ACOUNTID);
            try
            {
                DbFunctions.InsertUpdate(query, Parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public string SelectAccountName(string un)
        {
            DataTable dt = new DataTable();
            string name;
            try
            {
                query = "SELECT DESC_ENG FROM tb_AccountGroup  WHERE ACOUNTID=@UNDER";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@UNDER", un);

                name = Convert.ToString(DbFunctions.GetAValue(query, Parameter));
                return name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }


        }
        public DataTable SelectAccountGroup()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT     tb_AccountGroup.ACOUNTID, tb_AccountGroup.DESC_ENG, tb_AccountGroup.DESC_ARB,tb_AccountGroup.UNDER as u, tb_AccountGroup_1.DESC_ENG AS UNDER , tb_AccountGroup.ISBUILTIN FROM         tb_AccountGroup LEFT OUTER JOIN tb_AccountGroup AS tb_AccountGroup_1 ON tb_AccountGroup.UNDER = tb_AccountGroup_1.ACOUNTID";
                dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public DataTable SelectAccountGroupName()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "Select  ACOUNTID, DESC_ENG from tb_AccountGroup";
                dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;

        }

        public DataTable UnderUseOrNot()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "Select  * from tb_Ledgers where UNDER='" + ACOUNTID + "'";
                dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;

        }
        public void deleteUnder()
        {
            DataTable dt = new DataTable();
            try
            {
                query = "delete from tb_AccountGroup where ACOUNTID='" + ACOUNTID + "'";
                DbFunctions.InsertUpdate(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
