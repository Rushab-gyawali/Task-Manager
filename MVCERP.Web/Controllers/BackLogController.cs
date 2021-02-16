using MVCERP.Business.Business.BackLog;
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
    public class BackLogController : Controller
    {

        IBackLogBusiness bussiness;


        public BackLogController(IBackLogBusiness _buss)
        { 
          bussiness = _buss;
            }

        public ActionResult Index()
        {

            StaticData.CheckSession();
            return View();
        }


        public ActionResult AddBackLogTask()
        {

            StaticData.CheckSession();

            string id = Request.QueryString["id"];
            var ID = StaticData.Base64Decode_URL(id);
            var model = new BackLogTaskModel();
            if (ID == "")
            {
                var user = StaticData.GetUser();
                ViewBag.user = user;
                return View();
            }
            else
            {
                var data = bussiness.GetById(ID);
                model.BackLogId = data[0].BackLogId;
                model.TaskName = data[0].TaskName;
                model.TaskDescription = data[0].TaskDescription;
                model.TaskReportedDate = data[0].TaskReportedDate;
                model.DiscussionDate = data[0].DiscussionDate;
                model.ClientId = data[0].ClientId;
                model.StoryPoint = data[0].StoryPoint;


                return View(model);
            }
        }

        [HttpPost]
        public ActionResult AddBackLogTask(BackLogTaskModel task)
        {

            StaticData.CheckSession();
            var user = StaticData.GetUser();

            if (ModelState.IsValid)
            {
                var common = new BackLogCommon();
                common.BackLogId = Convert.ToInt32(task.BackLogId);
                common.TaskName = task.TaskName;
                common.TaskDescription = task.TaskDescription;
                common.TaskReportedDate = task.TaskReportedDate;
                common.DiscussionDate = task.DiscussionDate;
                common.Owner = user;
                common.ClientId = task.ClientId;
                common.StoryPoint = task.StoryPoint;
                common.CreatedBy = user;
          
                var response = bussiness.AddBackLogTask(common);

                StaticData.SetMessageInSession(response);
                if (response.ErrorCode == 1)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(task);
                }
                return RedirectToAction("Index", "BackLog");
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
        public JsonResult ListBackLogTask()
        {
            StaticData.CheckSession();
            var data = bussiness.ListBackLogTask();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].Action = StaticData.GetActions("BackLog", data[i].BackLogId, data[i].BackLogId.ToString(), "AddBackLogTask");
            }
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }




        public ActionResult DeleteTask()
        {

            StaticData.CheckSession();

            string id = Request.QueryString["id"];
            var Id = StaticData.Base64Decode_URL(id);
            var ID = Convert.ToInt32(Id);
            if (ModelState.IsValid)
            {
                BackLogCommon common = new BackLogCommon();
                common.BackLogId = ID;
                var response = bussiness.DeleteTask(common);
                StaticData.SetMessageInSession(response);
                if (response.ErrorCode == 1)
                {
                    ModelState.AddModelError("", response.Message);
                    ViewData["msg"] = "Delete Failed";
                    return RedirectToAction("Index", "BackLog");

                }
                ViewData["msg"] = "Delete Successful";
                return RedirectToAction("Index", "BackLog");
            }
            else
            {
                var errors = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));

                ModelState.AddModelError("", errors);
            }
            return RedirectToAction("Index", "Member");
        }




    }
}
