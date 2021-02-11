using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Controllers
{
    public class BackLogController : Controller
    {
        //
        // GET: /BackLog/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewTask()
        {
            return View();
        }

    }
}
