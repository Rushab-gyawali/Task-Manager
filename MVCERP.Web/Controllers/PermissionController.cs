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
        //
        // GET: /Permission/

        public ActionResult Index()
        {

            string id = Request.QueryString["id"];
            var RoleId = StaticData.Base64Decode_URL(id);
            var model = new PermissionModel();
            if (RoleId == "")
            {
                var user = StaticData.GetUser();
                ViewBag.user = user;
                //ViewData["AssignTo"] = StaticData.SetDDLValue(ddl.SetDropdownUser("UserDropDown", StaticData.GetUser()), "", "Select Role");
                return View();
            }
            else
            {
             


                //ViewData["AssignTo"] = StaticData.SetDDLValue(ddl.SetDropdownUser("UserDropDown", StaticData.GetUser()), "", "Select User");
                return View(model);
            }
            
        }

    }
}
