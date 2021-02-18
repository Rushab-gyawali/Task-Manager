using MVCERP.Business.Business.Common;
using MVCERP.Business.Business.TaskManager;
using MVCERP.Shared.Common;
using MVCERP.Web.Library;
using MVCERP.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace MVCERP.Web.Controllers
{
    public class TaskManagerController : Controller
    {
        //
        // GET: /TaskManager/
        ITaskManagerBusiness buss;
        ICommonBuss ddl;

        public TaskManagerController(ITaskManagerBusiness _buss, ICommonBuss _ddl)
        {
            buss = _buss;
            ddl = _ddl;
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
                var user = StaticData.GetUser();
                ViewBag.user = user;
                ViewData["AssignTo"] = StaticData.SetDDLValue(ddl.SetDropdownUser("UserDropDown", StaticData.GetUser()), "", "Select User");
                ViewData["Status"] = StaticData.SetDDLValue(ddl.SetDropdown("StatusList", StaticData.GetUser()), "", "Select Status");
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


                ViewData["AssignTo"] = StaticData.SetDDLValue(ddl.SetDropdownUser("UserDropDown", StaticData.GetUser()), "", "Select User");
                ViewData["Status"] = StaticData.SetDDLValue(ddl.SetDropdown("StatusList", StaticData.GetUser()), "", "Select Status");
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Task(TaskReportingModel task)
        {
                if (ModelState.IsValid)
            {
                var user = StaticData.GetUser();
                var common = new TaskReportingCommon();
                    common.TaskName = task.TaskName;
                    common.TaskStartDate = task.TaskStartDate.ToString();
                    common.TaskEndDate = task.TaskEndDate.ToString();
                    common.TaskDescription = task.TaskDescription;
                    common.Status = task.Status.ToString();
                    common.CreatedBy = user;
                    common.AssignTo = task.AssignTo;
                    common.TaskId = task.TaskId;
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
        public ActionResult DeleteTask()
        {
            string Taskid = Request.QueryString["id"];
            var TaskID = StaticData.Base64Decode_URL(Taskid);
            if (ModelState.IsValid)
            {
                TaskReportingCommon common = new TaskReportingCommon();
                common.TaskId = TaskID;
                var response = buss.DeleteTask(common);
                StaticData.SetMessageInSession(response);
                if (response.ErrorCode == 1)
                {
                    ModelState.AddModelError("", response.Message);
                    ViewData["msg"] = "Delete Failed";
                    return RedirectToAction("Index", "TaskReporting");

                }
                ViewData["msg"] = "Delete Successful";
                return RedirectToAction("Index", "TaskReporting");
            }
            else
            {
                var errors = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));

                ModelState.AddModelError("", errors);
            }
            return RedirectToAction("Index", "TaskReporting");
        }

    }
}
