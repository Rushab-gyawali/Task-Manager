
using MVCERP.Shared.Common.ReportComponent;

namespace MVCERP.Repository.Repository.Report
{
    public class MISGenerateReportRepo : IMISGenerateReportRepo
    {
        RepositoryDao dao;
        public MISGenerateReportRepo()
        {
            dao = new RepositoryDao();
        }

        public ReportComponent GetMISReport(ReportComponent reportComponent, string User)
        {
            var reportName = reportComponent.ReportName;
            var sql = "";
            switch (reportName)
            {    

                case "MISUserRegisterReport":
                    if (reportComponent.ReportType == "summary")
                    {
                        sql = "EXEC proc_MISUserReport @flag='MISAgentRegisterSummary' ";
                    }
                    else
                    {
                        sql = "EXEC proc_MISUserReport @flag='MISAgentRegister' ";
                    }
                    sql += ",@User = " + dao.FilterString(User);
                    sql += ",@fromDate= " + dao.FilterStringDR(reportComponent.FromDate);
                    sql += ",@toDate = " + dao.FilterStringDR(reportComponent.ToDate);
                    break;


                default:
                    break;
            }

            var res = dao.ExecuteDataset(sql);
            if (res.Tables.Count == 1)
            {
                reportComponent.ReportData = res.Tables[0];
                reportComponent.ReportHeader = res.Tables[1];

            }
            else if (res.Tables.Count > 1)
            {
                reportComponent.ReportData = res.Tables[0];
                reportComponent.ReportHeader = res.Tables[1];
            }

            else
            {
                reportComponent.ReportData = res.Tables[0];
            }

            return reportComponent;

        }

      


    }
}
