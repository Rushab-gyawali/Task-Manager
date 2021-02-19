using MVCERP.Business.Business.SprintBusiness;
using MVCERP.Shared.Common;
using MVCERP.Web.Library;
using MVCERP.Web.Models;
using MVCERP.Web.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVCERP.Web.Controllers
{
    public class SprintController : Controller
    {
        //
        // GET: /Sprint/
         ISprintBusiness _business;
        public SprintController(ISprintBusiness business)
        {
            StaticData.CheckSession();
            _business = business;
        }
        public ActionResult Index()
        {
            StaticData.CheckSession();
            return View();
        }

        public JsonResult SprintGrid()
        {
            StaticData.CheckSession();
            var data = _business.GetSprints();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].Action = StaticData.GetActions("sprint", data[i].RowId, data[i].SprintId, "AddEditSprint");
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult AddEditSprint()
        {
            StaticData.CheckSession();
            var listbacklogs = ListBacklog();
            ViewBag.backlogs = listbacklogs;
            var data = new SprintCommon();
            string SprintId = StaticData.GetIdFromQuery();
            if(SprintId == "")
            {
                return View(data);
            }
            else
            {
                data = _business.GetById(SprintId);
                data.taskobjlist = new JavaScriptSerializer().Serialize(data.TaskObjectList);
                ViewBag.taskobj = data.taskobjlist;
                return View(data);
                
            }
            
        }

        [HttpPost]
        public ActionResult InsertSprint(SprintAndBacklogViewModel vm)
        {
            StaticData.CheckSession();
            var data = JsonConvert.DeserializeObject<SprintAndBacklogViewModel>(Request.Form["response"]);
            SprintCommon common = new SprintCommon
            {
                BacklogList = StaticData.FilterString(data.BacklogList),
                SprintName = StaticData.FilterString(data.SprintName),
                StartedDate = StaticData.FrontToDBDate(data.StartedDate),
                EndDate = StaticData.FrontToDBDate(data.EndDate),
                SprintId = data.SprintId
            };
            _business.SprintAndBacklog(common);
            
            return RedirectToAction("Index", "Sprint");
        }
        public List<BackLogCommon> ListBacklog()
        {
            StaticData.CheckSession();
            return _business.GetBacklogs();
        }
         
    }
}
