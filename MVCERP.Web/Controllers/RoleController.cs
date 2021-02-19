using System;
using System.Web.Mvc;
using MVCERP.Web.Library;
using MVCERP.Web.Models;
using MVCERP.Web.Filters;
using MVCERP.Shared.Common;
using System.Collections.Generic;

namespace MVCERP.Web.Controllers
{
    //[SessionExpiryFilter]
    public class RoleController : Controller
    {

        MVCERP.Business.Business.Role.IRoleBusiness buss;
        private const string ControllerName = "Role";
        private const string ViewId = "108400";
        private const string AddEditId = "108410";
        private const string RoleId = "108420";
        public RoleController(MVCERP.Business.Business.Role.IRoleBusiness _buss)
        {
            StaticData.CheckSession();
            buss = _buss;
        }
        //
        // GET: /Role/

        //public ActionResult Index(string Search="", int Pagesize=10)
        //{
        //    StaticData.CheckSession();
        //    var list = buss.GetList(StaticData.GetUser(), Search, Pagesize);
        //    foreach (var item in list)
        //    {
        //        item.Action = StaticData.GetActions("Role", item.Id, RoleId, AddEditId);
        //    }

        //    IDictionary<string, string> param = new Dictionary<string, string>();
        //    param.Add("RoleName", "Role Name");
        //    param.Add("IsActive", "Is Active");
        //    param.Add("User", "Created By");
        //    param.Add("CreatedDate", "Created Date");
        //    param.Add("Action", "Action");

        //    ProjectGrid.column = param;
        //  var grid = ProjectGrid.MakeGrid(list, "Role", Search, Pagesize, true);
        //  ViewData["grid"] = grid;
        //    return View();
        //}

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListUser()
        {
            StaticData.CheckSession();
            var data = buss.GetList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].Action = StaticData.GetActions("Role", data[i].Id, data[i].Id.ToString(), "Manage");
            }
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Role/Details/5

        public ActionResult Details(int id = 0)
        {
            StaticData.CheckSession();
            RoleModel rolemodel = null;// db.RoleModels.Find(id);
            if (rolemodel == null)
            {
                return HttpNotFound();
            }
            return View(rolemodel);
        }

        public ActionResult Role()
        {
            StaticData.CheckSession();
            string id = StaticData.GetIdFromQuery();
            var data = buss.GetAssignedList(StaticData.GetUser(), id);
            ViewData["Menu"] = Helper.GetMenuList(data);
            ViewData["id"] = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Role(string returnUrl)
        {
            StaticData.CheckSession();
            var id = Request.Form["RoleId"];
            var functionRole = Request.Form["functionRole"];
            functionRole = StaticData.MakeXmlByComa(functionRole);
            var res = buss.AssignRole(StaticData.GetUser(), id, functionRole);
            StaticData.SetMessageInSession(res);
            if (res.ErrorCode == 1)
            {
                var data = buss.GetAssignedList(StaticData.GetUser(), Convert.ToInt16(id));
                ViewData["Menu"] = Helper.GetMenuList(data);
                ViewData["id"] = id;
                return View();
            }
            return RedirectToAction("Index");
        }
        //
        // GET: /Role/Create

        public ActionResult Manage()
        {
            StaticData.CheckSession();
            RoleModel model = new RoleModel();
            var data = buss.GetListById(StaticData.GetUser(), StaticData.GetIdFromQuery());
            if (data.Count > 0)
            {
                model.RoleName = data[0].RoleName;
                model.Id = data[0].Id;
                model.IsActive = data[0].IsActive;
            }

            return View(model);
        }

        //
        // POST: /Role/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(RoleModel model)
        {
            StaticData.CheckSession();
            if (ModelState.IsValid)
            {
                MVCERP.Shared.Common.RoleCommon common = new MVCERP.Shared.Common.RoleCommon();
                common.Id = model.Id;
                common.RoleName = model.RoleName;
                common.User = StaticData.GetUser();
                common.IsActive = model.IsActive;
                var response = buss.Manage(common);
                if (response.ErrorCode == 1)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(model);
                }
                return RedirectToAction("Index");
                //}else
                //    return HttpNotFound();
            }

            return View(model);
        }


        


    }
}