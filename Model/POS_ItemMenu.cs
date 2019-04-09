using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class POS_ItemMenu
    {
        string _ItemCategory, _MenuDescription, _ItemNo, _UOM,  _Barcode, _MenuState = "Add";
        int  _MenuSortOrder;
        Single _Quantity;

      public string ItemCategory
        {
            get
            {
                return _ItemCategory;
            }
            set
            {
                _ItemCategory = value;
            }
        }
public string MenuDescription
        {
            get
            {
                return _MenuDescription;
            }
            set
            {
                _MenuDescription = value;
            }
        }
public string ItemNo
        {
            get
            {
                return _ItemNo;
            }
            set
            {
                _ItemNo = value;
            }
        }
public string UOM
        {
            get
            {
                return _UOM;
            }
            set
            {
                _UOM = value;
            }
        }

        public Single Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }


public string Barcode
        {
            get
            {
                return _Barcode;
            }
            set
            {
                _Barcode = value;
            }
        }
public int MenuSortOrder
        {
            get
            {
                return _MenuSortOrder;
            }
            set
            {
                _MenuSortOrder = value;
            }
        }

        public string MenuState
        {
            get { return _MenuState; }
            set { _MenuState = value; }
        }

        public int MenuID
        {
            get; set;
        }

        public POS_ItemMenu()
        {

        }


    }
}
