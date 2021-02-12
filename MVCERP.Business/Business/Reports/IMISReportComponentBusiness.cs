using MVCERP.Shared.Common;
using MVCERP.Shared.Common.ReportComponent;


namespace MVCERP.Business.Business.Reports
{
    public interface IMISReportComponentBusiness
    {
        TaskReportingCommon GetMISReport(TaskReportingCommon reportComponent, string User);
    }
}
