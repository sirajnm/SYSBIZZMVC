using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class InvItemDirectoryPictureDB
    {
        string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        byte[] picture;

        public byte[] Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        public void DeleteInsert()
        {
            string query = "delete from  INV_ITEM_DIRECTORY_PICTURE where Code='" + code + "';Insert into  INV_ITEM_DIRECTORY_PICTURE values('" + code + "',@img)";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@img", picture);
            DbFunctions.InsertUpdate(query, Parameters);
        }


        public DataSet PictureByCode()
        {
            string query = "SELECT Picture from INV_ITEM_DIRECTORY_PICTURE where Code = '" + Code + "'";
            DataSet ds = new DataSet();
            ds.Tables.Add(DbFunctions.GetDataTable(query));
            return ds;
        }

    }
}
