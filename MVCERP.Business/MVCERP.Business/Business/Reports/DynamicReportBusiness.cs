using MVCERP.Repository.Repository.Report;
using MVCERP.Shared;
using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.Reports
{
    public class DynamicReportBusiness:IDynamicReportBusiness
    {
        IDynamicReportRepo repo;
        public DynamicReportBusiness(DynamicReportRepo _repo)
        {
            repo = _repo;
        }
        public List<DynamicReportData> GetColumns(string User)
        {
            return repo.GetColumns(User);
        }

        public DataTable GetReport(string User, DynamicReportCommon model)
        {
            return repo.GetReport(User,model);
        }
        
        public DbResponse UpdateAgentCommission(string commFromDate, string commToDate, string PaymentAc, string User)
        {
            return repo.UpdateAgentCommission(commFromDate, commToDate, PaymentAc, User);
        }
        
    }
}
