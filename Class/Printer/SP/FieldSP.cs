using Sys_Sols_Inventory.Class.Printer.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;

namespace Sys_Sols_Inventory.Class.Printer.SP
{
    public class FieldSP
    {


        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand sqlcmd = new SqlCommand(); 

        #region Functions
        /// <summary>
        /// Function for add the fields
        /// </summary>
        /// <param name="infoField"></param>
        public void FieldsAdd(FieldInfo infoField)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand sqlcmd = new SqlCommand("FieldsAdd", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@formId", SqlDbType.VarChar).Value = infoField.FormId;
                sqlcmd.Parameters.Add("@fieldName", SqlDbType.VarChar).Value = infoField.FieldName;
                sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
          //      MessageBox.Show(ex.Message, "Purchers add", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

        }
        /// <summary>
        /// Function for edit the fields
        /// </summary>
        /// <param name="infoField"></param>
        public void FieldsEdit(FieldInfo infoField)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand sqlcmd = new SqlCommand("FieldsEdit", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@fieldId", SqlDbType.VarChar).Value = infoField.FieldId;
                sqlcmd.Parameters.Add("@formId", SqlDbType.VarChar).Value = infoField.FormId;
                sqlcmd.Parameters.Add("@fieldName", SqlDbType.VarChar).Value = infoField.FieldName;
                sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
           //     MessageBox.Show(ex.Message, "Purchers add", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// Function for view the fields
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public FieldInfo FieldsView(int fieldId)
        {
            FieldInfo infoField = new FieldInfo();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand sqlcmd = new SqlCommand("FieldsView", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@fieldId", SqlDbType.Int).Value = fieldId;
                SqlDataReader sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    infoField.FieldId = int.Parse(sqldr["FieldId"].ToString());
                    infoField.FormId = int.Parse(sqldr["FormId"].ToString());
                    infoField.FieldName = sqldr["FieldName"].ToString();
                }
            }
            catch (Exception ex)
            {
           //     MessageBox.Show(ex.Message, "FieldsView", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return infoField;
        }
        /// <summary>
        /// Function for view all the fields
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public DataTable FieldsViewAll(int FormId)
        {
            DataTable dtblPurchers = new DataTable();
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter sqldaPurchers = new SqlDataAdapter("FieldsViewAll", conn);
                sqldaPurchers.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldaPurchers.SelectCommand.Parameters.Add("@formId", SqlDbType.Int).Value = FormId;
                sqldaPurchers.Fill(dtblPurchers);
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message, "FieldsViewAll", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return dtblPurchers;
        }
        /// <summary>
        /// Function for delete the fields
        /// </summary>
        /// <param name="formId"></param>
        public void FieldsDelete(int formId)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand sccmd = new SqlCommand("FieldsDelete", conn);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@formId", SqlDbType.Int);
                sprmparam.Value = formId;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
        }
        #endregion
    }
}
