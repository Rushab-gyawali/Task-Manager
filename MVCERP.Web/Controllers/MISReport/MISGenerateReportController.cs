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

        public ActionResult GenerateReport()
        {
            StaticData.CheckSession();
            Shared.Common.TaskReportingCommon common = new Shared.Common.TaskReportingCommon();
            var reportName = Request.QueryString["reportName"];
            var fromdate = Request.QueryString["FromDate"];
            var todate = Request.QueryString["ToDate"];
            var taskstatus = Request.QueryString["ReportStatus"];
            common.ReportName = StaticData.Base64Decode_URL(reportName);

            switch (common.ReportName)
            {

                case "MISUserRegisterReport":
                    common.Status = Request.QueryString["ReportStatus"];
                    if (common.Status == "summary")
                    {
                        common.ReportTitle = "User Register Summary Report";
                    }
                    else
                    {
                        common.ReportTitle = "User Status Report";
                    }
                    var taskstartdate = StaticData.Base64Decode_URL(fromdate);
                    common.TaskStartDate = taskstartdate.ToString();
                    var taskenddate  = StaticData.Base64Decode_URL(todate);
                    common.TaskEndDate = taskenddate.ToString();
                    common.Status = StaticData.Base64Decode_URL(taskstatus);
                    break;

                default:
                    break;
            }


            common = _buss.GetMISReport(common, StaticData.GetUser());
            //var unManipulatedData = StaticData.Clone(common);
            //StaticData.GetClosedXmlExcelSheet(common);
            //unManipulatedData.ExcelLink = common.ExcelLink;
            //unManipulatedData.ShowHeader = true;
            //return View(unManipulatedData);
            return View(common);
        }

    }
}
