﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Class.Printer.Info
{
    public class FieldInfo
    {

        private string _fieldName;
        private int _formId, _fieldId;
        public int FormId
        {
            set
            {
                _formId = value;
            }
            get
            {
                return _formId;
            }
        }
        public int FieldId
        {
            set
            {
                _fieldId = value;
            }
            get
            {
                return _fieldId;
            }
        }
        public string FieldName
        {
            set
            {
                _fieldName = value;
            }
            get
            {
                return _fieldName;
            }
        }
    }
}
