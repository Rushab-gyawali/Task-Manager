using System;
using System.Web.Mvc;
using MVCERP.Web.Filters;
using MVCERP.Web.Library;
using MVCERP.Web.Models;
using System.Data;
using MVCERP.Business.Business.User;
using MVCERP.Shared.Common;

namespace MVCERP.Web.Controllers
{
    //[Authorize]
    //[InitializeSimpleMembership]
    public class HomeController : Controller
    {
        IUserBusiness buss;
        //.LicStatus version;
        private string LimitedUsers;
        private string UsedDays;
        public HomeController(IUserBusiness _buss)
        {
            buss = _buss;
        }
        public ActionResult Dashboard2()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Index(string log = null)
        {

            //CheckLicense();


            //if (!string.IsNullOrWhiteSpace(StaticData.GetUser()) && log == null)
            //{
            //    var insuranceDetails = buss.GetInsuranceDetails(StaticData.GetUser());
            //    return View("DashBoard", insuranceDetails);
            //}
            UserMonitor.GetInstance().RemoveUser(StaticData.GetUser());
            //WebSecurity.Logout();
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            switch (log)
            {
                case "UnAuthorization":
                    ViewData["msg"] = "Sorry,Your Account is Currently blocked due to too many attempt of UnAuthorization";
                    break;
                case "Error":
                    ViewData["msg"] = "Sorry,Your Account is Currently blocked due to too many attempt of Errors";
                    break;
                default:
                    break;
            }

            return View();
        }

        private string GetDomainName()
        {
            string cURL = HttpContext.Request.Url.AbsoluteUri;
            string[] DomainNames = cURL.Split('/');

            if (DomainNames.Length > 3)
                return DomainNames[2];
            else
                return "Not Found";
        }

        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"]
                    , browser = Request.Browser.Browser + " Version :" + Request.Browser.Version;
        
            var login = new LoginCommon
            {
                UserName = model.UserName,
                Password = StaticData.Base64Encode(model.Password),
                Ip = ip,
                BrowserInfo = browser
            };
            var resp = buss.UserLogin(login);
            if (resp.Code == "0")
            {

                Session["ForcePwdChange"] = resp.ForcePwdChange;
                Session["UserName"] = model.UserName;              
                Session["sysDate"] = StaticData.DBToFrontDate(System.DateTime.Now.ToShortDateString());
                return RedirectToAction("UserDetail", "TaskReporting");
            }
            ViewData["msg"] = "The user name or password provided is incorrect.";
            return View(model);
        }
        // [Authorize]

        [Authorize]

        //[HttpGet]
        ////[Authorize]
        //[SessionExpiryFilter]
        public ActionResult LogOff()
        {
            UserMonitor.GetInstance().RemoveUser(StaticData.GetUser());
            //WebSecurity.Logout();
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("", "Home");
        }
        [AllowAnonymous]
        [SessionExpiryFilter]
        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult GetChartData()
        {

            ChartDataCommon[] obj = new ChartDataCommon[3];
            ChartDataCommon objData = new ChartDataCommon();

            var ds = buss.GetDashBoardChartDetail(StaticData.GetUser());

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Label");
            dt1.Columns.Add("Value");

            if (ds.Tables[0].Rows.Count > 0)
            {
                var dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr[0] = dt.Rows[i]["Label"].ToString(); //"Govt. Bond";
                    dr[1] = dt.Rows[i]["Value"].ToString(); //"19";
                    dt1.Rows.Add(dr);
                }
            }

            #region "Dt1"
            //DataRow dr = dt1.NewRow();
            //dr[0] = "Govt. Bond";
            //dr[1] = "19";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "Fixed-Comm";
            //dr[1] = "19";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "Fixed-Dev";
            //dr[1] = "3";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "Fixed_Fin";
            //dr[1] = "5";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "Share & Deventure";
            //dr[1] = "2";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "Other";
            //dr[1] = "3";
            //dt1.Rows.Add(dr);

            #endregion

