using MVCERP.Shared.Common.ReportComponent;


namespace MVCERP.Business.Business.Reports
{
    public interface IMISReportComponentBusiness
    {
        ReportComponent GetMISReport(ReportComponent reportComponent, string User);
    }
}
