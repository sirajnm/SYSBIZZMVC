using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys_Sols_Inventory.Class;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Class
{
    class FinancialYear
    {
        string _FinancialYearCode;
        DateTime _SDate, _EDate, _AllowPostingFrom, _AllowPostingTill;
        bool _CurrentFY = false, _Status = false;

     public int   FinaYearId { get; set; }
    public string FinancialYearCode
        {
            get { return _FinancialYearCode; }
            set { _FinancialYearCode = value; }
        }
    public DateTime SDate
        {
            get { return _SDate; }
            set { _SDate = value; }
        }
    public DateTime EDate {
            get { return _EDate; }
            set { _EDate = value; }
        }
    public bool Status {
            get { return _Status; }
            set { _Status = value; }
        }
    public DateTime AllowPostingFrom {
            get { return _AllowPostingFrom; }
            set { _AllowPostingFrom = value; }
        }
    public DateTime AllowPostingTill {
            get { return _AllowPostingTill; }
            set { _AllowPostingTill = value; }
        }
        public bool CurrentFY {
            get { return _CurrentFY; }
            set {
                _CurrentFY = value;
            } }
        public string NoSeriesSuffix { get; set; }

        public FinancialYear()
        {

        }

        public bool SetDefault()
        {
            string query = "Update tbl_FinancialYear set CurrentFY =  case when FinaYearId = @FinaYearId then   1 else 0 end ";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@FinaYearId", FinaYearId);
            if (DbFunctions.InsertUpdate(query, parameter) >=1 )
                return true;
            return false;
        }

        public bool Insert()
        {
            string query = "Insert Into tbl_FinancialYear (FinancialYearCode, SDate, EDate, [Status], AllowPostingFrom, AllowPostingTill, CurrentFY ) ";
            query += " Values (@FinancialYearCode, @SDate, @EDate, @Status, @AllowPostingFrom, @AllowPostingTill, @CurrentFY )";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@FinancialYearCode", FinancialYearCode);
            parameter.Add("@SDate", SDate);
            parameter.Add("@EDate", EDate);
            parameter.Add("@Status", Status);
            parameter.Add("@AllowPostingFrom",  SDate );
            parameter.Add("@AllowPostingTill",  EDate );
            parameter.Add("@CurrentFY", CurrentFY ? 1:0  );
            if (DbFunctions.InsertUpdate(query, parameter) >= 1)
            {
                if (CurrentFY) SetDefault();
                return true;
            }
            return false;

        }

        public bool Update()
        {
            string query = "Update tbl_FinancialYear set FinancialYearCode=@FinancialYearCode, SDate = @SDate, EDate= @EDate, [Status] = @Status, CurrentFY = @CurrentFY, NoSeriesSuffix = @NoSeriesSuffix   where FinaYearId = @FinaYearId";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@FinancialYearCode", FinancialYearCode);
            parameter.Add("@SDate", SDate);
            parameter.Add("@EDate", EDate);
            parameter.Add("@Status", Status ? 1 :0);
            parameter.Add("@FinaYearId", FinaYearId);
            parameter.Add("@CurrentFY",  CurrentFY ? 1:0);
            parameter.Add("@NoSeriesSuffix", NoSeriesSuffix);
            if (DbFunctions.InsertUpdate(query, parameter) >= 1)
            {
                if (CurrentFY) SetDefault();
                return true;
            }
            return false;

        }


    }
}
