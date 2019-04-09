using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    class StockEntry
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();

        public StockEntry()
        {
            //cmd.Connection = conn;
        }

        public void addStock(string item_id, string qty, string cost_price, string supplier_id, string mrp)
        {
            //cmd.CommandText = "SELECT Id FROM tblStock WHERE item_id = @item_id, AND cost_price = @cost_price AND supplier_id = @supplier_id AND mrp = @mrp";
            //conn.Open();
            //string id = Convert.ToString(cmd.ExecuteScalar());
            //if (id.Equals(""))
            //{
            //    cmd.CommandText = "INSERT INTO tblStock(Item_id, qty, Cost_price, supplier_id, MRP) values(@item_id, @qty, @cost_price, @supplier_id, @mrp)";
            //}
            //else
            //{
            //    cmd.CommandText = "UPDATE tblStock SET qty = @qty WHERE Item_id = @item_id AND Cost_price = @cost_price AND supplier_id = @supplier_id AND MRP = @mrp";
            //}
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@item_id", item_id);
            //cmd.Parameters.AddWithValue("@qty", qty);
            //cmd.Parameters.AddWithValue("@cost_price", (cost_price.Equals("") ? "0" : cost_price));
            //cmd.Parameters.AddWithValue("@mrp", mrp);
            //conn.Close();
        }

        public string addStock_with_batch(string item_id, string qty, string cost_price, string supplier_id, double mrp, DataTable dt_rates, string unit_code, string price_batch, DateTime Exdate, string status, bool hasPriceBatch)
        {

            DateTime mdt = new DateTime();
            //// cmd.CommandText = "SELECT batch_id FROM tblStock WHERE item_id = @item_id, AND cost_price = @cost_price AND supplier_id = @supplier_id AND mrp = @mrp";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@item_id", item_id);
            parameters.Add("@qty", qty);
            parameters.Add("@cost_price", (cost_price.Equals("") ? "0" : cost_price));
            parameters.Add("@mrp", mrp);
            parameters.Add("@supplier_id", supplier_id);
            parameters.Add("@ManBatch", price_batch);
            string Query = "";
            if (hasPriceBatch)
            {
                if (status == "true")
                {
                   // cmd.CommandText = "SELECT  batch_id  FROM tblStock WHERE item_id = @item_id  AND ManBatch=@ManBatch ";
                     Query = "SELECT  batch_id  FROM tblStock WHERE item_id = @item_id  AND ManBatch=@ManBatch ";
                }
                else
                {
                   // cmd.CommandText = "SELECT  batch_id  FROM tblStock WHERE item_id = @item_id AND cost_price = @cost_price   AND mrp = @mrp AND ManBatch=@ManBatch ";
                     Query = "SELECT  batch_id  FROM tblStock WHERE item_id = @item_id AND cost_price = @cost_price   AND mrp = @mrp AND ManBatch=@ManBatch ";
                }
            }
            else
            {
               // cmd.CommandText = "SELECT  batch_id  FROM tblStock WHERE item_id = @item_id AND batch_increment=1 ";
                    Query = "SELECT  batch_id  FROM tblStock WHERE item_id = @item_id AND batch_increment=1 ";
            }
            //if (conn.State == ConnectionState.Open)
            //{
            //    conn.Close();

            //}
            //conn.Open();
            // var id1 = cmd.ExecuteScalar();
            string id = "";
            DataTable dt = DbFunctions.GetDataTable(Query, parameters);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[0][0].ToString();
            }
           
            if (id.Equals(""))
            {
               
                int batch_increment = max_batch_id(item_id);
                string next_batch = item_id + "B" + batch_increment;
             
                Dictionary<string, object> parameters1 = new Dictionary<string, object>();
                parameters1.Add("@batch_id", next_batch);
                parameters1.Add("@item_id", item_id);
                parameters1.Add("@qty", qty);
                parameters1.Add("@cost_price", (cost_price.Equals("") ? "0" : cost_price));
                parameters1.Add("@mrp", mrp);
                parameters1.Add("@supplier_id", supplier_id);
                parameters1.Add("@batch_increment", batch_increment);
                parameters1.Add("@ManBatch", price_batch);
                if (status == "true")
                    parameters1.Add("@Exdate", Exdate);
                else
                {
                    parameters1.Add("@Exdate", DBNull.Value);
                }
                parameters1.Add("@ManDate", DBNull.Value);
             
                Query = "INSERT INTO tblStock(Item_id, qty, Cost_price, supplier_id, MRP,batch_id,batch_increment,ManBatch,Exdate,ManDate) values(@item_id, @qty, @cost_price, @supplier_id, @mrp,@batch_id,@batch_increment,@ManBatch,@Exdate,@ManDate)";
               
                DbFunctions.InsertUpdate(Query, parameters1);
                string query = "INSERT INTO INV_ITEM_PRICE (ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH)";
                for (int i = 0; i < dt_rates.Rows.Count; i++)
                {
                    query += " SELECT '" + item_id + "','" + dt_rates.Rows[i]["Rate_type"].ToString() + "','" + dt_rates.Rows[i]["rate"].ToString() + "','" + unit_code + "','" + next_batch + "','001'";
                    query += " UNION ALL ";
                }
                query = query.Substring(0, query.Length - 10);
               
                DbFunctions.InsertUpdate(query);
                return next_batch;

            }
            else
            {
                if (status == "true")
                {
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}
                    //conn.Open();
                    //SqlCommand cmd3 = new SqlCommand();
                    //cmd3.Parameters.Clear();
                    //cmd3.CommandText = "";
                    //cmd3.Connection = conn;
                    //cmd3.CommandType = CommandType.Text;
                  parameters.Clear();
                  Query= "UPDATE tblStock SET Exdate =@date,Qty=Qty+@Qty1,Cost_price=@cost_price WHERE Item_id = @item_id1 AND batch_id = @batch_id ";
                  parameters.Add("@date", Exdate);
                  parameters.Add("@item_id1", item_id);
                  parameters.Add("@batch_id", id);
                  parameters.Add("@Qty1", qty);
                  parameters.Add("@ManBatch", price_batch);
                  parameters.Add("@cost_price", (cost_price.Equals("") ? "0" : cost_price));
                    //cmd3.Parameters.AddWithValue("@date", Exdate);
                    //cmd3.Parameters.AddWithValue("@item_id1", item_id);
                    //cmd3.Parameters.AddWithValue("@batch_id", id);
                    //cmd3.Parameters.AddWithValue("@Qty1", qty);
                    //cmd3.Parameters.AddWithValue("@ManBatch", price_batch);
                    //cmd3.Parameters.AddWithValue("@cost_price", (cost_price.Equals("") ? "0" : cost_price));

                    try
                    {
                        int i = DbFunctions.InsertUpdate(Query, parameters);                  
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show("" + e);
                    }
                    updatePriceTable(dt_rates, item_id, unit_code, id);
                }
                else
                {
                    //if (conn.State == ConnectionState.Open)
                    //    conn.Close();
                    //conn.Open();
                    //cmd.Parameters.Clear();
                    //cmd.CommandText = "";
                    //cmd.CommandType = CommandType.Text;

                    parameters.Clear();
                    Query = "UPDATE tblStock SET Exdate =@date,Qty=Qty+@Qty1 WHERE Item_id = @item_id1 AND batch_id = @batch_id ";
                   
                   // cmd.CommandText = "UPDATE tblStock SET Exdate =@date,Qty=Qty+@Qty1 WHERE Item_id = @item_id1 AND batch_id = @batch_id ";
                    parameters.Add("@date", DBNull.Value);
                    parameters.Add("@item_id1", item_id);
                    parameters.Add("@batch_id", id);
                    parameters.Add("@Qty1", qty);
                    parameters.Add("@ManBatch", price_batch);


                    //cmd.Parameters.AddWithValue("@date", DBNull.Value);
                    //cmd.Parameters.AddWithValue("@item_id1", item_id);
                    //cmd.Parameters.AddWithValue("@batch_id", id);
                    //cmd.Parameters.AddWithValue("@Qty1", qty);
                    //cmd.Parameters.AddWithValue("@ManBatch", price_batch);
                    try
                    {
                        int i = DbFunctions.InsertUpdate(Query, parameters);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("" + e);
                    }
                }
                return id;
              //  conn.Close();
            }

         ///   conn.Close();



            //  return id;
        }
        public int max_batch_id(string item_id)
        {
            //if (conn.State == ConnectionState.Open)
            //{
            //    conn.Close();
            //}
            int id = 0;
          //  cmd.CommandText = "SELECT  isnull( max(batch_increment),0) as batch FROM tblStock WHERE item_id = @item_code";
            string Query = "SELECT  isnull( max(batch_increment),0) as batch FROM tblStock WHERE item_id = @item_code";
           // conn.Open();
           // cmd.Parameters.Clear();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@item_code", item_id);
           // cmd.Parameters.AddWithValue("@item_code", item_id);
            id = Convert.ToInt32(DbFunctions.GetAValue(Query,parameters));
            id += 1;
            return id;
        }
        public int MaxBatchId(string item_id)
        {
            //if (conn.State == ConnectionState.Open)
            //{
            //    conn.Close();
            //}
            int id = 0;
            //cmd.CommandText = "SELECT  isnull( max(batch_increment),0) as batch FROM tblStock WHERE item_id = @item_code";
            string Query = "SELECT  isnull( max(batch_increment),0) as batch FROM tblStock WHERE item_id = @item_code";
            
            //conn.Open();
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@item_code", item_id);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@item_code", item_id);
            id = Convert.ToInt32(DbFunctions.GetAValue(Query, parameters));
            
            return id;
        }
        public String MinBatchId(string item_id)
        {
            
            int id = 0;
            string batch = ""; 
           
            string Query = "SELECT  isnull( Min(batch_increment),0) as batch FROM tblStock WHERE item_id = @item_code";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@item_code", item_id);
            id = Convert.ToInt32(DbFunctions.GetAValue(Query, parameters));
           // id = Convert.ToInt32(cmd.ExecuteScalar());
           // conn.Close();
            if (id > 0)
            {
                //cmd.CommandText = "SELECT batch_id as batch FROM tblStock WHERE batch_increment='"+id+"' and item_id ='"+item_id+"'";
                //conn.Open();
               // cmd.Parameters.Clear();
                //cmd.Parameters.AddWithValue("@item_code", item_id);
                Query = "SELECT batch_id as batch FROM tblStock WHERE batch_increment='" + id + "' and item_id ='" + item_id + "'";
                batch = DbFunctions.GetAValue(Query).ToString();
               /// conn.Close();
            }
           return batch;
        }
        public void addStock(string item_id, string qty, string cost_price, string supplier_id)
        {
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@item_id", item_id);
            //cmd.Parameters.AddWithValue("@qty", qty);
            //cmd.Parameters.AddWithValue("@cost_price", (cost_price.Equals("") ? "0" : cost_price));
            //cmd.Parameters.AddWithValue("@supplier_id", supplier_id);
            //cmd.CommandText = "SELECT id  FROM tblStock WHERE item_id = @item_id AND cost_price = @cost_price AND supplier_id = @supplier_id";
            //conn.Open();
            string Query = "SELECT id  FROM tblStock WHERE item_id = @item_id AND cost_price = @cost_price AND supplier_id = @supplier_id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@item_id", item_id);
            parameters.Add("@qty", qty);
            parameters.Add("@cost_price", (cost_price.Equals("") ? "0" : cost_price));
            parameters.Add("@supplier_id", supplier_id);
            string id = Convert.ToString(DbFunctions.GetAValue(Query,parameters));
            if (id.Equals(""))
            {
                Query= "INSERT INTO tblStock(Item_id, qty, Cost_price, supplier_id) values(@item_id, @qty, @cost_price, @supplier_id)";
            }
            else
            {
                Query = "UPDATE tblStock SET qty = qty + @qty WHERE Item_id = @item_id AND Cost_price = @cost_price AND supplier_id = @supplier_id";
            }
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //cmd.Parameters.Clear();
            DbFunctions.InsertUpdate(Query);
        }
           public void addStockWithBatch(string item_id, string qty, string cost_price,string price_id)
        {
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@item_id", item_id);
            //cmd.Parameters.AddWithValue("@qty", qty);
            //cmd.Parameters.AddWithValue("@cost_price", (cost_price.Equals("") ? "0" : cost_price));
            //cmd.Parameters.AddWithValue("@price_id", price_id);
            //cmd.CommandText = "SELECT id  FROM tblStock WHERE item_id = @item_id AND batch_id = @price_id";
            //if (conn.State == ConnectionState.Open)
            //{
            //    conn.Close();
            //}
            //conn.Open();
            string Query = "SELECT id  FROM tblStock WHERE item_id = @item_id AND batch_id = @price_id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@item_id", item_id);
            parameters.Add("@qty", qty);
            parameters.Add("@cost_price", (cost_price.Equals("") ? "0" : cost_price));
            parameters.Add("@price_id", price_id);
            string id = Convert.ToString(DbFunctions.GetAValue(Query,parameters));
            if (id.Equals(""))
            {
               Query= "INSERT INTO tblStock(Item_id, qty, Cost_price, batch_id) values(@item_id, @qty, @cost_price, @price_id)";
            }
            else
            {
               Query= "UPDATE tblStock SET qty = qty + @qty WHERE Item_id = @item_id AND batch_id = @price_id";
            }
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //cmd.Parameters.Clear();
            DbFunctions.InsertUpdate(Query,parameters);
           // DbFunctions.GetConnection();
        }
           public void updatePriceTable(DataTable dt_rates, string item_id, string unit_code,string next_batch)
           {
               //SqlCommand cmd1 = new SqlCommand();
               //cmd1.Connection = conn;
               //cmd1.Parameters.Clear();
               //cmd1.Dispose();
               //cmd1.CommandText = "";
               string query = "DELETE FROM INV_ITEM_PRICE WHERE BATCH_ID='" + next_batch + "';INSERT INTO INV_ITEM_PRICE (ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH)";
               for (int i = 0; i < dt_rates.Rows.Count; i++)
               {
                   query += " SELECT '" + item_id + "','" + dt_rates.Rows[i]["Rate_type"].ToString() + "','" + dt_rates.Rows[i]["rate"].ToString() + "','" + unit_code + "','" + next_batch + "','001'";
                   query += " UNION ALL ";
               }
               query = query.Substring(0, query.Length - 10);
               DbFunctions.InsertUpdate(query);
               //cmd1.CommandText += query;
               //if (conn.State == ConnectionState.Open)
               //{
               //    conn.Close();
               //}
               //conn.Open();
               //cmd1.ExecuteNonQuery();
           }
    }
}
