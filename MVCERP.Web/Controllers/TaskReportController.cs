using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Controllers
{
    public class TaskReportController : Controller
    {
        //
        // GET: /TaskReport/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Assigned()
        {
            //return RedirectToAction("Index","TaskReport");
            return View();
        }

    }
}
