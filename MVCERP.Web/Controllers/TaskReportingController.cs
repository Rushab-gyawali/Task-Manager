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

        public TaskReportingController(ITaskReportingBusiness business)
        {
            _business = business;
        }
        
        public ActionResult Index()
        {
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
