﻿using MVCERP.Web.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Controllers
{
    public class HelperController : Controller
    {
        //
        // GET: /Helper/

        public ActionResult Index()
        {
            StaticData.CheckSession();
            return View();
        }
        public ActionResult ConvertIntoNepDate(string dt)
        {
            StaticData.CheckSession();
            var NepDt = StaticData.ConvertEng_NepDate(StaticData.FrontToDBDate(dt), 1);
            return Json(NepDt, JsonRequestBehavior.AllowGet);
        }
    }
}
