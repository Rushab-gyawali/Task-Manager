using MVCERP.Shared.Common;
using MVCERP.Shared.Common.ReportComponent;


namespace MVCERP.Repository.Repository.Report
{
    public interface IMISGenerateReportRepo
    {
        TaskReportingCommon GetMISReport(TaskReportingCommon reportComponent, string User);

    }
}
