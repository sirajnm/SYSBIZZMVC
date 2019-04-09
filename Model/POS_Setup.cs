using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sys_Sols_Inventory.Model;
using System.Data;

namespace Sys_Sols_Inventory.Model
{
    class POS_Setup
    {

        private bool _isSingleShift, _isMultiTill;
        public string LocalStoreNo
        {
            get; set;
        }
        public bool IsMultiTill
        {
            get; set;
        }
        public bool IsSingleShift
        {
            get; set;
        }
        public string DefaultTill
        {
            get; set;


        }
        public string CashierID
        {
            get; set;

        }
        public string CashierPassword
        {
            get; set;
        }

        public string POSPrinter
        {
            get; set;
        }

        public string TRNNumber
        {
            get; set;
        }
        public string SalesAccount
        {
            get; set;
        }
        public string VATOutPutAccount
        {
            get; set;
        }
        public string DiscountAccount
        {
            get; set;
        }

public string ReceiptHeader1
        {
            get; set;
        }
public string ReceiptHeader2
        {
            get; set;
        }
public string ReceiptHeader3
        {
            get; set;
        }
public string ReceiptFooter1
        {
            get; set;
        }
public string ReceiptFooter2
        {
            get; set;
        }
public string ReceiptFooter3
        {
            get; set;
        }
public float Pagewidth
        {
            get; set;

        }
public float PageHeight
        {
            get; set;
        }



        public string InsertUpdateMode
        {
            get; set;
        }
        public List<string> Tills = new List<string>();

        public POS_Setup()
        {
            string query = "Select * from POS_Setup";
            DataTable dt = DbFunctions.GetDataTable(query);
            if (dt.Rows.Count == 0)
            {
                InsertUpdateMode = "Insert";
            }
            else
            {
                
            
            foreach(DataRow dr in dt.Rows)
            {
                LocalStoreNo = dr["LocalStoreNo"].ToString();
                IsMultiTill = Convert.ToBoolean(dr["IsMultiTill"]);
                IsSingleShift = Convert.ToBoolean(dr["IsSingleShift"]);
                CashierID = dr["CashierID"].ToString();
                CashierPassword = dr["CashierPassword"].ToString();
                DefaultTill = dr["DefaultTill"].ToString();
                POSPrinter = dr["POSPrinter"].ToString();
                TRNNumber = dr["TRNNumber"].ToString();
                    SalesAccount = dr["SalesAccount"].ToString();
                    DiscountAccount = dr["DiscountAccount"].ToString();
                    VATOutPutAccount = dr["VATOutPutAccount"].ToString();
                ReceiptHeader1 = dr["ReceiptHeader1"].ToString();
                ReceiptHeader2 = dr["ReceiptHeader2"].ToString();
                ReceiptHeader3 = dr["ReceiptHeader1"].ToString();
                ReceiptFooter1 = dr["ReceiptFooter1"].ToString();
                    ReceiptFooter2 = dr["ReceiptFooter2"].ToString();
                    ReceiptFooter3 = dr["ReceiptFooter3"].ToString();
                    Pagewidth = dr["Pagewidth"] != DBNull.Value ? Convert.ToSingle(dr["Pagewidth"]) : 0 ;
                    PageHeight = dr["PageHeight"] != DBNull.Value ?  Convert.ToSingle(dr["PageHeight"]) : 0;


            }
                InsertUpdateMode = "Update";
            }

        }

        public int Update()
        {
            string updatequery = "Update POS_Setup set LocalStoreNo = @LocalStoreNo, IsMultiTill = @IsMultiTill, IsSingleShift = @IsSingleShift, CashierID = @CashierID, CashierPassword = @CashierPassword, DefaultTill = @DefaultTill, POSPrinter = @POSPrinter, TRNNumber = @TRNNumber, SalesAccount = @SalesAccount, DiscountAccount = @DiscountAccount,VATOutPutAccount = @VATOutPutAccount,   ReceiptHeader1 = @ReceiptHeader1, ";
            updatequery += " ReceiptHeader2 = @ReceiptHeader2, ReceiptHeader3 = @ReceiptHeader3, ReceiptFooter1 = @ReceiptFooter1, ReceiptFooter2=@ReceiptFooter2, ReceiptFooter3=@ReceiptFooter3, Pagewidth=@Pagewidth, PageHeight = @PageHeight";
            string insertquery = "Insert Into POS_Setup ([LocalStoreNo],[IsMultiTill],[IsSingleShift],[CashierID],[CashierPassword],[DefaultTill], [POSPrinter], [TRNNumber], [SalesAccount], [DiscountAccount], [VATOutPutAccount], [ReceiptHeader1],[ReceiptHeader2],[ReceiptHeader3],[ReceiptFooter1],[ReceiptFooter2],[ReceiptFooter3],[Pagewidth],[PageHeight]) ";
            insertquery += " values (@LocalStoreNo, @IsMultiTill, @IsSingleShift, @CashierID, @CashierPassword, @DefaultTill, @POSPrinter, @TRNNumber, @SalesAccount, @DiscountAccount, @VATOutPutAccount,  @ReceiptHeader1, @ReceiptHeader2, @ReceiptHeader3, @ReceiptFooter1, @ReceiptFooter2, @ReceiptFooter3, @Pagewidth, @PageHeight )";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@LocalStoreNo", LocalStoreNo);
            parameters.Add("@IsMultiTill", IsMultiTill);
            parameters.Add("@IsSingleShift", IsSingleShift);
            parameters.Add("@CashierID", CashierID);
            parameters.Add("@CashierPassword", CashierPassword);
            parameters.Add("@DefaultTill", DefaultTill);
            parameters.Add("@POSPrinter", POSPrinter);
            parameters.Add("@TRNNumber", TRNNumber);
            parameters.Add("@SalesAccount", SalesAccount);
            parameters.Add("@DiscountAccount", DiscountAccount);
            parameters.Add("@VATOutPutAccount", VATOutPutAccount);
            parameters.Add("@ReceiptHeader1", ReceiptHeader1);
            parameters.Add("@ReceiptHeader2", ReceiptHeader2);
            parameters.Add("@ReceiptHeader3", ReceiptHeader3);
            parameters.Add("@ReceiptFooter1", ReceiptFooter1);
            parameters.Add("@ReceiptFooter2", ReceiptFooter2);
            parameters.Add("@ReceiptFooter3", ReceiptFooter3);
            parameters.Add("@Pagewidth", Pagewidth);
            parameters.Add("@PageHeight", PageHeight);

            if (InsertUpdateMode == "Update")
            {
                return DbFunctions.InsertUpdate(updatequery, parameters);
                
            }
            if (InsertUpdateMode == "Insert")
            {
                return DbFunctions.InsertUpdate(insertquery, parameters);
                
            }
            return 0;

        }

    }
}