            objData.ChartDataList = dt1.DataTableToList<ChartData>();
            obj[0] = objData;

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Label");
            dt2.Columns.Add("Value");

            if (ds.Tables[1].Rows.Count > 0)
            {
                var dt = ds.Tables[1];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt2.NewRow();
                    dr[0] = dt.Rows[i]["Label"].ToString(); //"Govt. Bond";
                    dr[1] = dt.Rows[i]["Value"].ToString(); //"19";
                    dt2.Rows.Add(dr);
                }
            }

            #region "Dt2"
            //dr = dt2.NewRow();
            //dr[0] = "Endowment";
            //dr[1] = "12";
            //dt2.Rows.Add(dr);

            //dr = dt2.NewRow();
            //dr[0] = "Anticipated";
            //dr[1] = "19";
            //dt2.Rows.Add(dr);


            //dr = dt2.NewRow();
            //dr[0] = "Whole Life";
            //dr[1] = "3";
            //dt2.Rows.Add(dr);


            //dr = dt2.NewRow();
            //dr[0] = "Child";
            //dr[1] = "5";
            //dt2.Rows.Add(dr);

            //dr = dt2.NewRow();
            //dr[0] = "Joint Life";
            //dr[1] = "2";
            //dt2.Rows.Add(dr);

            //dr = dt2.NewRow();
            //dr[0] = "Term Life";
            //dr[1] = "3";
            //dt2.Rows.Add(dr);

            #endregion

            objData = new ChartDataCommon();
            objData.ChartDataList = dt2.DataTableToList<ChartData>();
            obj[1] = objData;


            DataTable dt3 = new DataTable();
            dt3.Columns.Add("Label");
            dt3.Columns.Add("Value");
            dt3.Columns.Add("Type");

            if (ds.Tables[2].Rows.Count > 0)
            {
                var dt = ds.Tables[2];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt3.NewRow();
                    dr[0] = dt.Rows[i]["Label"].ToString(); //"Govt. Bond";
                    dr[1] = dt.Rows[i]["Value"].ToString(); //"19";
                    dr[2] = dt.Rows[i]["Type"].ToString();
                    dt3.Rows.Add(dr);
                }
            }

            #region "Dt3"
            //dr = dt3.NewRow();
            //dr[0] = "January";
            //dr[1] = "12";
            //dr[2] = "Target";
            //dt3.Rows.Add(dr);

            //dr = dt3.NewRow();
            //dr[0] = "February";
            //dr[1] = "19";
            //dr[2] = "Target";
            //dt3.Rows.Add(dr);


            //dr = dt3.NewRow();
            //dr[0] = "March";
            //dr[1] = "3";
            //dr[2] = "Target";
            //dt3.Rows.Add(dr);


            //dr = dt3.NewRow();
            //dr[0] = "April";
            //dr[1] = "5";
            //dr[2] = "Target";
            //dt3.Rows.Add(dr);

            //dr = dt3.NewRow();
            //dr[0] = "May";
            //dr[1] = "2";
            //dr[2] = "Target";
            //dt3.Rows.Add(dr);

            //dr = dt3.NewRow();
            //dr[0] = "June";
            //dr[1] = "3";
            //dr[2] = "Target";
            //dt3.Rows.Add(dr);

            //dr = dt3.NewRow();
            //dr[0] = "July";
            //dr[1] = "";
            //dr[2] = "Target";
            //dt3.Rows.Add(dr);

            ////-------------------

            //dr = dt3.NewRow();
            //dr[0] = "January";
            //dr[1] = "20";
            //dr[2] = "Achievement";
            //dt3.Rows.Add(dr);

            //dr = dt3.NewRow();
            //dr[0] = "February";
            //dr[1] = "14";
            //dr[2] = "Achievement";
            //dt3.Rows.Add(dr);


            //dr = dt3.NewRow();
            //dr[0] = "March";
            //dr[1] = "7";
            //dr[2] = "Achievement";
            //dt3.Rows.Add(dr);


            //dr = dt3.NewRow();
            //dr[0] = "April";
            //dr[1] = "5";
            //dr[2] = "Achievement";
            //dt3.Rows.Add(dr);

            //dr = dt3.NewRow();
            //dr[0] = "May";
            //dr[1] = "0";
            //dr[2] = "Achievement";
            //dt3.Rows.Add(dr);

            //dr = dt3.NewRow();
            //dr[0] = "June";
            //dr[1] = "1";
            //dr[2] = "Achievement";
            //dt3.Rows.Add(dr);

            //dr = dt3.NewRow();
            //dr[0] = "July";
            //dr[1] = "";
            //dr[2] = "Achievement";
            //dt3.Rows.Add(dr);

            #endregion


            objData = new ChartDataCommon();
            objData.ChartDataList = dt3.DataTableToList<ChartData>();
            obj[2] = objData;

            var res = Json(obj, JsonRequestBehavior.AllowGet);
            return res;
        }

        public ActionResult GetDashBoardData()
        {
            ProgressBarCommon[] obj = new ProgressBarCommon[3];
            ProgressBarCommon objData = new ProgressBarCommon();

            var ds = buss.GetDashBoardDetail(StaticData.GetUser());

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("label");
            dt1.Columns.Add("value");
            dt1.Columns.Add("color");

            if (ds.Tables[0].Rows.Count > 0)
            {
                var dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt1.NewRow();
                    dr[0] = dt.Rows[i]["title"].ToString();
                    dr[1] = dt.Rows[i]["progressPercentage"].ToString(); //"100%";
                    dr[2] = dt.Rows[i]["cssClass"].ToString(); //"danger";
                    dt1.Rows.Add(dr);
                }
            }
            //------------------------------------------------------

            ScheduleTaskCommon[] objST = new ScheduleTaskCommon[4];
            ScheduleTaskCommon objSTData = new ScheduleTaskCommon();


            DataTable dtST = new DataTable();
            dtST.Columns.Add("topic");
            dtST.Columns.Add("description");
            dtST.Columns.Add("date");
            dtST.Columns.Add("color");

            if (ds.Tables[1].Rows.Count > 0)
            {
                var dt = ds.Tables[1];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtST.NewRow();
                    dr[0] = dt.Rows[i]["title"].ToString(); //"Meeting with reinsurer";
                    dr[1] = dt.Rows[i]["contentText"].ToString(); //"Meeting Detail";
                    dr[2] = dt.Rows[i]["titleDate"].ToString(); //"22 April, 2017";
                    dr[3] = dt.Rows[i]["cssClass"].ToString(); //"danger";
                    dtST.Rows.Add(dr);
                }
            }


            //objData = new ProgressBarCommon();
            objSTData.ScheduleTaskDataList = dtST.DataTableToList<ScheduleTaskData>();
            objST[0] = objSTData;

            var res = Json(obj, JsonRequestBehavior.AllowGet);
            return res;
        }
        public ActionResult GetActivitiesBarData()
        {
            ProgressBarCommon[] obj = new ProgressBarCommon[3];
            ProgressBarCommon objData = new ProgressBarCommon();

            var ds = buss.GetDashBoardDetail(StaticData.GetUser());

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("label");
            dt1.Columns.Add("value");
            dt1.Columns.Add("color");

            if (ds.Tables[0].Rows.Count > 0)
            {
                var dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt1.NewRow();
                    dr[0] = dt.Rows[i]["title"].ToString();
                    dr[1] = dt.Rows[i]["progressPercentage"].ToString(); //"100%";
                    dr[2] = dt.Rows[i]["cssClass"].ToString(); //"danger";
                    dt1.Rows.Add(dr);
                }
            }

            #region dt1
            //var dr =dt1.NewRow();
            //dr[0] = "New Agent added";
            //dr[1] = "100%";
            //dr[2] = "danger";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "Surrender Decreased";
            //dr[1] = "90%";
            //dr[2] = "info";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "Market Area Covered in <strong>70 Days</strong>";
            //dr[1] = "80%";
            //dr[2] = "success";
            //dt1.Rows.Add(dr);



            //dr = dt1.NewRow();
            //dr[0] = "Lapse Decreased";
            //dr[1] = "70%";
            //dr[2] = "info";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "New Branch Expansion";
            //dr[1] = "60%";
            //dr[2] = "danger";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "Loan Recovered <strong>30 Days</strong>";
            //dr[1] = "50%";
            //dr[2] = "info";
            //dt1.Rows.Add(dr);

            #endregion

            objData = new ProgressBarCommon();
            objData.ProgressBarList = dt1.DataTableToList<ProgressBarData>();
            obj[0] = objData;

            var res = Json(obj, JsonRequestBehavior.AllowGet);
            return res;
        }
        public ActionResult GetScheduleTaskData()
        {
            ScheduleTaskCommon[] obj = new ScheduleTaskCommon[4];
            ScheduleTaskCommon objData = new ScheduleTaskCommon();

            var ds = buss.GetDashBoardDetail(StaticData.GetUser());



            DataTable dt1 = new DataTable();
            dt1.Columns.Add("topic");
            dt1.Columns.Add("description");
            dt1.Columns.Add("date");
            dt1.Columns.Add("color");

            if (ds.Tables[1].Rows.Count > 0)
            {
                var dt = ds.Tables[1];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr[0] = dt.Rows[i]["title"].ToString(); //"Meeting with reinsurer";
                    dr[1] = dt.Rows[i]["contentText"].ToString(); //"Meeting Detail";
                    dr[2] = dt.Rows[i]["titleDate"].ToString(); //"22 April, 2017";
                    dr[3] = dt.Rows[i]["cssClass"].ToString(); //"danger";
                    dt1.Rows.Add(dr);
                }
            }

            #region d1
            //DataRow dr = dt1.NewRow();
            //dr[0] = "Meeting with reinsurer";
            //dr[1] = "Meeting Detail";
            //dr[2] = "22 April, 2017";
            //dr[3] = "danger";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "Branch Opening";
            //dr[1] = "New Branch Opening on 27 April, 2017";
            //dr[2] = "27 April, 2017";
            //dr[3] = "info";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "Branch Visit";
            //dr[1] = "Branches visited for business promotion.";
            //dr[2] = "29 April, 2017";
            //dr[3] = "success";
            //dt1.Rows.Add(dr);
            #endregion
            objData = new ScheduleTaskCommon();
            objData.ScheduleTaskDataList = dt1.DataTableToList<ScheduleTaskData>();
            obj[0] = objData;

            var res = Json(obj, JsonRequestBehavior.AllowGet);
            return res;
        }
        public ActionResult GetAgentActivityData()
        {
            AgentActivityCommon[] obj = new AgentActivityCommon[4];
            AgentActivityCommon objData = new AgentActivityCommon();

            var ds = buss.GetDashBoardDetail(StaticData.GetUser());

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("month");
            dt1.Columns.Add("day");
            dt1.Columns.Add("date");
            dt1.Columns.Add("agentname");
            dt1.Columns.Add("activitytext");
            dt1.Columns.Add("activitystatus");
            dt1.Columns.Add("dayscount");

            if (ds.Tables[2].Rows.Count > 0)
            {
                var dt = ds.Tables[2];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr[0] = dt.Rows[i]["activityMonth"].ToString(); //"June";
                    dr[1] = dt.Rows[i]["activityDay"].ToString(); //"Sunday";
                    dr[2] = dt.Rows[i]["activityDate"].ToString(); //"21";
                    dr[3] = dt.Rows[i]["agentName"].ToString(); //"Dinesh Chandra";
                    dr[4] = dt.Rows[i]["activityText"].ToString(); //"Published Training Campaign";
                    dr[5] = dt.Rows[i]["activityStats"].ToString(); //"Attendance";
                    dr[6] = dt.Rows[i]["participants"].ToString(); //"50";
                    dt1.Rows.Add(dr);
                }
            }
            #region d1
            //DataRow dr = dt1.NewRow();
            //dr[0] = "June";
            //dr[1] = "Sunday";
            //dr[2] = "21";
            //dr[3] = "Dinesh Chandra";
            //dr[4] = "Published Training Campaign";
            //dr[5] = "Attendance";
            //dr[6] = "50";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "July";
            //dr[1] = "Monday";
            //dr[2] = "27";
            //dr[3] = "Regional Training";
            //dr[4] = "Meeting Conducted";
            //dr[5] = "Participant";
            //dr[6] = "22";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "August";
            //dr[1] = "Friday";
            //dr[2] = "13";
            //dr[3] = "Agent Delegation";
            //dr[4] = "Dialogue Started";
            //dr[5] = "Attendance";
            //dr[6] = "54";
            //dt1.Rows.Add(dr);
            #endregion
            objData = new AgentActivityCommon();
            objData.AgentActivityDataList = dt1.DataTableToList<AgentActivityData>();
            obj[0] = objData;

            var res = Json(obj, JsonRequestBehavior.AllowGet);
            return res;
        }
        public ActionResult GetPlanHistoryData()
        {
            PlanHistoryCommon[] obj = new PlanHistoryCommon[4];
            PlanHistoryCommon objData = new PlanHistoryCommon();

            var ds = buss.GetDashBoardDetail(StaticData.GetUser());

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("date");
            dt1.Columns.Add("title");
            dt1.Columns.Add("text");
            dt1.Columns.Add("range");

            if (ds.Tables[3].Rows.Count > 0)
            {
                var dt = ds.Tables[3];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr[0] = dt.Rows[i]["planDate"].ToString(); //"April 24th, 2017";
                    dr[1] = dt.Rows[i]["planTitle"].ToString(); //"बाल बालिक";
                    dr[2] = dt.Rows[i]["planText"].ToString(); //"Child endosement";
                    dr[3] = dt.Rows[i]["planRange"].ToString(); //"NRs. 1,000 - NRs. 10,000";
                    dt1.Rows.Add(dr);
                }
            }

            #region d1
            //DataRow dr = dt1.NewRow();
            //dr[0] = "April 24th, 2017";
            //dr[1] = "बाल बालिक";
            //dr[2] = "Child endosement";
            //dr[3] = "NRs. 1,000 - NRs. 10,000";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "April 24th, 2017";
            //dr[1] = "बाल बालिक";
            //dr[2] = "Child endosement";
            //dr[3] = "NRs. 1,000 - NRs. 10,000";
            //dt1.Rows.Add(dr);

            //dr = dt1.NewRow();
            //dr[0] = "April 24th, 2017";
            //dr[1] = "बाल बालिक";
            //dr[2] = "Child endosement";
            //dr[3] = "NRs. 1,000 - NRs. 10,000";
            //dt1.Rows.Add(dr);
            #endregion

            objData = new PlanHistoryCommon();
            objData.PlanHistoryDataList = dt1.DataTableToList<PlanHistoryData>();
            obj[0] = objData;

            var res = Json(obj, JsonRequestBehavior.AllowGet);
            return res;
        }

        public ActionResult ChangePassword()
        {
            var c = new ChangePassword();
            return View(c);
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword model)
        {
            if (string.IsNullOrWhiteSpace(model.OldPassword))
            {
                StaticData.SetMessageInSession(1, "Old Password Field is required");
                ModelState.AddModelError("", "error");
                return View(model);
            }
            if (string.IsNullOrWhiteSpace(model.NewPassword))
            {
                StaticData.SetMessageInSession(1, "New Password Field is required");
                ModelState.AddModelError("", "error");
                return View(model);
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                StaticData.SetMessageInSession(1, "New password and confirmation password do not match.");
                ModelState.AddModelError("", "error");
                return View(model);
            }
            model.UserName = StaticData.GetUser();
            model.User = StaticData.GetUser();
            model.OldPassword = StaticData.Base64Encode(model.OldPassword);
            model.NewPassword = StaticData.Base64Encode(model.NewPassword);
            var resp = buss.ChangePassword(model);
            if (resp.ErrorCode == 1)
            {
                StaticData.SetMessageInSession(resp);
                ModelState.AddModelError("", "error");
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}
