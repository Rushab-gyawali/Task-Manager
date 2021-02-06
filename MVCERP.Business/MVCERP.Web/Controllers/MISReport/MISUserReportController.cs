using MVCERP.Business.Business.Common;
using MVCERP.Web.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iSolutionLife.Web.Controllers.MISReport
{
    public class MISUserReportController : Controller
    {
        //
        // GET: /MISReportFilter/
        ICommonBuss ddl;
        public MISUserReportController(ICommonBuss _ddl)
        {
            ddl = _ddl;
        }       

    
        public ActionResult MISUserRegister()
        {
            return View();
        }      


    }
}
