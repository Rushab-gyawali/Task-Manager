using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Report
{
    public class ReportRepository : IReportRepository
    {
        RepositoryDao dao;
        public ReportRepository()
        {
            dao = new RepositoryDao();
        }
        public Shared.Common.ReportResultCommon GetSampleReport()
        {
            var sql = "EXEC proc_SampleReport ";
            return dao.ParseReportResult(sql);
        }
    }
}
