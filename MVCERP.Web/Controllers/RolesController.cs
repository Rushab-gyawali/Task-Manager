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
   
    public class RolesController : Controller
    {
        MVCERP.Business.Business.Roles.IRolesBusiness buss;
        private const string RoleId = "108420";
        private const string ControllerName = "Role";
        private const string ViewId = "108400";
        private const string AddEditId = "108410";

        public RolesController(MVCERP.Business.Business.Roles.IRolesBusiness _buss)
        {
            buss = _buss;
        }


        //
        // GET: /Roles/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Manage()
        {
            StaticData.CheckFunctionId(ControllerName, AddEditId);
            RolesModel model = new RolesModel();
            //var data = buss.GetListById(StaticData.GetUser(), StaticData.GetIdFromQuery());
            //if (data.Count > 0)
            //{
            //    model.RoleName = data[0].RoleName;
            //    model.Id = data[0].Id;
            //    model.IsActive = data[0].IsActive;
            //}

            return View();
        }



        [HttpPost]
        public ActionResult Manage(RolesModel model)
        {
            StaticData.CheckFunctionId(ControllerName, AddEditId);
            if (ModelState.IsValid)
            {
                MVCERP.Shared.Common.RolesCommon common = new MVCERP.Shared.Common.RolesCommon();
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
                
            }

            return View(model);
        }

    }
}
