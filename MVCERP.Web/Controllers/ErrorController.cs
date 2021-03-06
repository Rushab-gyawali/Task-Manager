﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCERP.Business.Business.Common;
using MVCERP.Business.Business.Error;
using MVCERP.Web.Library;
using MVCERP.Web.Filters;

namespace MVCERP.Web.Controllers
{
    [SessionExpiryFilter]
    public class ErrorController : Controller
    {
        IErrorBusiness buss;
        public ErrorController(IErrorBusiness _buss)
        {
            StaticData.CheckSession();
            buss = _buss;
        }
        public ActionResult Index()
        {
            StaticData.CheckSession();
            var id = StaticData.ReadQueryString("id", "");
            ViewData["id"] = id;
            return View();
        }
        public ActionResult Detail(string id)
        {
            StaticData.CheckSession();
            var errorId = id;
            var errorDetails = buss.Details(id,StaticData.GetUser());
            return View(errorDetails);
        }

    }
}
