using MVCERP.Business.Business.Common;
using MVCERP.Web.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iSolutionLife.Web.Controllers.MISReport
{
    public class MISController : Controller
    {
        //
        // GET: /MIS/

        ICommonBuss ddl;
        public MISController(CommonBuss _ddl)
        {
            ddl = _ddl;
        }
        public ActionResult Index()
        {
            return View();
        }
    

    }
}
