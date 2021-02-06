using MVCERP.Business.Business.TaskManager;
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
    public class TaskManagerController : Controller
    {
        //
        // GET: /TaskManager/
        ITaskManagerBusiness buss;

        public TaskManagerController(ITaskManagerBusiness _buss)
        {
            buss = _buss;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Task()
        {
            string id = Request.QueryString["id"];
            var TaskId = StaticData.Base64Decode_URL(id);
            var model = new TaskReportingModel();
            if (TaskId == "")
            {
                return View();
            }
            else
            {
                var data = buss.GetById(TaskId);
               
                model.TaskName = data[0].TaskName;
                model.TaskStartDate = data[0].TaskStartDate;
                model.TaskEndDate = data[0].TaskEndDate;
                model.TaskDescription = data[0].TaskDescription;
                model.Status = data[0].Status;
                model.CreatedBy = data[0].CreatedBy;
                model.AssignTo = data[0].AssignTo;
                model.TaskId = data[0].TaskId;
                model.RowId = data[0].RowId;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Task(TaskReportingModel task)
        {
                if (ModelState.IsValid)
                {
                    var common = new TaskReportingCommon();
                    common.TaskName = task.TaskName;
                    common.TaskStartDate = task.TaskStartDate.ToString();
                    common.TaskEndDate = task.TaskEndDate.ToString();
                    common.TaskDescription = task.TaskDescription;
                    common.Status = task.Status.ToString();
                    common.CreatedBy = task.CreatedBy;
                    common.AssignTo = task.AssignTo;
                    common.TaskId = task.TaskId;
                    common.RowId = Convert.ToInt32(task.RowId);
                var response = buss.TaskManager(common);
                    StaticData.SetMessageInSession(response);
                    if (response.ErrorCode == 1)
                    {
                        ModelState.AddModelError("", response.Message);
                        return View(task);
                    }
                    return RedirectToAction("Index", "TaskReporting");
                }
                else
                {
                    var errors = string.Join("; ", ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));

                    ModelState.AddModelError("", errors);
                }
           
            //db.Entry(userprofile).State = EntityState.Modified;
            //db.SaveChanges();

            return View(task);
        }
        //public JsonResult GetById(string TaskId)
        //{
        //    var id = StaticData.Base64Decode_URL(TaskId);
        //    var data = buss.GetById(id);
        //    return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        //}

    }
}
