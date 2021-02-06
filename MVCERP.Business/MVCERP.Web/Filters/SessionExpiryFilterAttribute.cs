using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCERP.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SessionExpiryFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;


            // If the browser session or authentication session has expired...
            if (ctx.Session["UserName"] == null || !MVCERP.Web.Library.UserMonitor.GetInstance().IsUserExists(ctx.Session["UserName"].ToString()))
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                        { "Controller", "Home" },
                        { "Action", "LogOff" }
                        });

                //if (filterContext.HttpContext.Request.IsAjaxRequest())
                //{
                //    // For AJAX requests, we're overriding the returned JSON result with a simple string,
                //    // indicating to the calling JavaScript code that a redirect should be performed.
                //    filterContext.Result = new JsonResult { Data = "_Logon_" };
                //}
                //else
                //{
                //    // For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                //    // simply displays a temporary 5 second notification that they have timed out, and
                //    // will, in turn, redirect to the logon page.
                //    filterContext.Result = new RedirectToRouteResult(
                //        new RouteValueDictionary {
                //        { "Controller", "Home" },
                //        { "Action", "Index" }
                //});
                //}
            }

            base.OnActionExecuting(filterContext);
        }
    }

}