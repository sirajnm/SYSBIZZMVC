﻿using Sys_Sols_Inventory.Class.Printer.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;

namespace Sys_Sols_Inventory.Class.Printer.SP
{
    public class DetailsSP
    {

        private SqlConnection sqlcon = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand sqlcmd = new SqlCommand(); 
        #region Functions
        /// <summary>
        /// Function for add the details
        /// </summary>
        /// <param name="infoDetails"></param>
        /// <returns></returns>
        public int DetailsAdd(DetailsInfo infoDetails)
        {
            int retunvalue = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sqlcmd = new SqlCommand("DetailsAdd", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@masterId", SqlDbType.Int).Value = infoDetails.MasterId;
                sqlcmd.Parameters.Add("@name", SqlDbType.VarChar).Value = infoDetails.Name;
                sqlcmd.Parameters.Add("@text", SqlDbType.VarChar).Value = infoDetails.Text;
                sqlcmd.Parameters.Add("@row", SqlDbType.Int).Value = infoDetails.Row;
                sqlcmd.Parameters.Add("@columns", SqlDbType.Int).Value = infoDetails.Columns;
                sqlcmd.Parameters.Add("@width", SqlDbType.Int).Value = infoDetails.Width;
                sqlcmd.Parameters.Add("@dbf", SqlDbType.VarChar).Value = infoDetails.DBF;
                sqlcmd.Parameters.Add("@DorH", SqlDbType.VarChar).Value = infoDetails.DorH;
                sqlcmd.Parameters.Add("@repeat", SqlDbType.VarChar).Value = infoDetails.Repeat;
                sqlcmd.Parameters.Add("@align", SqlDbType.VarChar).Value = infoDetails.Align;
                sqlcmd.Parameters.Add("@repeatAll", SqlDbType.VarChar).Value = infoDetails.RepeatAll;
                sqlcmd.Parameters.Add("@footerRepeatAll", SqlDbType.VarChar).Value = infoDetails.FooterRepeatAll;
                sqlcmd.Parameters.Add("@textWrap", SqlDbType.VarChar).Value = infoDetails.TextWrap;
                sqlcmd.Parameters.Add("@wrapLineCount", SqlDbType.VarChar).Value = infoDetails.WrapLineCount;
                sqlcmd.Parameters.Add("@fieldsForExtra", SqlDbType.VarChar).Value = infoDetails.FieldsForExtra;
                sqlcmd.Parameters.Add("@extraFieldName", SqlDbType.VarChar).Value = infoDetails.ExtraFieldName;
                retunvalue = int.Parse(sqlcmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "DetailsAdd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
            return retunvalue;
        }
        /// <summary>
        /// Function for copy the added details
        /// </summary>
        /// <param name="infoDetails"></param>
        /// <returns></returns>
        public int DetailsCopyAdd(DetailsInfo infoDetails)
        {
            int retunvalue = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sqlcmd = new SqlCommand("DetailsCopyAdd", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@masterId", SqlDbType.Int).Value = infoDetails.MasterId;
                sqlcmd.Parameters.Add("@name", SqlDbType.VarChar).Value = infoDetails.Name;
                sqlcmd.Parameters.Add("@text", SqlDbType.VarChar).Value = infoDetails.Text;
                sqlcmd.Parameters.Add("@row", SqlDbType.Int).Value = infoDetails.Row;
                sqlcmd.Parameters.Add("@columns", SqlDbType.Int).Value = infoDetails.Columns;
                sqlcmd.Parameters.Add("@width", SqlDbType.Int).Value = infoDetails.Width;
                sqlcmd.Parameters.Add("@dbf", SqlDbType.VarChar).Value = infoDetails.DBF;
                sqlcmd.Parameters.Add("@DorH", SqlDbType.VarChar).Value = infoDetails.DorH;
                sqlcmd.Parameters.Add("@repeat", SqlDbType.VarChar).Value = infoDetails.Repeat;
                sqlcmd.Parameters.Add("@align", SqlDbType.VarChar).Value = infoDetails.Align;
                sqlcmd.Parameters.Add("@repeatAll", SqlDbType.VarChar).Value = infoDetails.RepeatAll;
                sqlcmd.Parameters.Add("@footerRepeatAll", SqlDbType.VarChar).Value = infoDetails.FooterRepeatAll;
                sqlcmd.Parameters.Add("@textWrap", SqlDbType.VarChar).Value = infoDetails.TextWrap;
                sqlcmd.Parameters.Add("@wrapLineCount", SqlDbType.VarChar).Value = infoDetails.WrapLineCount;
                sqlcmd.Parameters.Add("@fieldsForExtra", SqlDbType.VarChar).Value = infoDetails.FieldsForExtra;
                sqlcmd.Parameters.Add("@extraFieldName", SqlDbType.VarChar).Value = infoDetails.ExtraFieldName;
                retunvalue = int.Parse(sqlcmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "DetailsCopyAdd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
            return retunvalue;
        }
        /// <summary>
        /// Function for edit the details
        /// </summary>
        /// <param name="infoDetails"></param>
        public void DetailsEdit(DetailsInfo infoDetails)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sqlcmd = new SqlCommand("DetailsEdit", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@detailsId", SqlDbType.Int).Value = infoDetails.DetailsId;
                sqlcmd.Parameters.Add("@masterId", SqlDbType.Int).Value = infoDetails.MasterId;
                sqlcmd.Parameters.Add("@name", SqlDbType.VarChar).Value = infoDetails.Name;
                sqlcmd.Parameters.Add("@text", SqlDbType.Bit).Value = infoDetails.Text;
                sqlcmd.Parameters.Add("@row", SqlDbType.Bit).Value = infoDetails.Row;
                sqlcmd.Parameters.Add("@columns", SqlDbType.Int).Value = infoDetails.Columns;
                sqlcmd.Parameters.Add("@width", SqlDbType.Int).Value = infoDetails.Width;
                sqlcmd.Parameters.Add("@dbf", SqlDbType.Int).Value = infoDetails.DBF;
                sqlcmd.Parameters.Add("@DorH", SqlDbType.VarChar).Value = infoDetails.DorH;
                sqlcmd.Parameters.Add("@repeat", SqlDbType.Int).Value = infoDetails.Repeat;
                sqlcmd.Parameters.Add("@align", SqlDbType.Int).Value = infoDetails.Align;
                sqlcmd.Parameters.Add("@repeatAll", SqlDbType.Int).Value = infoDetails.RepeatAll;
                sqlcmd.Parameters.Add("@footerRepeatAll", SqlDbType.Int).Value = infoDetails.FooterRepeatAll;
                sqlcmd.Parameters.Add("@textWrap", SqlDbType.VarChar).Value = infoDetails.TextWrap;
                sqlcmd.Parameters.Add("@wrapLineCount", SqlDbType.VarChar).Value = infoDetails.WrapLineCount;
                sqlcmd.Parameters.Add("@fieldsForExtra", SqlDbType.VarChar).Value = infoDetails.FieldsForExtra;
                sqlcmd.Parameters.Add("@extraFieldName", SqlDbType.VarChar).Value = infoDetails.ExtraFieldName;
                sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "DetailsEdit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
        }
        /// <summary>
        /// Function for view all the details
        /// </summary>
        /// <param name="MasterId"></param>
        /// <returns></returns>
        public DataTable DetailsViewAll(int MasterId)
        {
            DataTable dtblPurchers = new DataTable();
            try
            {

                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sqldaPurchers = new SqlDataAdapter("DetailsViewAll", sqlcon);
                sqldaPurchers.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldaPurchers.SelectCommand.Parameters.Add("@masterId", SqlDbType.Int).Value = MasterId;
                sqldaPurchers.Fill(dtblPurchers);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "DetailsViewAll", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtblPurchers;
        }
        /// <summary>
        /// Function for view all the copied details
        /// </summary>
        /// <param name="MasterId"></param>
        /// <returns></returns>
        public DataTable DetailsCopyViewAll(int MasterId)
        {
            DataTable dtblPurchers = new DataTable();
            try
            {

                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sqldaPurchers = new SqlDataAdapter("DetailsCopyViewAll", sqlcon);
                sqldaPurchers.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldaPurchers.SelectCommand.Parameters.Add("@masterId", SqlDbType.Int).Value = MasterId;
                sqldaPurchers.Fill(dtblPurchers);
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message, "DetailsViewAll", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtblPurchers;
        }
        /// <summary>
        /// Function for Delete the details
        /// </summary>
        /// <param name="masterId"></param>
        public void DetailsDelete(int masterId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("DetailsDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@masterId", SqlDbType.Int);
                sprmparam.Value = masterId;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DetailsDelete");
            }

            finally
            {
                sqlcon.Close();
            }
        }
        /// <summary>
        /// Function for delete the copied details
        /// </summary>
        /// <param name="masterId"></param>
        public void DetailsCopyDelete(int masterId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("DetailsCopyDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@masterId", SqlDbType.Int);
                sprmparam.Value = masterId;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DetailsCopyDelete");
            }

            finally
            {
                sqlcon.Close();
            }
        }
        #endregion
    }
}
