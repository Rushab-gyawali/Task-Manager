using MVCERP.Business.Business.Common;
using MVCERP.Business.Business.TaskReporting;
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
    public class TaskReportingController : Controller
    {
        //
        // GET: /TaskReporting/

       ITaskReportingBusiness _business;
        ICommonBuss ddl;

        public TaskReportingController(ITaskReportingBusiness business, ICommonBuss _ddl)
        {
            _business = business;
            ddl = _ddl;
        }
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserDetail()
        {
            var user = StaticData.GetUser();
            ViewBag.user = user;
            //var data = _business.GetUserDetails(user);
            //var common = new UserModel
            //{
            //    UserId = data["UserId"].ToString(),

            //};
            if(ViewBag.user == StaticData.GetUser())
            {
                string Status = "Completed";
                string CreatedBy = user;
                string StatusList = "";
                var model = new TaskReportingModel();
                var common = new TaskReportingCommon();
                common.Status = Status;
                common.CreatedBy = CreatedBy;
                common.StatusListCount = StatusList;
                var data = _business.StatusCount(common);
                model.StatusCount = data[0].StatusCount;
                ViewBag.statuscomplete = model.StatusCount;
            }
            if (ViewBag.user == StaticData.GetUser())
            {
                string Status = "Assigned InProgress";
                string CreatedBy = user;
                string StatusList = "";
                var model = new TaskReportingModel();
                var common = new TaskReportingCommon();
                common.Status = Status;
                common.CreatedBy = CreatedBy;
                common.StatusListCount = StatusList;
                var data = _business.StatusCount(common);
                model.StatusCount = data[0].StatusCount;
                ViewBag.statusAssigned = model.StatusCount;
            }
            if (ViewBag.user == StaticData.GetUser())
            {
                string Status = "Testing";
                string CreatedBy = user;
                string StatusList = "";
                var model = new TaskReportingModel();
                var common = new TaskReportingCommon();
                common.Status = Status;
                common.CreatedBy = CreatedBy;
                common.StatusListCount = StatusList;
                var data = _business.StatusCount(common);
                model.StatusCount = data[0].StatusCount;
                ViewBag.statusTesting = model.StatusCount;
            }
            if (ViewBag.user == StaticData.GetUser())
            {
                string Status = " ";
                string CreatedBy = user;
                string StatusList = "StatusList";
                var model = new TaskReportingModel();
                var common = new TaskReportingCommon();
                common.Status = Status;
                common.CreatedBy = CreatedBy;
                common.StatusListCount = StatusList;
                var data = _business.StatusCount(common);
                model.StatusCount = data[0].StatusCount;
                ViewBag.statuscount = model.StatusCount;
            }

            ViewData["Status"] = StaticData.SetDDLValue(ddl.SetDropdown("StatusList", StaticData.GetUser()), "", "Select Status");
            return View();
        }

        public JsonResult GetAllTask()
        { 
           var data = _business.GetAllTask();
            for(int i=0; i<data.Count; i++) {
                data[i].Action = StaticData.GetActions("TaskManager", data[i].RowId, data[i].TaskId,"Task");
            }
            return Json(new { data = data },JsonRequestBehavior.AllowGet);
        }
        public JsonResult StatusList(string status)
        {
            var user = StaticData.GetUser();
            var data = _business.StatusList(status, user);
            for (int i = 0; i < data.Count; i++)
            {
                data[i].Action = StaticData.GetActions("TaskManager", data[i].RowId, data[i].TaskId, "Task");
            }
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTaskStatus()
        {
            var data = _business.GetAllTask();
            return View(data);
        }

        public ActionResult MeroTaskMainLayout()
        {
            return View();
        }
    }

}
