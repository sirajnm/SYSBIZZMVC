using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory.Manufacture
{
    class ProductionMaster
    {
        public delegate void CalculationUpdated(double productionCost, double damageCost, double otherExpense);
        public event CalculationUpdated  CalculationUpdatedEventHandler;
        //private SqlConnection conn = Model.DbFunctions.GetConnection();
        //private SqlCommand cmd = new SqlCommand();
        DataTable dt_rates = new DataTable();
        private String _DocId;
        public String DocId
        {
            get
            {
                return _DocId;
            }
            set
            {
                _DocId = value;
                if (value.Equals(""))
                {
                    ResetData();
                }
                else
                {
                    LoadData(_DocId);
                }
            }
        }
        public DateTime Date { get; set; }
        public double ProductionCost { get; set; }
        public double DamageCost { get; set; }
        public double OtherExpense { get; set; }
        public bool hasPriceBatch { get; set; }
        public BindingList<ProductionItem> Items { get; set; }
        public BindingList<ProductionOtherCharge> OtherCharges { get; set; }
        public BindingList<ProductionRawMaterial> RawMaterials { get; set; }

        public ProductionMaster(String docId = "")
        {
            //cmd.Connection = conn;
            Items = new BindingList<ProductionItem>();
            OtherCharges = new BindingList<ProductionOtherCharge>();
            RawMaterials = new BindingList<ProductionRawMaterial>();
            DocId = docId;

            RawMaterials.ListChanged += RawMaterials_ListChanged;
            OtherCharges.ListChanged += OtherCharges_ListChanged;
            bindPriceTable();
        }

        void OtherCharges_ListChanged(object sender, ListChangedEventArgs e)
        {
            OtherExpense = 0;
            foreach (var expense in OtherCharges)
            {
                OtherExpense += expense.Amount;
            }
            CalculationUpdatedEventHandler(ProductionCost, DamageCost, OtherExpense);
        }

        void RawMaterials_ListChanged(object sender, ListChangedEventArgs e)
        {
            ProductionCost = 0;
            DamageCost = 0;
            foreach (var material in RawMaterials)
            {
                double damageAmount = material.DamageQty * material.CostPrice;
                double totalAmount = material.CostPrice * (material.Qty + material.DamageQty);
                ProductionCost += totalAmount;
                DamageCost += damageAmount;
            }
            CalculationUpdatedEventHandler(ProductionCost, DamageCost, OtherExpense);
        }

        private void LoadData(String docId)
        {
            //open connection.
            Model.DbFunctions.GetConnection();

            ResetData();
            string query = "SELECT TOP 1 CONVERT(NVARCHAR(50), ProductionDate, 103) as ProductionDate, ProductionCost, DamageCost, OtherExpense FROM ProductionMaster WHERE Id = @id";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@id", docId);
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@id", docId);
            SqlDataReader r = DbFunctions.GetDataReader(query, Parameters);
            if (r.HasRows)
            {
                r.Read();
                Date = DateTime.ParseExact(r["ProductionDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ProductionCost = Convert.ToDouble(r["ProductionCost"]);
                DamageCost = Convert.ToDouble(r["DamageCost"]);
                OtherExpense = Convert.ToDouble(r["OtherExpense"]);
            }
            else
            {
                Model.DbFunctions.CloseConnection();
                return;
            }
            DbFunctions.CloseConnection();

            //Load Products
            query = "SELECT ProductionProducts.*, items.DESC_ENG as ItemName FROM ProductionProducts LEFT JOIN INV_ITEM_DIRECTORY as items ON ProductionProducts.ItemCode = items.CODE WHERE ProductionId = @productionId";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@productionId", DocId);
            Parameters = new Dictionary<string, object>();
            Parameters.Add("@productionId", docId);
            r = DbFunctions.GetDataReader(query, Parameters);
            while (r.Read())
            {
                ProductionItem item = new ProductionItem(
                    Convert.ToString(r["ItemCode"]),
                    Convert.ToString(r["ItemName"]),
                    Convert.ToString(r["Batch"]),
                    Convert.ToString(r["PriceBatch"]),
                    Convert.ToDateTime(r["ExpiryDate"]),
                    Convert.ToString(r["UOM"]),
                    Convert.ToDouble(r["PackSize"]),
                    Convert.ToDouble(r["Qty"]),
                    Convert.ToDouble(r["CostPrice"]),
                    Convert.ToDouble(r["MRP"]),
                    Convert.ToDouble(r["Total"])
                    );
                Items.Add(item);

            }
            DbFunctions.CloseConnection();

            //Load Raw Materials
            query = "SELECT ProductionRawMaterials.*, items.DESC_ENG as ItemName FROM ProductionRawMaterials LEFT JOIN INV_ITEM_DIRECTORY AS items ON ProductionRawMaterials.ItemCode = items.CODE WHERE ProductionId = @productionId";
            Parameters = new Dictionary<string, object>();
            Parameters.Add("@productionId", DocId);
            //cmd.Parameters.AddWithValue("@productionId", DocId);
            r = DbFunctions.GetDataReader(query, Parameters);
            while (r.Read())
            {
                ProductionRawMaterial rawMaterial = new ProductionRawMaterial(
                    Convert.ToString(r["ItemCode"]),
                    Convert.ToString(r["ItemName"]),
                    Convert.ToString(r["Batch"]),
                    Convert.ToString(r["UOM"]),
                    Convert.ToDouble(r["PackSize"]),
                    Convert.ToDouble(r["Qty"]),
                    Convert.ToDouble(r["CostPrice"]),
                    Convert.ToDouble(r["DamagePackSize"]),
                    Convert.ToString(r["DamageUOM"]),
                    Convert.ToDouble(r["DamageQty"])
                    );
                RawMaterials.Add(rawMaterial);
            }
            DbFunctions.CloseConnection();

            //Load Other Expenses.
            query = "SELECT * FROM ProductionOtherExpenses WHERE ProductionId = @productionId";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@productionId", DocId);
            //r = cmd.ExecuteReader();
            Parameters = new Dictionary<string, object>();
            Parameters.Add("@productionId", DocId);
            //cmd.Parameters.AddWithValue("@productionId", DocId);
            r = DbFunctions.GetDataReader(query, Parameters);
            while(r.Read())
            {
                ProductionOtherCharge otherCharge = new ProductionOtherCharge(Convert.ToString(r["Description"]), Convert.ToDouble(r["Amount"]));
                OtherCharges.Add(otherCharge);
            }

            //close connection.
            Model.DbFunctions.CloseConnection();
        }

        private void ResetData()
        {
            Date = DateTime.Now;
            Items.Clear();
            RawMaterials.Clear();
            OtherCharges.Clear();
        }

        public void ResetDocId()
        {
            DocId = "";
        }

        public void AddItem(ProductionItem item)
        {
            Items.Add(item);
          //  Items.Insert()
        }
         public void AddItemByIndex(ProductionItem item,int index)
        {
           // Items.Add(item);
            Items.RemoveAt(index);
            Items.Insert(index, item);
        }
        public void RemoveItem(int index)
        {
            Items.RemoveAt(index);
        }

        public void AddRawMaterial(ProductionRawMaterial rawMaterial)
        {
            RawMaterials.Add(rawMaterial);
        }

        public void RemoveRawMaterial(int index)
        {
            RawMaterials.RemoveAt(index);
        }

        public void AddOtherCharge(ProductionOtherCharge otherCharge)
        {
            OtherCharges.Add(otherCharge);
        }

        public void RemoveOtherCharge(int index)
        {
            OtherCharges.RemoveAt(index);
        }
        public void bindPriceTable()
        {
            
            dt_rates.Columns.Add("Rate_type", typeof(string));
            dt_rates.Columns.Add("rate", typeof(double));
            Model.DbFunctions.GetConnection();
            String query = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";
            SqlDataReader r = DbFunctions.GetDataReader(query);
            while (r.Read())
            {
                dt_rates.Rows.Add(r[0].ToString(),0);
              
            }
            
            DbFunctions.CloseConnection();
        }
        StockEntry stockEntry = new StockEntry();
        public void Save()
        {
            string date="";
            //conn.Open();
            Model.DbFunctions.GetConnection();
            //INSERT MASTER
            //cmd.Parameters.Clear();
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@id", _DocId);
            Parameters.Add("@productionDate", Date);
            Parameters.Add("@productionCost", ProductionCost);
            Parameters.Add("@damageCost", DamageCost);
            Parameters.Add("@otherExpense", OtherExpense);
            if (_DocId.Equals(""))
            {
                //insert.
                String query = "INSERT INTO ProductionMaster Values(@productionDate, @productionCost, @damageCost, @otherExpense);SELECT Id FROM ProductionMaster WHERE Id = @@IDENTITY";
                //_DocId = Convert.ToString(cmd.ExecuteScalar());
                _DocId = Convert.ToString(DbFunctions.GetAValue(query,Parameters));
            }
            else
            {
                //update.
                String query = "UPDATE ProductionMaster SET ProductionDate = @productionDate, ProductionCost = @productionCost, DamageCost = @damageCost, OtherExpense = @otherExpense WHERE Id = @id";
                //cmd.ExecuteNonQuery();
                DbFunctions.InsertUpdate(query);
                StockReduce();
            }

            //DELETE PRODUCTS, RAW MATERIALS, OTHER EXPENSES.
            Delete();
            //if (conn.State == ConnectionState.Closed)
            //{
            //    Model.DbFunctions.GetConnection();
            //}
           // conn.Open();
            //INSERT PRODUCT
            if (Items.Count > 0)
            {
                string query = "INSERT INTO ProductionProducts(ProductionId, ItemCode, Batch,PriceBatch,ExpiryDate, UOM, PackSize, Qty, CostPrice, MRP, Total) VALUES ";
                foreach (var item in Items)
                {

                    for (int i = 0; i < dt_rates.Rows.Count; i++)
                    {
                        if (dt_rates.Rows[i][0].ToString() == "PUR")
                        {
                            dt_rates.Rows[i][1] = item.CostPrice;
                        }
                        else if (dt_rates.Rows[i][0].ToString() == "MRP")
                        {
                            dt_rates.Rows[i][1] = (!item.MRP.Equals("")) ? item.MRP : 0 ;
                        }
                    }
                     string flag = "";
                    if (item.Batch == null)
                        flag = "false";
                    else
                        flag = "true";
                  
                    double TotralQty = item.Qty * item.PackSize;
                    string PRICE_BATCH = stockEntry.addStock_with_batch(item.ItemCode, TotralQty.ToString(), item.CostPrice.ToString(), "", item.MRP, dt_rates, item.UOM, item.Batch, item.ExDate, flag,hasPriceBatch);
                    //query += " ('" + _DocId + "','" + item.ItemCode + "','" + item.Batch + "','" +PRICE_BATCH + "','" + item.ExDate.ToShortDateString() + "','" + item.UOM + "','" + item.PackSize + "','" + item.Qty + "','" + item.CostPrice + "','" + item.MRP + "','" + item.Total + "'),";
                    query += " ('" + _DocId + "','" + item.ItemCode + "','" + item.Batch + "','" + PRICE_BATCH + "', @d1 ,'" + item.UOM + "','" + item.PackSize + "','" + item.Qty + "','" + item.CostPrice + "','" + item.MRP + "','" + item.Total + "'),";
                    date = item.ExDate.ToShortDateString();
                }
                query = query.Substring(0, query.Length - 1);
                Parameters = new Dictionary<string, object>();
                Parameters.Add("@d1",date);
                //cmd.ExecuteNonQuery();
                DbFunctions.InsertUpdate(query, Parameters);
            }

            
            //INSERT RAW MATERIALS
            if (RawMaterials.Count > 0)
            {
                string query = "INSERT INTO ProductionRawMaterials(ProductionId, ItemCode, Batch, UOM, PackSize, CostPrice, Qty, DamagePackSize, DamageUOM, DamageQty) VALUES ";
                foreach (var raw in RawMaterials)
                {
                    query += " ('"+_DocId+"','"+raw.ItemCode+"','"+raw.Batch+"','"+raw.UOM+"','"+raw.PackSize+"','"+raw.CostPrice+"','"+raw.Qty+"','"+raw.DamagePackSize+"','"+raw.DamageUOM+"','"+raw.DamageQty+"'),";
                    string item_id = raw.ItemCode;
                    double qty = (raw.Qty * raw.PackSize) +(raw.DamagePackSize*raw.DamageQty);
                    qty = -1 * qty;
                  
                    stockEntry.addStockWithBatch(item_id, qty.ToString(), raw.CostPrice.ToString(), raw.Batch);
                }
                query = query.Substring(0, query.Length - 1);
                DbFunctions.InsertUpdate(query);
                //cmd.ExecuteNonQuery();
            }

            
            //INSERT OTHER CHARGES
            if (OtherCharges.Count > 0)
            {
                string query = "INSERT INTO ProductionOtherExpenses(ProductionId, Description, Amount) VALUES ";
                foreach (var charge in OtherCharges)
                {
                    query += " ('"+_DocId+"','"+charge.Description+"','"+charge.Amount+"'),";
                }
                query = query.Substring(0, query.Length - 1);
                //String query = query;
                //if (conn.State == ConnectionState.Closed)
                //{
                //    Model.DbFunctions.GetConnection();
                //}
                DbFunctions.InsertUpdate(query);
                //cmd.ExecuteNonQuery();
            }


            //Model.DbFunctions.CloseConnection();
        }

        public void Delete()
        {
            if (!_DocId.Equals(""))
            {
               // StockReduce();
                bool didThisMethodOpenedConnection = false;
                //if (conn.State == System.Data.ConnectionState.Closed)
                //{
                    Model.DbFunctions.GetConnection();
                    didThisMethodOpenedConnection = true;
                //}
                //DELETE PRODUCT
                String query = "DELETE FROM ProductionProducts WHERE ProductionId = " + _DocId;
                //cmd.ExecuteNonQuery();
                DbFunctions.InsertUpdate(query);
                //DELETE RAW MATERIALS
                query = "DELETE FROM ProductionRawMaterials WHERE ProductionId = " + _DocId;
                //cmd.ExecuteNonQuery();
                DbFunctions.InsertUpdate(query);
                //DELETE OTHER CHARGES
                query = "DELETE FROM ProductionOtherExpenses WHERE ProductionId = " + _DocId;
                //cmd.ExecuteNonQuery();
                DbFunctions.InsertUpdate(query);

                if (didThisMethodOpenedConnection)
                {
                    Model.DbFunctions.CloseConnection();
                }
            }
            
        }
        public void DeleteInvoice()
        {
            if (!_DocId.Equals(""))
            {
                StockReduce();
                bool didThisMethodOpenedConnection = false;
                //if (conn.State == System.Data.ConnectionState.Closed)
                //{
                //    Model.DbFunctions.GetConnection();
                //    didThisMethodOpenedConnection = true;
                //}
                //DELETE PRODUCT
                String query = "DELETE FROM ProductionProducts WHERE ProductionId = " + _DocId;
                //cmd.ExecuteNonQuery();
                DbFunctions.InsertUpdate(query);
                //DELETE RAW MATERIALS
                query = "DELETE FROM ProductionRawMaterials WHERE ProductionId = " + _DocId;
                //cmd.ExecuteNonQuery();
                DbFunctions.InsertUpdate(query);
                //DELETE OTHER CHARGES
                query = "DELETE FROM ProductionOtherExpenses WHERE ProductionId = " + _DocId;
                //cmd.ExecuteNonQuery();
                DbFunctions.InsertUpdate(query);

                //if (didThisMethodOpenedConnection)
                //{
                //    Model.DbFunctions.CloseConnection();
                //}
            }

        }
        public void StockReduce()
        {
            //SqlCommand reduceStockCommand = new SqlCommand();
            //reduceStockCommand.Connection = conn;
            //if (conn.State==ConnectionState.Open)
            //{
            //    Model.DbFunctions.CloseConnection();
            //}
            //Model.DbFunctions.GetConnection();
            string query = "SELECT ItemCode,Qty,PackSize, CostPrice,PriceBatch FROM ProductionProducts WHERE ProductionId = " + _DocId;
            //SqlDataReader r = reduceStockCommand.ExecuteReader();
            DataTable r =DbFunctions.GetDataTable(query);
            StockEntry se = new StockEntry();



            for (int i = 0; i < r.Rows.Count;i++)
            {
                double qty = -1 * (Convert.ToDouble(r.Rows[i]["Qty"]) * Convert.ToDouble(r.Rows[i]["PackSize"]));
                //if (type.Equals("LGR.PRT"))
                //{
                //    qty = -1 * qty;
                //}
                se.addStockWithBatch(Convert.ToString(r.Rows[i]["ItemCode"]), Convert.ToString(qty), Convert.ToString(r.Rows[i]["CostPrice"]), Convert.ToString(r.Rows[i]["PriceBatch"]));
            }
            //r.Close();
            //reduceStockCommand.CommandText = "";
            query = "SELECT ItemCode,Qty,PackSize, CostPrice,Batch,DamageQty,DamagePackSize FROM ProductionRawMaterials WHERE ProductionId = " + _DocId;
            r = new DataTable();
            r = DbFunctions.GetDataTable(query);

            for (int i = 0; i < r.Rows.Count; i++)
            {
                double qty = (Convert.ToDouble(r.Rows[i]["Qty"]) * Convert.ToDouble(r.Rows[i]["PackSize"])) + (Convert.ToDouble(r.Rows[i]["DamageQty"]) * Convert.ToDouble(r.Rows[i]["DamagePackSize"]));

                se.addStockWithBatch(Convert.ToString(r.Rows[i]["ItemCode"]), Convert.ToString(qty), Convert.ToString(r.Rows[i]["CostPrice"]), Convert.ToString(r.Rows[i]["Batch"]));
            }
           
            Model.DbFunctions.CloseConnection();
        }
    }
}
