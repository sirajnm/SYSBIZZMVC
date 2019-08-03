using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory.Class
{
    class GenDocSerial
    {

public string   BRANCH_CODE { get; set; }
public string STORE_CODE { get; set; }
public string DOC_TYPE { get; set; }
public int  SERIAL_NO { get; set; }
public string PRIFIX { get; set; }
public string SUFIX { get; set; }
public string AUTO_NUM { get; set; }
public int NEED_ZERO_BEFORE { get; set; }
public int SERIAL_LENGTH { get; set; }
public string MODULES_CODE { get; set; }
public int id { get; set; }

        public GenDocSerial()
        {

        }
        public GenDocSerial(int docid)
        {
            string query = "Select * from GEN_DOC_SERIAL where id = "+ docid;
            DataTable dt = DbFunctions.GetDataTable(query);
            foreach(DataRow dr in dt.Rows)
            {
                id = dr["id"] == null ? 0 : Convert.ToInt16(dr["id"]);
                if (id == 0) break;
                BRANCH_CODE = dr["BRANCH_CODE"].ToString();
                DOC_TYPE = dr["DOC_TYPE"].ToString();
                PRIFIX = dr["PRIFIX"].ToString();
                SUFIX = dr["SUFIX"].ToString();
                SERIAL_NO = dr["SERIAL_NO"] == null ? 0 : Convert.ToInt16(dr["SERIAL_NO"]);
                SERIAL_LENGTH = dr["SERIAL_LENGTH"] == null ? 0 : Convert.ToInt16(dr["SERIAL_LENGTH"]);


            }

        }
        public bool Insert()
        {
            string query = "Insert Into Gen_Doc_Serial ( BRANCH_CODE, STORE_CODE, DOC_TYPE, SERIAL_NO, PRIFIX, SUFIX,  NEED_ZERO_BEFORE, SERIAL_LENGTH)";
            query += " values( @BRANCH_CODE, @STORE_CODE, @DOC_TYPE, @SERIAL_NO, @PRIFIX, @SUFIX,  @NEED_ZERO_BEFORE, @SERIAL_LENGTH) ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            
            parameters.Add("@BRANCH_CODE", BRANCH_CODE);
            parameters.Add("@STORE_CODE", STORE_CODE);
            parameters.Add("@DOC_TYPE", DOC_TYPE);
            parameters.Add("@SERIAL_NO", SERIAL_NO);
            parameters.Add("@PRIFIX", PRIFIX);
            parameters.Add("@SUFIX", SUFIX);
            parameters.Add("@AUTO_NUM", AUTO_NUM);
            parameters.Add("@NEED_ZERO_BEFORE", NEED_ZERO_BEFORE);
            parameters.Add("@SERIAL_LENGTH", SERIAL_LENGTH);
         //   parameters.Add("@MODULES_CODE", MODULES_CODE);
            if (DbFunctions.InsertUpdate(query, parameters) >= 1)
                return true;
            return false;
        }

        public bool Update()
        {
            string query = "Update Gen_Doc_Serial set BRANCH_CODE=@BRANCH_CODE, STORE_CODE = @STORE_CODE, DOC_TYPE =@DOC_TYPE, SERIAL_NO=@SERIAL_NO, PRIFIX=@PRIFIX, SUFIX=@SUFIX, AUTO_NUM=@AUTO_NUM, NEED_ZERO_BEFORE=@NEED_ZERO_BEFORE, SERIAL_LENGTH=@SERIAL_LENGTH,MODULES_CODE=@MODULES_CODE where id= @id ";
        
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("@id", id);
            parameters.Add("@BRANCH_CODE", BRANCH_CODE);
            parameters.Add("@STORE_CODE", STORE_CODE);
            parameters.Add("@DOC_TYPE", DOC_TYPE);
            parameters.Add("@SERIAL_NO", SERIAL_NO);
            parameters.Add("@PRIFIX", PRIFIX);
            parameters.Add("@SUFIX", SUFIX);
            parameters.Add("@AUTO_NUM", AUTO_NUM);
            parameters.Add("@NEED_ZERO_BEFORE", NEED_ZERO_BEFORE);
            parameters.Add("@SERIAL_LENGTH", SERIAL_LENGTH);
            parameters.Add("@MODULES_CODE", MODULES_CODE);
            if (DbFunctions.InsertUpdate(query, parameters) >= 1)
                return true;
            return false;
        }

    }

    
}
