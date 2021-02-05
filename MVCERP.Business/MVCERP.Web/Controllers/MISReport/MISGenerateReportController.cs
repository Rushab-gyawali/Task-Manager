using MVCERP.Business.Business.Common;
using MVCERP.Business.Business.Reports;
using MVCERP.Web.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Controllers.MISReport
{
    public class MISGenerateReportController : Controller
    {
        ICommonBuss ddl;
        private IMISReportComponentBusiness _buss;

        public MISGenerateReportController(IMISReportComponentBusiness buss, ICommonBuss _ddl)
        {

            _buss = buss;
            ddl = _ddl;
        }


        //[EnableCors]
        public ActionResult GenerateReport()
        {
            StaticData.CheckSession();
            Shared.Common.ReportComponent.ReportComponent reportComponent = new Shared.Common.ReportComponent.ReportComponent();
            var reportName = Request.QueryString["reportName"];
            reportComponent.ReportName = reportName;

            switch (reportName)
            {    

                case "MISUserRegisterReport":
                    reportComponent.ReportType = Request.QueryString["ReportType"];
                    if (reportComponent.ReportType == "summary")
                    {
                        reportComponent.ReportTitle = "User Register Summary Report";
                    }
                    else
                    {
                        reportComponent.ReportTitle = "User Register Report";
                    }                    
                    reportComponent.FromDate = StaticData.FrontToDBDate(Request.QueryString["FromDate"]);                   
                    reportComponent.ToDate = StaticData.FrontToDBDate(Request.QueryString["ToDate"]);   
                    break;    

                default:
                    break;
            }


            reportComponent = _buss.GetMISReport(reportComponent, StaticData.GetUser());
            var unManipulatedData = StaticData.Clone(reportComponent);        
            StaticData.GetClosedXmlExcelSheet(reportComponent);
            unManipulatedData.ExcelLink = reportComponent.ExcelLink;
            unManipulatedData.ShowHeader = true;
            return View(unManipulatedData);
        }

    }
}
