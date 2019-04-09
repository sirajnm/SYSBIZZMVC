using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Sys_Sols_Inventory.Model
{
    class POS_Repositery 
    {
        public static List<POS_PaymentType> GetAllPaymentTypes()
        {
            List<POS_PaymentType> paymenttypes = new List<POS_PaymentType>();
            string query = "Select * from POS_PaymentTypes";
            DataTable dt = DbFunctions.GetDataTable(query);
            foreach (DataRow dr in dt.Rows)
            {
                POS_PaymentType pt = new POS_PaymentType
                {
                    CurrencyCode = dr["CurrencyCode"].ToString(),
                    PaymentCode = dr["PaymentCode"].ToString(),
                    PaymentDescription = dr["PaymentDescription"].ToString(),
                    PaymentType = dr["PaymentType"].ToString(),
                    GLAccount = dr["GLAccount"].ToString(),
                    RefundAllowed = Convert.ToBoolean(dr["RefundAllowed"]),
                    OverTenderAllowed = Convert.ToBoolean(dr["OverTenderAllowed"])

                };
                paymenttypes.Add(pt);


            }
            return paymenttypes;
        }
        
        public static List<TransactionHeaderTEMP> GetAllTransactions(string shiftno)
        {
            List<TransactionHeaderTEMP> transactions = new List<TransactionHeaderTEMP>();
            
            string query = "Select * from POS_Transaction_Header where TransactionStatus = 'Closed' and ShiftNo = @ShiftNo ";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ShiftNo", shiftno);
            DataTable dt = DbFunctions.GetDataTable(query, parameter); 
            foreach (DataRow dr in dt.Rows)
            {
                TransactionHeaderTEMP transhead = ProcessDataTable(dr);
                transactions.Add(transhead);
            }
            return transactions;
        }
        public static List<TransactionHeaderTEMP> SuspendedTransactions()
        {
            List<TransactionHeaderTEMP> transactions = new List<TransactionHeaderTEMP>();

            string query = "Select * from POS_Transaction_Header where TransactionStatus = 'Suspened'";
            DataTable dt = DbFunctions.GetDataTable(query);
            foreach (DataRow dr in dt.Rows)
            {
                TransactionHeaderTEMP transhead = ProcessDataTable(dr);
                transactions.Add(transhead);
            }
            return transactions;
        }

        public static List<TransactionHeaderTEMP> PayonDeliveryTransactions()
        {
            List<TransactionHeaderTEMP> transactions = new List<TransactionHeaderTEMP>();
            string query = "Select * from POS_Transaction_Header where TransactionStatus = 'PayOnDelivery'";
            DataTable dt = DbFunctions.GetDataTable(query);
            foreach (DataRow dr in dt.Rows)
            {
                TransactionHeaderTEMP transhead = ProcessDataTable(dr);
                transhead.TransactionDetails = POS_Repositery.TransactionDetailsTemp(transhead.ShiftNo, transhead.Branch, transhead.TillID, transhead.TransactionNo);
                transactions.Add(transhead);
            }
            return transactions;


        }

        public static List<TransactionHeaderTEMP> DeliveryTransactions()
        {
            List<TransactionHeaderTEMP> transactions = new List<TransactionHeaderTEMP>();
            string query = "Select * from POS_Transaction_Header where TransactionStatus = 'Delivery'";
            DataTable dt = DbFunctions.GetDataTable(query);
            foreach (DataRow dr in dt.Rows)
            {
                TransactionHeaderTEMP transhead = ProcessDataTable(dr);
                transhead.TransactionDetails = POS_Repositery.TransactionDetailsTemp(transhead.ShiftNo, transhead.Branch, transhead.TillID, transhead.TransactionNo);
                transactions.Add(transhead);
            }
            return transactions;

        }

        public static bool IsPayOnDelivery(string trans)
        {
            string query = "Select count(*) Recs from POS_CashOnDeliveryEntries where TransactionNo = @TransactionNo";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@TransactionNo", trans);
            var obj = DbFunctions.GetAValue(query, parameters);

            int val = obj == DBNull.Value ? 0 : Convert.ToInt16(obj);
            return val >= 1 ? true : false;

        }

        public static TransactionHeaderTEMP ReceivePayments(string shiftno, string branch, string tillid, string transno)
        {
            TransactionHeaderTEMP transhead = new TransactionHeaderTEMP();
            string query = "Select * from POS_Transaction_Header where ShiftNo = @ShiftNo and Branch=@Branch and TillID=@TillID and TransactionNo=@TransactionNo";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ShiftNo", shiftno);
            parameter.Add("@Branch", branch);
            parameter.Add("@TillID", tillid);
            parameter.Add("@TransactionNo", transno);
            DataTable dt = DbFunctions.GetDataTable(query, parameter);
            foreach (DataRow dr in dt.Rows)
            {
                transhead = ProcessDataTable(dr);
                transhead.TransactionDetails = POS_Repositery.TransactionDetailsTemp(transhead.ShiftNo, transhead.Branch, transhead.TillID, transhead.TransactionNo);
                
            }

            return transhead;

        }

        public static TransactionHeaderTEMP RetreiveTransaction(string shiftno, string branch,  string tillid, string transno)
        {
            TransactionHeaderTEMP transhead = new TransactionHeaderTEMP();
            string query = "Select * from POS_Transaction_Header where ShiftNo = @ShiftNo and Branch=@Branch and TillID=@TillID and TransactionNo=@TransactionNo";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ShiftNo", shiftno);
            parameter.Add("@Branch", branch);
            parameter.Add("@TillID", tillid);
            parameter.Add("@TransactionNo", transno);
            DataTable dt = DbFunctions.GetDataTable(query, parameter);
            foreach(DataRow dr in dt.Rows)
            {
                 transhead = ProcessDataTable(dr);
               
                
            }
            transhead.TransactionDetails = POS_Repositery.TransactionDetailsTemp(transhead.ShiftNo, transhead.Branch, transhead.TillID, transhead.TransactionNo);
            transhead.TransactionPayments = POS_Repositery.PaymentLines(transhead.ShiftNo, transhead.Branch, transhead.TillID, transhead.TransactionNo);
            transhead.TransactionStatus = "Open";
            transhead.Update();
            return transhead;

        }

        public static List<TransactionDetailTEMP> TransactionDetailsTemp(string shiftno, string branch, string tillid, string transno)
        {
            List<TransactionDetailTEMP> transdetaillist = new List<TransactionDetailTEMP>();
            string detaiquery = "Select * from POS_Transaction_Detail Where ShiftNo = @ShiftNo and Branch=@Branch and TillID=@TillID and TransactionNo=@TransactionNo";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ShiftNo", shiftno);
            parameter.Add("@Branch", branch);
            parameter.Add("@TillID", tillid);
            parameter.Add("@TransactionNo", transno);
            DataTable dt = DbFunctions.GetDataTable(detaiquery, parameter);
            foreach (DataRow dr in dt.Rows)
            {
                TransactionDetailTEMP transactionDetail = new TransactionDetailTEMP();

                 transactionDetail.ShiftNo = dr["ShiftNo"].ToString();
                  transactionDetail.Branch = dr["Branch"].ToString();
                  transactionDetail.TillID = dr["TillID"].ToString();
                  transactionDetail.TransactionNo = dr["TransactionNo"].ToString();
                  transactionDetail.LineNo = dr["LineNo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["LineNo"]);
                  transactionDetail.ItemNo = dr["ItemNo"].ToString();
                  transactionDetail.ItemDescription = dr["ItemDescription"].ToString();
                  transactionDetail.Barcode = dr["Barcode"].ToString();
                  transactionDetail.UOM = dr["UOM"].ToString();
                transactionDetail.PriceVAT = dr["PriceVAT"] == DBNull.Value ? 0 : Convert.ToSingle(dr["PriceVAT"]);
                transactionDetail.UOMQty = dr["UOMQty"] == DBNull.Value ? 0 : Convert.ToSingle(dr["UOMQty"]);
                 transactionDetail.Quantity = dr["Quantity"] == DBNull.Value ? 0 : Convert.ToSingle(dr["Quantity"]);
                transactionDetail.AmountForPay = dr["AmountForPay"] == DBNull.Value ? 0 : Convert.ToSingle(dr["AmountForPay"]);
                  transactionDetail.VATAmount = dr["VATAmount"] == DBNull.Value ? 0 : Convert.ToSingle(dr["VATAmount"]);
                //transactionDetail.VATPercentage = 0;
                transactionDetail.NetAmount = dr["NetAmount"] == DBNull.Value ? 0 : Convert.ToSingle(dr["NetAmount"]);
                transactionDetail.DiscountPercentage = dr["DiscountPercentage"] == DBNull.Value ? 0 : Convert.ToSingle(dr["DiscountPercentage"]);
                transactionDetail.DiscountAmount = dr["DiscountAmount"] == DBNull.Value ? 0 : Convert.ToSingle(dr["DiscountAmount"]);
                transactionDetail.VoidStatus = Convert.ToBoolean(dr["Voided"]);
                transdetaillist.Add(transactionDetail);

            }
            
            return transdetaillist;
        }

        public static List<TransactionPayment> PaymentLines(string shiftno, string branch, string tillid, string transno)
        {
            List<TransactionPayment> payments = new List<TransactionPayment>();
            string query = "Select * from POS_PaymentEntry where ShiftNo = @ShiftNo and Branch=@Branch and TillID=@TillID and TransactionNo=@TransactionNo";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ShiftNo", shiftno);
            parameter.Add("@Branch", branch);
            parameter.Add("@TillID", tillid);
            parameter.Add("@TransactionNo", transno);
            DataTable dt = DbFunctions.GetDataTable(query, parameter);
            foreach (DataRow dr in dt.Rows)
            {
               payments.Add( ProcessPaymentData(dr));
            }

            return payments;
        }

        public static List<TransactionPayment> PaymentLines(string shiftno, string branch, string tillid)
        {
            List<TransactionPayment> payments = new List<TransactionPayment>();
            string query = "Select * from POS_PaymentEntry where ShiftNo = @ShiftNo and Branch=@Branch and TillID=@TillID";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ShiftNo", shiftno);
            parameter.Add("@Branch", branch);
            parameter.Add("@TillID", tillid);
            
            DataTable dt = DbFunctions.GetDataTable(query, parameter);
            foreach (DataRow dr in dt.Rows)
            {
                payments.Add(ProcessPaymentData(dr));
            }

            return payments;
        }


        public static List<TransactionHeaderTEMP> GetOpenTransactions()
        {
            List<TransactionHeaderTEMP> transactions = new List<TransactionHeaderTEMP>();

            string query = "Select * from POS_Transaction_Header where TransactionStatus = 'Open'";
            DataTable dt = DbFunctions.GetDataTable(query);
            foreach (DataRow dr in dt.Rows)
            {
                TransactionHeaderTEMP transhead = ProcessDataTable(dr);
                transhead.TransactionDetails = POS_Repositery.TransactionDetailsTemp(transhead.ShiftNo, transhead.Branch, transhead.TillID, transhead.TransactionNo);
                transhead.TransactionPayments = POS_Repositery.PaymentLines(transhead.ShiftNo, transhead.Branch, transhead.TillID, transhead.TransactionNo);
                transactions.Add(transhead);
            }
            return transactions;
        }

       private static TransactionPayment ProcessPaymentData(DataRow dr)
        {
            TransactionPayment payment = new TransactionPayment();
            payment.ShiftNo = dr["ShiftNo"].ToString();
            payment.Branch = dr["Branch"].ToString();
            payment.TillID = dr["TillID"].ToString();
            payment.TransactionNo = dr["TransactionNo"].ToString();
            payment.CashierID = dr["CashierID"].ToString();
            payment.LineNo =  Convert.ToInt16(dr["LineNo"]);
            payment.PaymentType = dr["PaymentType"].ToString();
            payment.Currency = dr["Currency"].ToString();
            payment.AmountinCurrency = Convert.ToSingle(dr["AmountInCurrency"]);
            payment.CurrencyRate = Convert.ToSingle(dr["CurrencyRate"]);
            payment.Amount = Convert.ToSingle(dr["Amount"]);

            return payment;
        }



        private static TransactionHeaderTEMP ProcessDataTable(DataRow dr)
        {
            TransactionHeaderTEMP transhead = new TransactionHeaderTEMP();
            
            transhead.ShiftNo = dr["ShiftNo"].ToString();
            transhead.Branch = dr["Branch"].ToString();
            transhead.SupervisorID = dr["SupervisorID"].ToString();
            transhead.CashierID = dr["CashierID"].ToString();
            transhead.TillID = dr["TillID"].ToString();
            transhead.TransactionNo = dr["TransactionNo"].ToString();
            object tdate = dr["TransactionDate"];
            transhead.TransactionDate = Convert.ToDateTime(tdate);
            object ttime = dr["TransactionTime"];
            transhead.TransactionTime = DateTime.Now;
            transhead.TransactionType = dr["TransactionType"].ToString();
            transhead.TransactionStatus = dr["TransactionStatus"].ToString();
            
            transhead.AmountForPay = dr["AmountForPay"] == DBNull.Value? 0:  Convert.ToSingle(dr["AmountForPay"]);
            transhead.VATAmount = dr["VATAmount"] == DBNull.Value? 0: Convert.ToSingle(dr["VATAmount"]);
            transhead.NetAmount = dr["NetAmount"] == DBNull.Value? 0:  Convert.ToSingle(dr["NetAmount"]);
            transhead.TotalQty = dr["TotalQty"] == DBNull.Value ? 0 : Convert.ToSingle(dr["TotalQty"]);
            


            transhead.CustomerMobileNo = dr["CustomerMobileNo"].ToString();
            transhead.CustomerName = dr["CustomerName"].ToString();
            transhead.CustomerAddress1 = dr["CustomerAddress1"].ToString();
            transhead.CustomerAddress2 = dr["CustomerAddress2"].ToString();
            transhead.IsCustomerAccount = dr["IsCustomerAccount"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsCustomerAccount"]);
            transhead.CustomerAccount = dr["CustomerAccount"].ToString();
            
            transhead.Updated = true;
            return transhead;

        }


        public static List<POS_DeliveryBoy>  GetDeliveryBoys()
        {
            string query = "Select * from POS_DeliveryBoys where [status] = 1";
            List<POS_DeliveryBoy> deliveryboys = new List<POS_DeliveryBoy>();
            DataTable dt = DbFunctions.GetDataTable(query);
            foreach(DataRow dr in dt.Rows)
            {
                POS_DeliveryBoy boy = new POS_DeliveryBoy();
                boy.DeliveryBoyID = dr["DeliveryBoyID"].ToString();
                boy.DeliveryBoyName = dr["DeliveryBoyName"].ToString();
                boy.EmployeeNo = dr["EmployeeNo"].ToString();
                boy.ContactNo = dr["ContactNo"].ToString();
                boy.Status = dr["Status"] == DBNull.Value ? false : Convert.ToBoolean(dr["Status"]);
                deliveryboys.Add(boy);
            }
            return deliveryboys;

        }
        
        public static List<String> POSMenuCategories()
        {
            List<string> itemcategories = new List<string>();
            string query = "Select distinct ItemCategory from POS_ItemMenu";
            DataTable dt = DbFunctions.GetDataTable(query);
            foreach(DataRow dr in dt.Rows)
            {
                itemcategories.Add(dr["ItemCategory"].ToString());
            }

            return itemcategories;
        }

        public static List<POS_ItemMenu> GetItemMenus()
        {
            List<POS_ItemMenu> menus = new List<POS_ItemMenu>();
            string query = "Select * from POS_ItemMenu";
            DataTable dt = DbFunctions.GetDataTable(query);
            foreach(DataRow dr in dt.Rows)
            {
                POS_ItemMenu menu = new POS_ItemMenu();
                menu.ItemCategory = dr["ItemCategory"].ToString();
                menu.MenuDescription = dr["MenuDescription"].ToString();
                menu.ItemNo = dr["ItemNo"].ToString();
                menu.UOM = dr["UOM"].ToString();
                menu.Barcode = dr["Barcode"].ToString();
                menu.MenuSortOrder = Convert.ToInt16(dr["MenuSortOrder"]);
                menu.MenuID = Convert.ToInt16(dr["MenuID"]);
                menu.MenuState = "Update";
                menus.Add(menu);
            }
            return menus;
        }

        public static List<TransactionPayment> PosPaymentEntries(string shiftno, string branch)
        {
            List<TransactionPayment> payments = new List<TransactionPayment>();
            string query = "Select p.ShiftNo, p.Branch, PaymentType,  sum(Amount) Amount  from POS_PaymentEntry p ";
            query += "inner join POS_Transaction_Header h on p.TransactionNo = h.TransactionNo";
            query += " where ShiftNo = @ShiftNo and Branch=@Branch";
            query += " group by p.ShiftNo, p.Branch, PaymentType";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ShiftNo", shiftno);
            parameter.Add("@Branch", branch);
            DataTable dt = DbFunctions.GetDataTable(query, parameter);
            foreach (DataRow dr in dt.Rows)
            {
                payments.Add(ProcessPaymentData(dr));
            }

            return payments;


        }

        public static List<POS_GeneratedMenu> GenerateItemMenus()
        {
            string query = "Select i.CODE, i.DESC_ENG +' '+ i.CATEGORY + '' + iu.UNIT_CODE Descr, i.DESC_ENG, i.CATEGORY, iu.UNIT_CODE, iu.PACK_SIZE, iu.BARCODE, ";
            query += " case when pm.ItemNo is null then 0 else 1 end Available ";
            query += " from INV_ITEM_DIRECTORY i inner ";
            query += " join INV_ITEM_DIRECTORY_UNITS iu on i.CODE = iu.ITEM_CODE ";
            query += " left join POS_ItemMenu pm on i.code = pm.ItemNo and iu.UNIT_CODE = pm.UOM";
            DataTable dt = DbFunctions.GetDataTable(query);
            List<POS_GeneratedMenu> generatedMenus = new List<POS_GeneratedMenu>();
            foreach(DataRow dr in dt.Rows)
            {
                POS_GeneratedMenu posmenu = new POS_GeneratedMenu();
                posmenu.CODE = dr["CODE"].ToString();
                posmenu.DESC_ENG = dr["DESC_ENG"].ToString();
                posmenu.CATEGORY = dr["CATEGORY"].ToString();
                posmenu.UNIT_CODE = dr["UNIT_CODE"].ToString();
                posmenu.PACK_SIZE = Convert.ToSingle(dr["PACK_SIZE"]);
                posmenu.BARCODE = dr["BARCODE"].ToString();
                posmenu.Available = Convert.ToBoolean(dr["Available"]);
                generatedMenus.Add(posmenu);


            }
            return generatedMenus;


        }



        public static int UpdateItemMenus(List<POS_ItemMenu> menus)
        {
            string Insertquery = "Insert Into POS_ItemMenu ([ItemCategory],[MenuDescription],[ItemNo],[UOM],[Barcode],[MenuSortOrder]) ";
            Insertquery += " values (@ItemCategory, @MenuDescription, @ItemNo, @UOM, @Barcode, @MenuSortOrder)";
            string Updatequery = "Update POS_ItemMenu set ItemCategory = @ItemCategory, MenuDescription = @MenuDescription, ItemNo=@ItemNo, UOM=@UOM, Barcode=@Barcode, MenuSortOrder=@MenuSortOrder ";
            Updatequery += " where MenuID=@MenuID";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            int reccount = 0;
            foreach (POS_ItemMenu men in menus.Where(m => m.MenuState == "Add"))
            {
                parameters.Clear();
                parameters.Add("@ItemCategory", men.ItemCategory);
                parameters.Add("@MenuDescription", men.MenuDescription);
                parameters.Add("@ItemNo", men.ItemNo);
                parameters.Add("@UOM", men.UOM);
                parameters.Add("@Barcode", men.Barcode);
                parameters.Add("@MenuSortOrder", men.MenuSortOrder);
                reccount += DbFunctions.InsertUpdate(Insertquery, parameters);
            }
            foreach(POS_ItemMenu men in menus.Where(m => m.MenuState == "Update"))
            {
                parameters.Clear();
                parameters.Add("@ItemCategory", men.ItemCategory);
                parameters.Add("@MenuDescription", men.MenuDescription);
                parameters.Add("@ItemNo", men.ItemNo);
                parameters.Add("@UOM", men.UOM);
                parameters.Add("@Barcode", men.Barcode);
                parameters.Add("@MenuSortOrder", men.MenuSortOrder);
                parameters.Add("@MenuID", men.MenuID);
                reccount += DbFunctions.InsertUpdate(Updatequery, parameters);
            }
            return reccount;

        }

        public static TransactionHeaderTEMP GetATransaction(string branch, string transno)
        {
            TransactionHeaderTEMP transhead = new TransactionHeaderTEMP();
            string query = "Select * from POS_Transaction_Header where Branch=@Branch and TransactionNo=@TransactionNo";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            
            parameter.Add("@Branch", branch);
           
            parameter.Add("@TransactionNo", transno);
            DataTable dt = DbFunctions.GetDataTable(query, parameter);
            foreach (DataRow dr in dt.Rows)
            {
                transhead = ProcessDataTable(dr);


            }
            transhead.TransactionDetails = POS_Repositery.TransactionDetailsTemp(transhead.ShiftNo, transhead.Branch, transhead.TillID, transhead.TransactionNo);
            transhead.TransactionPayments = POS_Repositery.PaymentLines(transhead.ShiftNo, transhead.Branch, transhead.TillID, transhead.TransactionNo);
            return transhead;

        }

        public static List<clsCustomer> GetAllCustomers()
        {
            string query = "Select * from REC_CUSTOMER";
            List<clsCustomer> customerlist = new List<clsCustomer>();
            
            DataTable dt = DbFunctions.GetDataTable(query);
            foreach(DataRow dr in dt.Rows)
            {
                clsCustomer cust = new clsCustomer();
                cust.Code = dr["Code"].ToString();
                cust.DescEng = dr["DESC_ENG"].ToString();
                cust.AddressA = dr["ADDRESS_A"].ToString();
                cust.AddressB = dr["ADDRESS_B"].ToString();
                
                cust.LedgerId = dr["LedgerId"].ToString();
                cust.Mobile = dr["MOBILE"].ToString();
                customerlist.Add(cust);

            }

            return customerlist;
        }





    }
}
