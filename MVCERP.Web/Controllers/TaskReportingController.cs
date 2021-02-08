using MVCERP.Business.Business.Common;
using MVCERP.Business.Business.TaskReporting;
using MVCERP.Shared.Common;
using MVCERP.Web.Library;
using MVCERP.Web.Models;
using System;
using System.Collections;
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
            ViewData["Status"] = StaticData.SetDDLValue(ddl.SetDropdown("StatusList", StaticData.GetUser()), "", "Select Status");
            return View();
        }


        public JsonResult GetAllTask()
<<<<<<< HEAD
        { 
           var data = _business.GetAllTask();
            for(int i=0; i<data.Count; i++) {
                data[i].Action = StaticData.GetActions("TaskManger", data[i].RowId, data[i].TaskId,"Task");
=======
        {
            var data = _business.GetAllTask();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].Action = StaticData.GetActions("TaskManager", data[i].RowId, data[i].TaskId,"Task");
>>>>>>> 011b1b73ea1fc4b9ebbbe8c6c13a7a035989b58a
            }
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTaskStatus()
        {
            var data = _business.GetAllTask();
            return View(data);
        }
    }
}