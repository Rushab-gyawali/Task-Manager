
using MVCERP.Shared.Common;
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

        public TaskReportingCommon GetMISReport(TaskReportingCommon common, string User)
        {
            var sql = "EXEC PROC_TASKMANAGER @Flag='TaskReport' ";
            sql += ",@AssignTo = " + dao.FilterString(User);
            sql += ",@TaskStartDate= " + dao.FilterStringDR(common.TaskStartDate);
            sql += ",@TaskEndDate = " + dao.FilterStringDR(common.TaskEndDate);
            sql += ",@Status = " + dao.FilterStringDR(common.Status);

            var res = dao.ExecuteDataset(sql);
            if (res.Tables.Count == 1)
            {
                common.ReportData = res.Tables[0];
                //common.ReportHeader = res.Tables[1];

            }
            else if (res.Tables.Count > 1)
            {
                common.ReportData = res.Tables[0];
                //common.ReportHeader = res.Tables[1];
            }

            else
            {
                common.ReportData = res.Tables[0];
            }

            return common;

        }

      


    }
}
