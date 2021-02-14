using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCERP.Repository.Repository.Report;
using MVCERP.Shared.Common;
using MVCERP.Shared.Common.ReportComponent;

namespace MVCERP.Business.Business.Reports
{
    public class MISReportComponentBusiness : IMISReportComponentBusiness
    {
        IMISGenerateReportRepo repo;
        public MISReportComponentBusiness(MISGenerateReportRepo _repo)
        {
            repo = _repo;
        }
        public TaskReportingCommon GetMISReport(TaskReportingCommon reportComponent, string User)
        {
            return repo.GetMISReport(reportComponent, User);
        }

        
    }
}
