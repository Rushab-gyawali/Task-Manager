﻿using MVCERP.Business.Business.Common;
using MVCERP.Business.Business.TaskReporting;
using MVCERP.Shared.Common;
using MVCERP.Web.App_Start;
using MVCERP.Web.Filters;
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
            StaticData.CheckSession();
            _business = business;
            ddl = _ddl;
        }
        [CheckSessionOut]
        public ActionResult Index()
        {
            StaticData.CheckSession();
            return View();
        }
        public ActionResult UserDetail()
        {
            StaticData.CheckSession();

            var user = StaticData.GetUser();
            ViewBag.user = user;
            if(ViewBag.user == StaticData.GetUser())
            {
                string Status = "Completed";
                string AssignTo = user;
                string StatusList = "";
                var model = new TaskReportingModel();
                var common = new TaskReportingCommon();
                common.Status = Status;
                common.AssignTo = AssignTo;
                common.StatusListCount = StatusList;
                var data = _business.StatusCount(common);
                model.StatusCount = data[0].StatusCount;
                ViewBag.statuscomplete = model.StatusCount;
            }
            if (ViewBag.user == StaticData.GetUser())
            {
                string Status = "Assigned InProgress";
                string AssignTo = user;
                string StatusList = "";
                var model = new TaskReportingModel();
                var common = new TaskReportingCommon();
                common.Status = Status;
                common.AssignTo = AssignTo;
                common.StatusListCount = StatusList;
                var data = _business.StatusCount(common);
                model.StatusCount = data[0].StatusCount;
                ViewBag.statusAssigned = model.StatusCount;
            }
            if (ViewBag.user == StaticData.GetUser())
            {
                string Status = "Testing";
                string AssignTo = user;
                string StatusList = "";
                var model = new TaskReportingModel();
                var common = new TaskReportingCommon();
                common.Status = Status;
                common.AssignTo = AssignTo;
                common.StatusListCount = StatusList;
                var data = _business.StatusCount(common);
                model.StatusCount = data[0].StatusCount;
                ViewBag.statusTesting = model.StatusCount;
            }
            if (ViewBag.user == StaticData.GetUser())
            {
                string Status = " ";
                string AssignTo = user;
                string StatusList = "StatusList";
                var model = new TaskReportingModel();
                var common = new TaskReportingCommon();
                common.Status = Status;
                common.AssignTo = AssignTo;
                common.StatusListCount = StatusList;
                var data = _business.StatusCount(common);
                model.StatusCount = data[0].StatusCount;
                ViewBag.statuscount = model.StatusCount;
            }

            return View();
        }

        public JsonResult GetAllTask()
        {
            StaticData.CheckSession();
            var data = _business.GetAllTask();
            for(int i=0; i<data.Count; i++) {
                data[i].Action = StaticData.GetActions("TaskManager", data[i].RowId, data[i].TaskId,"Task");
            }
            return Json( data,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTaskStatus()
        {
            StaticData.CheckSession();
            var data = _business.GetAllTask();
            return View(data);
        }

        public ActionResult MeroTaskMainLayout()
        {
            StaticData.CheckSession();
            return View();
        }

        [HttpPost]
        public JsonResult ChangeTask(string id, string task)
        {
            StaticData.CheckSession();
            var returnResult= _business.ChangeTask(id, task);
            return Json(returnResult);
        }

        [HttpGet]
        public JsonResult StatusList(string status)
        {
            StaticData.CheckSession();
            var user = StaticData.GetUser();
            var data = _business.StatusList(status, user);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

    }

}
