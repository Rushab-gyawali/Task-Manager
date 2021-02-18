using MVCERP.Business.Business.Common;
using MVCERP.Business.Business.Permission;
using MVCERP.Web.Library;
using MVCERP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Controllers
{
    public class PermissionController : Controller
    {

        IPermissionBusiness buss;
        ICommonBuss ddl;

        public PermissionController(IPermissionBusiness _buss, ICommonBuss _ddl)
        {
            buss = _buss;
            ddl = _ddl;
        }


        //
        // GET: /Permission/

        public ActionResult Index()
        {
            StaticData.CheckSession();
            string id = Request.QueryString["id"];
            var RoleId = StaticData.Base64Decode_URL(id);
            var model = new PermissionModel();
            if (RoleId == "")
            {
                var user = StaticData.GetUser();
                ViewBag.user = user;
                ViewData["Roles"] = StaticData.SetDDLValue(ddl.SetDropdownRoles("ListRoles", StaticData.GetUser()), "", "Select Role");
                return View();
            }
            else
            {
                ViewData["Roles"] = StaticData.SetDDLValue(ddl.SetDropdownRoles("ListRoles", StaticData.GetUser()), "", "Select Role");
                return View(model);
            }
            
        }

    }
}
