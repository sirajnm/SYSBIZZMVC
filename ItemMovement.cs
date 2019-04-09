using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    public partial class ItemMovement : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();


        public ItemMovement()
        {
            InitializeComponent();
            //cmd.Connection = conn;
        }

        private bool valid()
        {
            return true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                //cmd.CommandType = CommandType.StoredProcedure;
                //if (conn.State == ConnectionState.Open)
                //{
                //}
                //else
                //{

                //    conn.Open();
                //}
                //cmd.CommandText = "ItemMovement";
                //cmd.Parameters.Clear();
                string cmd = "ItemMovement";
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                parameter.Add("@ITEM_CODE", txtCode.Text);
                //cmd.Parameters.AddWithValue("@ITEM_CODE",txtCode.Text);
                SqlDataReader r = Model.DbFunctions.GetDataReaderProcedure(cmd,parameter);
                while (r.Read())
                {
                    txtOpenQty.Text = r["OPENING"].ToString();
                    txtTINQty.Text = r["TRANSFER_IN"].ToString();
                    txtCSRQty.Text = r["CS_RETURN"].ToString();
                    txtCRSRQty.Text = r["CR_RETURN"].ToString();
                    txtPurQty.Text = r["PURCHASE"].ToString();
                    txtSINTotalQty.Text = r["TOTAL"].ToString();
                }
                r.NextResult();
                while (r.Read())
                {
                    txtTOutQty.Text = r["TRANSFER_OUT"].ToString();
                    txtSCRQty.Text = r["CREDIT_SALES"].ToString();
                    txtSCQty.Text = r["CASH_SALES"].ToString();
                    txtPURRQty.Text = r["PURCHASE_RETURN"].ToString();
                    txtSOTTotalQty.Text = r["TOTAL"].ToString();
                } 
                r.NextResult();
                while (r.Read())
                {
                    txtBalQty.Text = r["STOCK_BALANCE"].ToString();
                }
               /// r.Close();
                //cmd.CommandText = "ItemMovementDetails";
                cmd = "ItemMovementDetails";
              ///  r = cmd.ExecuteReader();
                double add = 0;
                double sub = 0;
                double total = 0;
                dgMovements.Rows.Clear();
                while (r.Read())
                {
                    add = add + Convert.ToDouble(r["IN"]);
                    sub = sub + Convert.ToDouble(r["OUT"]);
                    total = add - sub;
                    dgMovements.Rows.Add(r["DOC_NO"], r["DOC_TYPE"], r["DATE"],r["NOTES"], r["IN"], r["OUT"],total);
                }
                r.Close();
                //conn.Close();
                Model.DbFunctions.CloseConnection();
            }
        }

        private void btnCode_Click(object sender, EventArgs e)
        {
            ItemMasterHelp h = new ItemMasterHelp(0);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                txtCode.Text = h.c[0].Value.ToString();
                txtName.Text = h.c[1].Value.ToString();
            }
        }
    }
}
