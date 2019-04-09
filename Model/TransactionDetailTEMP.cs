using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace Sys_Sols_Inventory.Model
{
    class TransactionDetailTEMP
    {
        private string _ShiftNo, _Branch, _TillID, _TransactionNo, _CashierID, _ItemNo, _UOM, _item_description;
        private int _lineno;
        private Single _UOMQty, _PacketSize,  _Quantity, _Price, _PriceVAT, _VATPercentage, _CESSPercentage, _DiscountPercentage, _DiscountAmount, _AmountForPay, _VATAmount, _CESSAmount, _NetAmount;
        private bool _VoidStatus, _Updated;
       public string ShiftNo
        {
            get { return _ShiftNo; }
            set { _ShiftNo = value; }
        }

        public string Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }

        public string TillID
        {
            get { return _TillID; }
            set { _TillID = value; }
        }
        public string TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        public string CashierID
        {
            get { return _CashierID; }
            set { _CashierID = value; }
        }
        public string ItemNo
        {
            get { return _ItemNo; }
            set { _ItemNo = value; }
        }

        public string Barcode { get; set; }
        
        public string UOM
        {
            get { return _UOM; }
            set { _UOM = value; }
        }
        public int LineNo
        {
            get { return _lineno; }
            set { _lineno = value; }
        }
        public Single UOMQty
        {
            get { return _UOMQty; }
            set { _UOMQty = value;
                if (_PacketSize == 0) GetPacketSize(Barcode);
                Quantity = UOMQty*PacketSize;


            }
        }
        
        public Single PacketSize
        {
            get { return _PacketSize; }
            set { _PacketSize = value; }
        }
        public Single Quantity
        {
            get { return _Quantity; }
            set {
                _Quantity = value;
         
                AmountForPay = (Single) Math.Round( _Quantity * PriceVAT - _Quantity * PriceVAT*_DiscountPercentage, 2) ;
                NetAmount = _Quantity * Price - _Quantity * Price*_DiscountPercentage;
                VATAmount = AmountForPay - NetAmount;
                
                


            }
        }

        private void GetPacketSize(string barcode)
        {
            InvItemDirectoryUnits iuom = new InvItemDirectoryUnits(barcode);
         
         
            UOM = iuom.Unitcode;
            PacketSize = iuom.PackSize;

        }

        public Single Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        public Single PriceVAT
        {
            get { return _PriceVAT; }
            set { _PriceVAT = value; }
        }
        public Single VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        public Single CESSPercentage
        {
            get { return _CESSPercentage; }
            set { _CESSPercentage = value; }
        }
        public Single DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set {
                _DiscountPercentage = value/100;
                DiscountAmount = PriceVAT * _DiscountPercentage * Quantity;
                AmountForPay =  ((PriceVAT * Quantity - PriceVAT * _DiscountPercentage * Quantity));
                Price = PriceVAT / (1+ VATPercentage);
                NetAmount = Price * Quantity - Price * _DiscountPercentage * Quantity;


            }
        }
        public Single DiscountAmount
        {
            get { return _DiscountAmount; }
            set {
                _DiscountAmount = value;


            }
        }
        public Single AmountForPay
        {
            get { return _AmountForPay; }
            set { _AmountForPay = value; }
        }
        public Single VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }
        public Single CESSAmount
        {
            get { return _CESSAmount; }
            set { _CESSAmount = value; }
        }
        public Single NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }

        public string ItemDescription
        {
            get { return _item_description; }
            set { _item_description = value; }
        }

        public bool VoidStatus
        {
            get { return _VoidStatus; }
            set { _VoidStatus = value; }
        }
        
        public bool Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value; }
        }

        public TransactionDetailTEMP()
        {

        }

        public  TransactionDetailTEMP(TransactionHeaderTEMP thead, string barc )
        {

            InvItemDirectoryUnits iuom = new InvItemDirectoryUnits(barc);
            Barcode = iuom.Barcode;
            ItemNo = iuom.ItemCode;
            UOM = iuom.Unitcode;
            PacketSize = iuom.PackSize;

            ItemDirectoryDB item = ItemDirectoryDB.GetanItem(ItemNo);

            ShiftNo = thead.ShiftNo;
            Branch = thead.Branch;
            TillID = thead.TillID;
            TransactionNo = thead.TransactionNo;
            CashierID = thead.CashierID;
            Barcode = barc;
            
            ItemDescription = item.Desc_Eng;
            LineNo = GetNewLine(thead.Branch, thead.TillID, thead.TransactionNo);
            Price = GetPrice(ItemNo, UOM);
            VATPercentage = item.GetTaxRate(ItemNo) / 100;
            PriceVAT = Price + (Single)Math.Round( Price * VATPercentage, 2,MidpointRounding.AwayFromZero);
            

            if (thead.TransactionType == "Sales")
            {
                UOMQty = 1;
            
            }
            if (thead.TransactionType == "Return")
            {
                UOMQty = -1;
            
            }
            VoidStatus = false;
            Updated = InsertTransactionDetaiTemp();



        }

        public void ChangeQuantity(int qty)
        {

            
            UOMQty = qty;
            Updated = false;
           
        }

        public int Void()
        {
            if (VoidStatus)
            {
                VoidStatus = false;
            } else   VoidStatus = true;
            string updatequery = "Update POS_Transaction_Detail set Voided = @Voided where TransactionNo=@TransactionNo and [LineNo]=@LineNo";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@TransactionNo", TransactionNo);
            parameters.Add("@LineNo", LineNo);
            parameters.Add("@Voided", VoidStatus ? 1 : 0);
            return DbFunctions.InsertUpdate(updatequery, parameters);
            
        }

        private DataTable ItemUOM(string barc)
        {
            string query = "Select [Item_code], [Unit_code], [Pack_Size] from [INV_ITEM_DIRECTORY_UNITS] where BARCODE = @barc";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@barc", barc);
            DataTable dt = DbFunctions.GetDataTable(query, parameter);
            return dt;

        }

        



        private int GetNewLine(string brch, string tid, string trnsno)
        {
            string query = "Select Max([LineNo]) MaxLineNo from [POS_Transaction_Detail] where Branch = @brch and TillID = @tid and TransactionNo = @trnsno";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@brch", brch);
            parameters.Add("@tid", tid);
            parameters.Add("@trnsno", trnsno);
            object obj = DbFunctions.GetAValue(query, parameters);
            if (DBNull.Value == obj || obj.ToString() == "")
            {
                return 10;
            }else
            {
                return (int)obj + 10;
            }

        }
        private Single GetPrice(string itemno, string uom)
        {
            string query = "Select Max(Price) Price from INV_ITEM_PRICE_DF where ITEM_CODE = @ITEM_CODE AND UNIT_CODE = @UNIT_CODE AND SAL_TYPE = 'RTL'";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            
            parameters.Add("@ITEM_CODE", itemno);
            parameters.Add("@UNIT_CODE", uom);
            
            object a = DbFunctions.GetAValue(query, parameters);
            if (a != "")
            {
                return Convert.ToSingle(a);
            }
            return 0;
        }

        public bool Update()
        {
            string udpatestring = "Update POS_Transaction_Detail set UOMQty= @UOMQty, Quantity=@Quantity, AmountForPay=@AmountForPay, VATAmount=@VATAmount,  NetAmount=@NetAmount, DiscountPercentage=@DiscountPercentage, DiscountAmount=@DiscountAmount  ";
            udpatestring += " where TransactionNo=@TransactionNo and [LineNo]=@LineNo";
            Dictionary<String, object> parameter = new Dictionary<string, object>();
            parameter.Add("@TransactionNo", TransactionNo);
            parameter.Add("@LineNo", LineNo);
            
            parameter.Add("@UOMQty", UOMQty);
            parameter.Add("@Quantity", Quantity);
            parameter.Add("@AmountForPay", AmountForPay);
            parameter.Add("@VATAmount", VATAmount);
            parameter.Add("@NetAmount", NetAmount);
            parameter.Add("@Voided", VoidStatus ? 1 : 0);
            parameter.Add("@DiscountPercentage", DiscountPercentage);
            parameter.Add("@DiscountAmount", DiscountAmount);
            if (DbFunctions.InsertUpdate(udpatestring, parameter) == 1) return true;
                return false;
        }


        public bool InsertTransactionDetaiTemp()
        {
            string query = "Insert Into POS_Transaction_Detail (ShiftNo, Branch, TillID, TransactionNo, CashierID, [LineNo], ItemNo, Barcode, ItemDescription, UOM, UOMQty, Quantity, PriceVAT, VATPercentage, CESSPercentage, AmountForPay, VATAmount, CESSAmount, NetAmount, Voided) ";
            query += " values ( @ShiftNo, @Branch, @TillID, @TransactionNo, @CashierID, @LineNo, @ItemNo, @barcode, @ItemDescription, @UOM, @UOMQty, @Quantity, @PriceVAT, @VATPercentage, @CESSPercentage, @AmountForPay, @VATAmount, @CESSAmount, @NetAmount, @Voided )";
            Dictionary<String, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ShiftNo", ShiftNo);
            parameter.Add("@Branch", Branch);
            parameter.Add("@TillID", TillID);
            parameter.Add("@TransactionNo", TransactionNo);
            parameter.Add("@CashierID", CashierID);
            parameter.Add("@LineNo", LineNo);
            parameter.Add("@ItemNo", ItemNo);
            parameter.Add("@barcode", Barcode);
            parameter.Add("@ItemDescription", ItemDescription);
            parameter.Add("@UOM", UOM);
            parameter.Add("@UOMQty", UOMQty);
            parameter.Add("@Quantity", Quantity);
            parameter.Add("@PriceVAT", PriceVAT);
            parameter.Add("@VATPercentage", VATPercentage);
            parameter.Add("@CESSPercentage", CESSPercentage);
            parameter.Add("@AmountForPay", AmountForPay);
            parameter.Add("@VATAmount", VATAmount);
            parameter.Add("@CESSAmount", CESSAmount);
            parameter.Add("@NetAmount", NetAmount);
            parameter.Add("@Voided", VoidStatus?1:0);
            if (DbFunctions.InsertUpdate(query, parameter) == 1)
            {
                return true;
            }

            return false;
        }


    }
}
