using MVCERP.Business.Business.TaskReport;
using MVCERP.Business.Business.Common;
using MVCERP.Shared.Common;
using MVCERP.Web.Library;
using MVCERP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Controllers
{
    public class TaskReportController : Controller
    {
        //
        // GET: /TaskReport/
        ITaskReportBusiness buss;
        ICommonBuss ddl;
        public TaskReportController(ITaskReportBusiness _buss, ICommonBuss _ddl)
        {
            StaticData.CheckSession();
            buss = _buss;
            ddl = _ddl;
        }

        public ActionResult Index()
        {
            StaticData.CheckSession();
            return View();
        }

        public ActionResult Assigned()
        {
            StaticData.CheckSession();
            return View();
        }

        public ActionResult Completed()
        {
            StaticData.CheckSession();
            return View();
        }

        public ActionResult Testing()
        {
            StaticData.CheckSession();
            return View();
        }

        [HttpPost]
        public ActionResult Completed(TaskReportingModel model)
        {
            StaticData.CheckSession();
            if (ModelState.IsValid)
            {
                var ReportName = "MISUserRegisterReport";
                var status = "Completed";
                var reportname = StaticData.Base64Encode_URL(ReportName);
                var startdate = StaticData.Base64Encode_URL(model.TaskStartDate);
                var enddate = StaticData.Base64Encode_URL(model.TaskEndDate);
                var reportstatus = StaticData.Base64Encode_URL(status);
                return RedirectToAction("GenerateReport", "MISGenerateReport", new { FromDate = startdate, ToDate = enddate, ReportStatus = reportstatus, reportName = reportname });
            }
            return RedirectToAction("Index", "TaskReport");
        }

        [HttpPost]
        public ActionResult Assigned(TaskReportingModel model)
        {
            StaticData.CheckSession();
            if (ModelState.IsValid)
            {
                var ReportName = "MISUserRegisterReport";
                var status = "Assigned InProgress";
                var reportname = StaticData.Base64Encode_URL(ReportName);
                var startdate = StaticData.Base64Encode_URL(model.TaskStartDate);
                var enddate = StaticData.Base64Encode_URL(model.TaskEndDate);
                var reportstatus = StaticData.Base64Encode_URL(status);
                return RedirectToAction("GenerateReport", "MISGenerateReport", new { FromDate = startdate, ToDate = enddate, ReportStatus = reportstatus, reportName = reportname });
            }
            return RedirectToAction("Index", "TaskReport");
        }

        [HttpPost]
        public ActionResult Testing(TaskReportingModel model)
        {
            StaticData.CheckSession();
            if (ModelState.IsValid)
            {
                var ReportName = "MISUserRegisterReport";
                var status = "Testing";
                var reportname = StaticData.Base64Encode_URL(ReportName);
                var startdate = StaticData.Base64Encode_URL(model.TaskStartDate);
                var enddate = StaticData.Base64Encode_URL(model.TaskEndDate);
                var reportstatus = StaticData.Base64Encode_URL(status);
                return RedirectToAction("GenerateReport", "MISGenerateReport", new { FromDate = startdate, ToDate = enddate, ReportStatus = reportstatus, reportName = reportname });
            }
            return RedirectToAction("Index", "TaskReport");
        }

    }

}
