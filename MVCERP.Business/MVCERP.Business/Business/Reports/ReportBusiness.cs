using MVCERP.Repository.Repository.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.Reports
{
    public class ReportBusiness : IReportBusiness
    {
        IReportRepository repo;
        public ReportBusiness(ReportRepository _repo) {
            repo = _repo;
        }
        public Shared.Common.ReportResultCommon GetSampleReport()
        {
            return repo.GetSampleReport();
        }
    }
}
