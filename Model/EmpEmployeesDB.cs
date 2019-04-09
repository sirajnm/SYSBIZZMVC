using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class EmpEmployeesDB
    {
        private int empId, deleted, jbId;
        private string empFName, empMName, empLName, empAddress, empSex, empDesig;
        private string empEmail, empTelephone, empMobile, empBranch, ledgerId;
        private DateTime empDob;

        public DateTime EmpDob
        {
            get { return empDob; }
            set { empDob = value; }
        }

        public string LedgerId
        {
            get { return ledgerId; }
            set { ledgerId = value; }
        }

        public string EmpBranch
        {
            get { return empBranch; }
            set { empBranch = value; }
        }

        public string EmpMobile
        {
            get { return empMobile; }
            set { empMobile = value; }
        }

        public string EmpTelephone
        {
            get { return empTelephone; }
            set { empTelephone = value; }
        }

        public string EmpEmail
        {
            get { return empEmail; }
            set { empEmail = value; }
        }
        public string EmpDesig
        {
            get { return empDesig; }
            set { empDesig = value; }
        }

        public string EmpSex
        {
            get { return empSex; }
            set { empSex = value; }
        }

        public string EmpAddress
        {
            get { return empAddress; }
            set { empAddress = value; }
        }

        public string EmpLName
        {
            get { return empLName; }
            set { empLName = value; }
        }

        public string EmpMName
        {
            get { return empMName; }
            set { empMName = value; }
        }

        public string EmpFName
        {
            get { return empFName; }
            set { empFName = value; }
        }

        public int JbId
        {
            get { return jbId; }
            set { jbId = value; }
        }

        public int Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        public int EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        
    }
}
