using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class POS_GeneratedMenu
    {
        private string  _CODE, _Descr, _DESC_ENG, _CATEGORY, _UNIT_CODE, _BARCODE;
        private int _PACK_SIZE, _Available;

        public string CODE
        {
            get; set;
        }
        public string Descr
        {
            get; set;
        }
        public string  DESC_ENG
        {
            get; set;
        }
        public string CATEGORY
        {
            get; set;
        }
        public string UNIT_CODE
        {
            get; set;
        }
        public string BARCODE
        {
            get; set;
        }
        public Single PACK_SIZE
        {
            get; set;
        }
        public bool Available
        {
            get; set;
        }

        public string UpdateStatus
        {
            get; set;
        }


        public POS_GeneratedMenu()
        {

        }





    }

}
