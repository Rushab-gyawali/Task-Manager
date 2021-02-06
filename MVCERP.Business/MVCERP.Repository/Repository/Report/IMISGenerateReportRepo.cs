using MVCERP.Shared.Common.ReportComponent;


namespace MVCERP.Repository.Repository.Report
{
    public interface IMISGenerateReportRepo
    {
        ReportComponent GetMISReport(ReportComponent reportComponent, string User);

    }
}
