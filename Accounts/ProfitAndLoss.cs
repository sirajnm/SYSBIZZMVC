using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Accounts
{
    public partial class ProfitAndLoss : Form
    {
        Class.Transactions trans = new Class.Transactions();
        public DataTable ExpenseTbl, IncomeTbl;
        public ProfitAndLoss()
        {
            InitializeComponent();
            adddatatoExpense();
            AddDataToIncome();
        }

        private void ProfitAndLoss_Load(object sender, EventArgs e)
        {

        }
        public void adddatatoExpense()
        {
            ExpenseTbl = new DataTable();
            ExpenseTbl.Columns.Add("Particulars");
            ExpenseTbl.Columns.Add("Amount");
            dataExpense.DataSource = ExpenseTbl;
            DataRow newrow1 = ExpenseTbl.NewRow();
            ExpenseTbl.Rows.Add(newrow1);
            DataRow newrow2 = ExpenseTbl.NewRow();
            newrow2["Particulars"] = "Opening Stock";
            newrow2["Amount"] = Openingstock();
            ExpenseTbl.Rows.Add(newrow2);
            DataRow newrow3 = ExpenseTbl.NewRow();
            newrow3["Particulars"] = "Purchase";
            newrow3["Amount"] = Purchase();
            ExpenseTbl.Rows.Add(newrow3);
        }

        public void AddDataToIncome()
        {
            IncomeTbl = new DataTable();
            IncomeTbl.Columns.Add("Particulars");
            IncomeTbl.Columns.Add("Amount");
            dataIncome.DataSource = IncomeTbl;
            DataRow newrow1 = IncomeTbl.NewRow();
            IncomeTbl.Rows.Add(newrow1);
            DataRow newrow2 = IncomeTbl.NewRow();
            newrow2["Particulars"] = "Closing Stock";
            IncomeTbl.Rows.Add(newrow2);
            DataRow newrow3 = IncomeTbl.NewRow();
            newrow3["Particulars"] = "Sales";
            newrow3["Amount"] = Purchase();
            IncomeTbl.Rows.Add(newrow3);
        }


        public string Openingstock()
        {
            DataTable dt = new DataTable();
            dt = trans.GetOpeningStock(Date_From.Value,Date_To.Value);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "0.00";
            }

        }

        //public string Purchase()
        //{
        //    DataTable dt = new DataTable();
        //    dt = trans.GetOpeningStock(Date_From.Value, Date_To.Value);
        //    if (dt.Rows.Count > 0)
        //    {
        //        return dt.Rows[0][0].ToString();
        //    }
        //    else
        //    {
        //        return "0.00";
        //    }
        //}

        public string Purchase()
        {
            string purchase,Purchaseret;
            DataTable dt = new DataTable();
            dt = trans.Purchase(Date_From.Value, Date_To.Value);
            if (dt.Rows.Count > 0)
            {
                purchase = dt.Rows[0][0].ToString();
                if (purchase == "")
                {
                    purchase = "0.00";
                }
            }
            else
            {
                purchase = "0.00";
            }

            DataTable dt1 = new DataTable();
            dt1 = trans.PurchaseReturn(Date_From.Value, Date_To.Value);
            if (dt.Rows.Count > 0)
            {
                Purchaseret = dt1.Rows[0][0].ToString();
                if(Purchaseret=="")
                {
                    Purchaseret="0.00";
                }
            }
            else
            {
                Purchaseret = "0.00";
            }

            purchase = (Convert.ToDouble(purchase) + Convert.ToDouble(Purchaseret)).ToString();
            return purchase;
        }



        public string Sales()
        {
            string sale, salesret;
            DataTable dt = new DataTable();
            dt = trans.Purchase(Date_From.Value, Date_To.Value);
            if (dt.Rows.Count > 0)
            {
                sale = dt.Rows[0][0].ToString();
            }
            else
            {
                sale = "0.00";
            }

            DataTable dt1 = new DataTable();
            dt1 = trans.PurchaseReturn(Date_From.Value, Date_To.Value);
            if (dt.Rows.Count > 0)
            {
                salesret = dt1.Rows[0][0].ToString();
            }
            else
            {
                salesret = "0.00";
            }

            sale = (Convert.ToDouble(sale) + Convert.ToDouble(salesret)).ToString();
            return sale;
        }
    }
}
