﻿using MVCERP.Business.Business.Common;
using MVCERP.Business.Business.Permission;
using MVCERP.Shared.Common;
using MVCERP.Web.Library;
using MVCERP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
            var user = StaticData.GetUser();
            var data = buss.MenuList();
            ViewBag.user = user;
            ViewData["Roles"] = StaticData.SetDDLValue(ddl.SetDropdownRoles("ListRoles", StaticData.GetUser()), "", "Select Role");
            return View(data);
            
        }

        [HttpPost]
        public ActionResult Index(PermissionCommon common)
        {
            var user = StaticData.GetUser();
            ViewData["Roles"] = StaticData.SetDDLValue(ddl.SetDropdownRoles("ListRoles", StaticData.GetUser()), "", "Select Role");
            var data = buss.GetMenuPermission(common,user);
            return RedirectToAction("Index","Permission");
        }

        [HttpGet]
        public ActionResult GetMenuByUser(PermissionCommon common)
        {
            var user = StaticData.GetUser();
            var data = buss.GetMenuByUser(user);
            ViewBag.user = user;
            ViewData["Roles"] = StaticData.SetDDLValue(ddl.SetDropdownRoles("ListRoles", StaticData.GetUser()), "", "Select Role");
            //return new JavaScriptSerializer().Serialize(data);
            return PartialView("_Menu",data);
        }

    }
}
