using MVCERP.Business.Business.Common;
using MVCERP.Business.Business.Reports;
using MVCERP.Shared;
using MVCERP.Web.Filters;
using MVCERP.Web.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Controllers
{
    [SessionExpiryFilter]
    public class DynamicReportController : Controller
    {
        //
        // GET: /DynamicReport/
        bool MergeColumnHead = false;
        string TotalInColumn = "";
        string CountInColumn = "";
        private string MenuName = "DynamicReport";
        private string ControllerName = "DynamicReport";
        IDynamicReportBusiness buss;
        ICommonBuss ddl;
        public DynamicReportController(IDynamicReportBusiness _buss, ICommonBuss _ddl)
        {
            buss = _buss;
            ddl = _ddl;
        }
        public ActionResult Index()
        {
            var cols = buss.GetColumns(StaticData.GetUser());
            return View(cols);
        }
        public ActionResult Report()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Report(string FunctionId)
        {
            var ReportName = Request.Form["ReportName"];
            DataTable dt = null;

            if (ReportName == "Dynamic")
            {
                dt = GetDynamicReport();
            }
            else if (ReportName == "NewBusiness")
            {
                dt = GetNewBusinessReport();
            }
            else if (ReportName == "PolicyAsOn")
            {
                dt = GetPolicyAsOnReport();
            }
            else if (ReportName == "NonPaid")
            {
                dt = GetNotPaidReport();
            }
            else if (ReportName == "AgentDetail")
            {
                dt = GetAgentDetail();
            }
            else if (ReportName == "RenewalBusiness")
            {
                dt = GetRenewalBusinessReport();
            }
            else if (ReportName == "RenewalDates")
            {
                dt = GetRenewalDatesReport();
            }
            else if (ReportName == "NewBusinessSummary")
            {
                dt = GetNewBusinessSummaryReport();
            }
            else if (ReportName == "RenewalBusinessSummary")
            {
                dt = GetRenewalBusinessSummaryReport();
            }
            else if (ReportName == "CommissionSummary")
            {
                dt = CommissionSummaryReport();
            }
            else if (ReportName == "CommissionDetail")
            {
                dt = CommissionDetailReport();
            }
            else if (ReportName == "CommissionPayable")
            {
                dt = CommissionPayableReport();
            }

            else if (ReportName == "IncentiveCriteriaReport")
            {
                dt = IncentiveCriteriaReport();
            }
            else if (ReportName == "PolicyLoanPayment")
            {
                dt = PolicyLoanPaymentReport();
            }
            else if (ReportName == "PolicyLoanRepayment")
            {
                dt = PolicyLoanRepaymentReport();
            }
            else if (ReportName == "PolicyLoanSummary")
            {
                dt = PolicyLoanSummaryReport();
            }

            else if (ReportName == "SurvivalBenefitsDetail")
            {
                dt = SurvivalBenefitsDetailReport();
            }
            else if (ReportName == "SurvivalBenefitsSummary")
            {
                dt = SurvivalBenefitsSummaryReport();
            }

            else if (ReportName == "AgentBusiness")
            {
                dt = AgentBusinessReport();
            }
            else if (ReportName == "AgentNewBusinessSummary")
            {
                dt = AgentNewBusinessSummaryReport();
            }
            else if (ReportName == "AgentNewBusinessDetail")
            {
                dt = AgentNewBusinessDetailReport();
            }
            else if (ReportName == "DeathClaimDetail")
            {
                dt = DeathClaimDetailReport();
            }
            else if (ReportName == "DeathClaimSummary")
            {
                dt = DeathClaimSummaryReport();
            }
            else if (ReportName == "SurrenderDetail")
            {
                dt = SurrenderDetailReport();
            }
            else if (ReportName == "SurrenderSummary")
            {
                dt = SurrenderSummaryReport();
            }
            else if (ReportName == "PremiumSummary")
            {
                dt = PremiumSummaryReport();
            }
            else if (ReportName == "MaturityDetail")
            {
                dt = MaturityDetailReport();
            }
            else if (ReportName == "MaturitySummary")
            {
                dt = MaturitySummaryReport();
            }
            else if (ReportName == "LapsedRevivalDetail")
            {
                dt = LapsedRevivalDetailReport();
            }
            else if (ReportName == "LapsedRevivalSummary")
            {
                dt = LapsedRevivalSummaryReport();
            }
            else if (ReportName == "LapsedDetail")
            {
                dt = LapsedDetailReport();
            }
            else if (ReportName == "LapsedSummary")
            {
                dt = LapsedSummaryReport();
            }
            else if (ReportName == "Forfeiture")
            {
                dt = ForfeitureReport();
            }
            else if (ReportName == "ProposalDetail")
            {
                dt = ProposalDetailReport();
            }
            else if (ReportName == "ForeignPolicy")
            {
                dt = ForeignPolicyDetailReport();
            }
            else if (ReportName == "GroupPolicy")
            {
                dt = GroupPolicyDetailReport();
            }

            else if (ReportName == "GroupPolicySummaryNewReport")
            {
                dt = GetGroupPolicySummaryReport();
            }

            else if (ReportName == "PlanWiseBusiness")
            {
                dt = PlanWiseBusiness();
            }
            else if (ReportName == "CurrentDay")
            {
                dt = CurrentDayDetailReport();
            }
            else if (ReportName == "AMLReport")
            {
                dt = GetAMLReport();
            }
            else if (ReportName == "PassiveAgentReport")
            {
                dt = GetPassiveAgentReport();
            }

            else if (ReportName == "AgentNewBusinessSummaryRegionalReport")
            {
                dt = AgentNewBusinessSummaryRegionalReport();
            }

            else if (ReportName == "AgentNewBusinessDetailRegionalReport")
            {
                dt = AgentNewBusinessDetailRegionalReport();
            }

            else if (ReportName == "RegionalReportAsOnReport")
            {
                dt = RegionalReportAsOnReport();
            }

            else if (ReportName == "SalesReport")
            {
                dt = SalesCodeReport();
            }

            else if (ReportName == "GroupReport")
            {
                dt = GetGroupReport();
            }

            else if (ReportName == "CashBankReport")
            {
                dt = GetCashBankReport();
            }

            else if (ReportName == "PolicyAsOnDetail")
            {
                dt = GetPolicyAsOnDetailReport();
            }

            else if (ReportName == "GroupPolicyCurrentDateReport")
            {
                dt = GetGroupPolicyCurrentDateReport();
            }
            else if (ReportName == "GroupComissionSummary")
            {
                dt = GetGroupComissionSummary();
            }
            else if (ReportName == "GroupSuperiorReport")
            {
                dt = AgentSuperoirIncentiveReport();
            }
            else if (ReportName == "CheckPremiumReport")
            {
                dt = CheckPremiumReportdt();
            }
            else if (ReportName == "RBEDownLineReport")
            {
                dt = GetRBEDownLineReport();
            }
            else if (ReportName == "AgentTravelIncentiveReport")
            {
                dt = AgentTravelIncentiveReportdt();
            }
            else if (ReportName == "CashReport")
            {
                dt = CashReportdt();
            }
            else
            {
                return RedirectToAction("", "Error");
            }
            ViewData["ReportBody"] = GenerateReport(dt);
            return View();
        }

        public DataTable GetGroupReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var status = Request.Form["Status"];
            var GroupId = Request.Form["GroupId"].Split('|')[0];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "GroupReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Status = status;
            model.GroupId = GroupId;

            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From Date :</b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To Date :</b>" + StaticData.DBToFrontDate(model.ToDate);
            if (model.GroupId != "")
                strFilter += " <br/><br/><b>GroupID :</b>" + model.GroupId;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            ViewData["filters"] = strFilter;
            return dt;

        }
        public DataTable RegionalReportAsOnReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var reportType = Request.Form["ReportType"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "RegionalReportAsOnReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;

            ViewData["head"] = "Regional As On Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From Date :</b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To Date :</b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "5|6";

            ViewData["filters"] = strFilter;
            return dt;


        }

        public DataTable CheckPremiumReportdt()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "CheckPremiumReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            
            ViewData["head"] = "Check Premium Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From Date :</b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To Date :</b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "6|7";

            ViewData["filters"] = strFilter;
            return dt;


        }




        public DataTable AgentNewBusinessDetailRegionalReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var reportType = Request.Form["ReportType"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            //var salecode = Request.Form["SalesCode"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "AgentNewBusinessDetailRegionalReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;

            ViewData["head"] = "Agent New Business Detail Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From Date :</b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To Date :</b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;


            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "5|6";
            ViewData["filters"] = strFilter;
            return dt;
        }


        public DataTable AgentNewBusinessSummaryRegionalReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var reportType = Request.Form["ReportType"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "AgentNewBusinessSummaryRegionalReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;

            ViewData["head"] = "Agent New Business Summary Regional Report";
            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "2|3|4";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From Date :</b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To Date :</b>" + StaticData.DBToFrontDate(model.ToDate);
           
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;
            
            return dt;
        }

        private DataTable GroupPolicyDetailReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var status = Request.Form["Status"];
            var GroupId = Request.Form["GroupId"].Split('|')[0];
            var branchName = Request.Form["BranchName"];
            var DateType = Request.Form["DateDropdown"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "GroupPolicy";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.GroupId = GroupId.ToString();
            model.DateType = DateType;
            ViewData["head"] = "Group Policy Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);

            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "2|3|4|5";
            return dt;
        }
        public DataTable GetGroupPolicySummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var status = Request.Form["Status"];
            var GroupId = Request.Form["GroupId"].Trim('|')[0];
            var DateType = Request.Form["DateDropdown"];


            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "GroupPolicySummaryNewReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.GroupId = GroupId.ToString();
            model.DateType = DateType;

            ViewData["head"] = "Group Policy Detail Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (model.Branch != "")
                strFilter += " <b>Branch :</b>" + model.Branch;

            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "2|3|4|5|7";
            return dt;
        }

        private DataTable CurrentDayDetailReport()
        {
            var FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            var branch = Request.Form["Branch"];
            var status = Request.Form["Status"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "CurrentDay";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
           
            model.Branch = branch;

            ViewData["head"] = "Current Day Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);

            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "6|7";
            return dt;
        }
        private DataTable GroupPolicySummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            //var branch = Request.Form["Branch"];
            var status = Request.Form["Status"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "GroupPolicy";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            //model.Branch = branch;

            ViewData["head"] = "Group Policy Summary Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);

            if (model.Branch != "")
                strFilter += " <b>Branch :</b>" + model.Branch;
            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "2|";
            return dt;
        }
        public DataTable IncentiveCriteriaReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            //var AgCode = Request.Form["AgentCode"].Split('|')[0];
            var IncentiveType = Request.Form["IncentiveType"];
            var IncentiveName=IncentiveType.Split('|')[1];



            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "IncentiveCriteriaReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.IncentiveType = IncentiveType.Split('|')[0];


            //model.AgentCode = AgCode;

            ViewData["head"] = " Incentive Criteria Report";
            var strFilter = "";

            if (model.IncentiveType != "")
                strFilter += " <b>" + IncentiveName + "</b></br>";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);
           
            //if (model.Branch != "")
            //    strFilter += " <b>Branch :</b>" + model.Branch;


            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "8";

            return dt;
        }
        public ActionResult RBEDownLineReport()
        {
            StaticData.CheckFunctionId(ControllerName, "113900");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            return View();
        }
        private DataTable GetDynamicReport()
        {
            var SelectQuery = Request.Form["txtQuery"];
            var WhereQuery = Request.Form["txtCondition"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "DynamicReport";
            model.Column = SelectQuery;
            model.Clause = WhereQuery;
            ViewData["head"] = "Dynamic Report";
            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            return dt;
        }
        private DataTable GetNewBusinessReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var ReportType = Request.Form["ReportType"];
            var Branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "NewBusiness";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.ReportType = ReportType;
            model.Branch = Branch;




            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "2|3|4";
            ViewData["head"] = "New Business Report";

            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;

            return dt;
        }


        public DataTable GetPolicyAsOnReport()
        {
            var ReportType = Request.Form["ReportType"];
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var Branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();       
            model.ReportName = "PolicyAsOn";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = Branch;

            model.ReportType = ReportType;

            ViewData["head"] = "Policy  As On Report";
            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "2|3|4|5|6|7|8|9|10|11";
            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;


            ViewData["filters"] = strFilter;
          

            //ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");

         
            return dt;
        }
        public DataTable GetPolicyAsOnDetailReport()
        {
            var ReportType = Request.Form["ReportType"];
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var Branch = Request.Form["Branch"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "PolicyAsOnDetail";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = Branch;

            model.ReportType = ReportType;

            ViewData["head"] = "Policy  As On Detail Report";
            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "2|3|4|5|6|7|8|9|10|11";
            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;
            if (model.Branch != "")
                strFilter += " <b>Branch :</b>" + model.Branch;


            ViewData["filters"] = strFilter;


            //ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");

           
            return dt;
        }

        private DataTable GetNotPaidReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var Branch = Request.Form["Branch"];
            var ReportType = Request.Form["ReportType"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "NonPaid";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = Branch;
            model.ReportType = ReportType;

            ViewData["head"] = "Approved But Not Paid Report";
            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NotPaidReportType"), "1");

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "1";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);

            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;
          
           
            return dt;
        }
        public DataTable GetAgentDetail()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var Branch = Request.Form["Branch"];
            var agentCode = Request.Form["AgentCode"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "AgentDetail";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = Branch;
            model.AgentCode = agentCode;
           model.AgentCode =model.AgentCode.Split('|')[0].Trim(' ');
            ViewData["head"] = "Agent Detail Report";

            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;
            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            return dt;
        }


        private DataTable GetRenewalBusinessReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var ReportType = Request.Form["ReportType"];
            var Branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "RenewalBusiness";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.ReportType = ReportType;
            model.Branch = Branch;
            ViewData["head"] = "Renewal Business Report";
            var strFilter = "";



            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;
            return dt;
        }
        private DataTable GetNewBusinessSummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var ReportType = Request.Form["ReportType"];
            var Branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "NewBusinessSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.ReportType = ReportType;
            model.Branch = Branch;

            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "1|2|3";
            ViewData["head"] = "New Business Summary Report";
            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;

            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;
            return dt;
        }
        private DataTable GetRenewalBusinessSummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var ReportType = Request.Form["ReportType"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "RenewalBusinessSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.ReportType = ReportType;
            ViewData["head"] = "Renewal Business Summary Report";



            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;

            ViewData["filters"] = strFilter;
            return dt;
        }

        public ActionResult NewBusinessReport()
        {
            StaticData.CheckFunctionId(ControllerName, "103600");
            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All"); 
            return View();
        }

        public ActionResult PolicyAsOn()
        {
            StaticData.CheckFunctionId(ControllerName, "103650");
            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("PolicyAsOn"), "1");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All"); 
            return View();
        }
        public ActionResult PolicyAsOnDetail()
        {
            StaticData.CheckFunctionId(ControllerName, "103650");
            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("PolicyAsOn"), "1");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            return View();
        }
        public ActionResult RenewalBusinessReport()
        {
            StaticData.CheckFunctionId(ControllerName, "112600");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("GetBranch", StaticData.GetUser()), "1");


            // ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");
            return View();
        }
        public ActionResult NewBusinessSummaryReport()
        {
            StaticData.CheckFunctionId(ControllerName, "112610");
            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("GetBranch", StaticData.GetUser()), "1");

            return View();
        }
        public ActionResult RenewalBusinessSummaryReport()
        {
            StaticData.CheckFunctionId(ControllerName, "112605");
            //ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");
            return View();
        }

        public ActionResult PlanWiseReport()
        {
            StaticData.CheckFunctionId(ControllerName, "113600");
            ViewData["Plan"] = StaticData.SetDDLValue(ddl.SetDropdown("AllPlan"), "All");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All"); 
            return View();

        }
        public ActionResult SalesReport()
        {
            StaticData.CheckFunctionId(ControllerName, "113600");
            ViewData["Plan"] = StaticData.SetDDLValue(ddl.SetDropdown("Plan"), "All");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            return View();

        }
        public DataTable SalesCodeReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var reportType = Request.Form["ReportType"];
            var branch = Request.Form["Branch"];
            var SalesCode = Request.Form["SalesCode"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "AgentNewBusinessDetailRegionalReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;
            model.SalesCode = SalesCode.Split('|')[0];

            ViewData["head"] = "Sales Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From Date :</b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To Date :</b>" + StaticData.DBToFrontDate(model.ToDate);
            if (model.Branch != "")
                strFilter += " <b>Branch :</b>" + model.Branch;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "5|6";
            return dt;
        }

        public ActionResult NotpaidReport()
        {
            StaticData.CheckFunctionId(ControllerName, "103660");
            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NotPaidReportType"), "", "1");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            return View();
        }

        public ActionResult ProposalReport()
        {
            StaticData.CheckFunctionId(ControllerName, "113500");
            ViewData["StatusList"] = StaticData.SetDDLValue(ddl.SetDropdown("ProposalStatus"), "1");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
     
            return View("ProposalDetailReport");
        }
        public ActionResult ForeignPolicyReport()
        {
            LoadDDl();
            return View();
        }


        public ActionResult CheckPremiumReport()
        {
            StaticData.CheckFunctionId(ControllerName, "114300");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            return View("CheckPremiumReport");
        }


        public ActionResult GroupReport()
        {
            ViewData["StatusList"] = StaticData.SetDDLValue(ddl.SetDropdown("GroupStatus"), "All");
            return View("GroupReport");
        }

        public ActionResult AgentTravelIncentiveReport()
        {
            LoadDDl();
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            return View();
        }
        public ActionResult CashReport()
        {
            //StaticData.CheckFunctionId(ControllerName, "113700");
            
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            
            return View();
        }

        public ActionResult GroupPolicyReport()
        {
            StaticData.CheckFunctionId(ControllerName, "113700");
            LoadDDl();
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            ViewData["DateDropdown"] = StaticData.SetDDLValue(ddl.SetDropdown("DateDropdown"), "");
            return View();
        }
        public ActionResult GroupPolicySummaryNewReport()
        {
            StaticData.CheckFunctionId(ControllerName, "113700");
            LoadDDl();
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            ViewData["DateDropdown"] = StaticData.SetDDLValue(ddl.SetDropdown("DateDropdown"),"");

            return View();
        }
        public ActionResult CurrentDayReport()
        {
            StaticData.CheckFunctionId(ControllerName, "113900");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All"); 
            return View();
        }
        private void LoadDDl()
        {
            var ds = ddl.GetDropDownLists("FPPolicy");
            //ViewData["DistrictList"] = StaticData.SetDDLByTable(ds.Tables[0], "", "Select District");
            ViewData["CountryList"] = StaticData.SetDDLByTable(ds.Tables[1], "", "Select Country");
            //ViewData["GenderList"] = StaticData.SetDDLByTable(ds.Tables[2], "", "Select Gender");
            //ViewData["ClientCodeList"] = StaticData.SetDDLByTable(ds.Tables[3], "", "Select Agent");
            //ViewData["RelationList"] = StaticData.SetDDLByTable(ds.Tables[4], "", "Select Relation");
            //ViewData["NominRelationList"] = StaticData.SetDDLByTable(ds.Tables[4], "", "Select Relation");
            ViewData["ManpowerList"] = StaticData.SetDDLByTable(ds.Tables[5], "", "Select Manpower");
         
        }
        public ActionResult AMLReport()
        {
            StaticData.CheckFunctionId(ControllerName, "112615");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            ViewData["StatusList"] = StaticData.SetDDLValue(ddl.SetDropdown("ProposalStatus"), "1");
            return View();
        }

        public DataTable CommissionSummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "CommissionSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.AgentCode = Request.Form["AgentCode"];
            model.AgentCode = model.AgentCode.Split('|')[0].Trim(' ');
            model.CommissionType = Request.Form["CommissionType"];
            model.Branch = Request.Form["Branch"];
            ViewData["head"] = "Commission Summary Report";


            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "5|6|7|8|9";
            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;
            ViewData["fromdate"] = StaticData.DBToFrontDate(model.FromDate);
            ViewData["todate"] = StaticData.DBToFrontDate(model.ToDate);
            ViewData["Lockcommission"] = "commissionsummary";
            ViewData["filters"] = strFilter;
            return dt;
        }
        public DataTable CommissionDetailReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var agentCode = Request.Form["AgentCode"];
            var commissionType = Request.Form["CommissionType"];
            var Branch = Request.Form["Branch"];

            var branchName = Request.Form["BranchName"];


            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "CommissionDetail";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);

            model.AgentCode = agentCode;
            model.AgentCode = model.AgentCode.Split('|')[0].Trim(' ');
            model.Branch = Branch;
            model.CommissionType = commissionType;

            ViewData["head"] = "Commission Detail Report";


            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "6|8|9|10|11";
            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;
            return dt;
        }

        public DataTable CommissionPayableReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var Branch = Request.Form["Branch"];
            var commissionType = Request.Form["CommissionType"];
            var agentCode = Request.Form["AgentCode"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "CommissionPayable";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = Branch;
            model.AgentCode = agentCode;
            model.AgentCode = model.AgentCode.Split('|')[0].Trim(' ');
            model.CommissionType = commissionType;
          
            ViewData["head"] = "Commission Payable Report";

            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);

            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            return dt;
        }
        public DataTable PolicyLoanPaymentReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "PolicyLoanPayment";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;

            ViewData["head"] = "Policy Loan Payment Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }

        public DataTable GetRenewalDatesReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            string PolicyNo = Request.Form["PolicyNo"].Split('|')[0].Trim();
            var PayMode = Request.Form["PayMode"];
            var Branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "RenewalDates";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.PayMode = PayMode;
            model.PolicyNo = PolicyNo;
            model.Branch = Branch;

            ViewData["head"] = "Renewal Dates Report";


            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;


            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;
            return dt;

        }
        public DataTable PolicyLoanRepaymentReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "PolicyLoanRepayment";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;

            ViewData["head"] = "Policy Loan Repayment Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;


            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable PolicyLoanSummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "PolicyLoanSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;

            ViewData["head"] = "Policy Loan Summary Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;


            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable SurvivalBenefitsDetailReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            var reportType = Request.Form["ReportType"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "SurvivalBenifitDetail";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;

            ViewData["head"] = "Survival Benifit Detail Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b> To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            //if (model.ReportType != "")
            //    strFilter += "<b> Report Type :</b>"+model.ReportType;

            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable SurvivalBenefitsSummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            var reportType = Request.Form["ReportType"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "SurvivalBenefitSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;

            ViewData["head"] = "Survival Benifit Summary Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b> To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;
            //if (model.ReportType != "")
            //    strFilter += "<b> Report Type :</b>" + model.ReportType;

            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable AgentBusinessReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            //var reportType = Request.Form["ReportType"];

            var agentCode = Request.Form["AgentCode"];
            //var officerCode = Request.Form["OfficerCode"];
            //var officerName = Request.Form["OfficerName"];
            //var managerCode = Request.Form["ManagerCode"];
            //var managerName = Request.Form["ManagerName"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            var agentBusinessStatus = Request.Form["AgentBusinessStatus"];
            var licenseNo = Request.Form["LicenseNo"];
            //var contactNo = Request.Form["ContactNo"];
            //var bankACNo = Request.Form["BankACNo"];


            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "AgentBusiness";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);

            model.AgentCode = agentCode;
            model.AgentCode = model.AgentCode.Split('|')[0].Trim(' ');

            //model.OfficerCode = officerCode;
            //model.OfficerName = officerName;
            //model.ManagerCode = managerCode;
            //model.ManagerName = managerName;
            model.Branch = branch;
            model.AgentBusinessStatus = agentBusinessStatus;
            model.LicenseNo = licenseNo;

            //model.ContactNo = contactNo;
            //model.BankACNo = bankACNo;
            //model.ReportType = reportType;

            ViewData["head"] = "New Agent Business Report";
            var strFilter = "";


            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "3|4";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;
            if (model.AgentCode != "")
                strFilter += " <b>Agent Code :</b>" + model.AgentCode;
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;
            if (model.AgentBusinessStatus != "")
                strFilter += " <b>Status :</b>" + model.AgentBusinessStatus;
            if (model.LicenseNo != "")
                strFilter += " <b>License No :</b>" + model.LicenseNo;
           
            ViewData["filters"] = strFilter;

            return dt;
        }
        public DataTable AgentNewBusinessSummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var reportType = Request.Form["ReportType"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "AgentNewBusinessSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;



            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "2|3|4";
            ViewData["head"] = "New Agent Business Summary Report";
            var strFilter = "";
            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From Date :</b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To Date :</b>" + model.ToDate;

            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;


            ViewData["filters"] = strFilter;

            return dt;
        }
        public DataTable AgentNewBusinessDetailReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var reportType = Request.Form["ReportType"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "AgentNewBusinessDetail";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;



            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            ViewData["head"] = "Agent New Business Detail Report";
            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From Date :</b>" + model.FromDate;
            if (model.ToDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += " <b>To Date :</b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;

            TotalInColumn = "5|6";

            return dt;
        }
        public DataTable DeathClaimDetailReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var reportType = Request.Form["ReportType"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "DeathClaimDetail";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;

            ViewData["head"] = "Death Claim Detail Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b> To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch </b>" + branchName;


            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable DeathClaimSummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var reportType = Request.Form["ReportType"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "DeathClaimSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;

            ViewData["head"] = "Death Claim Summary Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b> To </b>" + model.ToDate;
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable SurrenderDetailReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var reportType = Request.Form["ReportType"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "SurrenderDetail";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;

            ViewData["head"] = "Surrender Detail Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b> To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch </b>" + branchName;


            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable SurrenderSummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var reportType = Request.Form["ReportType"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "SurrenderSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.ReportType = reportType;

            ViewData["head"] = "Surrender Summary";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b> To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch </b>" + branchName;


            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable PremiumSummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var reportType = Request.Form["ReportType"];
            var branchName = Request.Form["BranchName"];
            var policyNo = Request.Form["PolicyNo"];
            var holderName = Request.Form["HolderName"];
            var dob = Request.Form["DOB"];
            var doc = Request.Form["DOC"];
            var pt = Request.Form["PT"];
            var nextFUP = Request.Form["NextFUP"];
            var policyStatus = Request.Form["PolicyStatus"];
            var payMode = Request.Form["PayMode"];
            var branch = Request.Form["Branch"];
            var sa = Request.Form["SA"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "PremiumSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);

            model.PolicyNo = policyNo;
            model.HolderName = holderName;
            model.DOB = dob;
            model.DOC = doc;
            model.PT = pt;
            model.NextFUP = nextFUP;
            model.PolicyStatus = policyStatus;
            model.PayMode = payMode;
            model.Branch = branch;
            model.SA = sa;
            model.ReportType = reportType;

            ViewData["head"] = "Premium Summary Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);

            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            if (model.ReportType != "")
                strFilter += "<b>Report Type :</b>" + model.ReportType;
            if (model.PolicyNo != "")
                strFilter += "<b>Policy No :</b>" + model.PolicyNo;
            if (model.HolderName != "")
                strFilter += "<b>Holder Name :</b>" + model.HolderName;
            if (model.DOB != "")
                strFilter += "<b>DOB :</b>" + model.DOB;
            if (model.DOC != "")
                strFilter += "<b>DOC :</b>" + model.DOC;
            if (model.PolicyNo != "")
                strFilter += "<b>PT :</b>" + model.PT;
            if (model.NextFUP != "")
                strFilter += "<b>Next FUP :</b>" + model.NextFUP;
            if (model.PolicyStatus != "")
                strFilter += "<b>Policy Status :</b>" + model.PolicyStatus;
            if (model.PayMode != "")
                strFilter += "<b>Pay Mode :</b>" + model.PayMode;
            if (model.SA != "")
                strFilter += "<b>SA :</b>" + model.SA;

            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable MaturityDetailReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "MaturityDetail";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;

            ViewData["head"] = "Surrender Summary";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b> To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch </b>" + branchName;


            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable MaturitySummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "MaturitySummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;

            ViewData["head"] = "Surrender Summary";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b>Branch </b>" + branchName;


            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable LapsedRevivalDetailReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "LapsedRevivalBusiness";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;

            ViewData["head"] = "Surrender Summary";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b> To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch </b>" + branchName;


            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable LapsedRevivalSummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "LapsedRevivalBusinessSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;

            ViewData["head"] = "Surrender Summary Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b> To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch </b>" + branchName;

            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable LapsedDetailReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "LapsedBusiness";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;

            ViewData["head"] = "Surrender Summary Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b> To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch </b>" + branchName;


            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable LapsedSummaryReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "LapsedBusinessSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;

            ViewData["head"] = "Surrender Summary Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b> To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch </b>" + branchName;

            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable ForfeitureReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "Forfeiture";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            ViewData["head"] = "Forfeiture Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;



            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }
        public DataTable ProposalDetailReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var status = Request.Form["Status"];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "ProposalDetail";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.Status = status;

            ViewData["head"] = "Proposal Detail Report";


            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;

            if (model.Status != "")
                strFilter += " <b>Status :</b>" + ((model.Status == "1") ? "APPROVED" : "UNAPPROVED");
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;

            return dt;
        }
        public DataTable ForeignPolicyDetailReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var status = Request.Form["Status"];

            var manpower = Request.Form["ManpowerList"];
            var country = Request.Form["CountryList"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "ForeignPolicy";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.Status = status;
            model.Country = country;
            model.Manpower = manpower;

            ViewData["head"] = "Foreign Policy Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);


            ViewData["filters"] = strFilter;

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "8|";
            return dt;
        }

        public DataTable GetAMLReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];
            var status = Request.Form["Status"];
            var policyNo = Request.Form["PolicyNo"].Split('|')[0];
            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "AML";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.PolicyNo = policyNo;
            model.Status = status;

            ViewData["head"] = "AML Report";
            var strFilter = "";

            if (model.FromDate != "")
                strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
            if (model.ToDate != "")
                strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);

            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            if (model.Status != "")
                strFilter += " <b>Status :</b>" + ((model.Status == "1") ? "APPROVED" : "UNAPPROVED");

            ViewData["filters"] = strFilter;

            TotalInColumn = "3";

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            return dt;
        }

        public DataTable GetRBEDownLineReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];

            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "RBEDownLineReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;


            ViewData["head"] = "RBE DownLine Report";


            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;

            if (model.Status != "")
                strFilter += " <b>Status :</b>" + ((model.Status == "1") ? "APPROVED" : "UNAPPROVED");
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;

            return dt;
        }

        public DataTable CashReportdt()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "CashReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.Status = "1";
            var branchName = Request.Form["BranchName"];

            ViewData["head"] = "Cash Report";
            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;

            if (model.Status != "")
                strFilter += " <b>Status :</b>" + ((model.Status == "1") ? "APPROVED" : "UNAPPROVED");
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;
            return dt;
        }

        public DataTable AgentTravelIncentiveReportdt()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];

            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "AgentTravelIncentiveReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = "2018/07/16";
            model.Branch = branch;
            model.FromDate = "2018/06/16";


            ViewData["head"] = "Agent Travel Incentive Report";


            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;

            if (model.Status != "")
                strFilter += " <b>Status :</b>" + ((model.Status == "1") ? "APPROVED" : "UNAPPROVED");
            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;

            return dt;
        }
        public ActionResult RBEDownDrillDownReport(string SuperiorCode)
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var branch = Request.Form["Branch"];

            var branchName = Request.Form["BranchName"];
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "RBEDrillDownReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = branch;
            model.SuperiorCode = SuperiorCode;


            ViewData["head"] = "RBE DownLine Report";


            DataTable dt = buss.GetReport(StaticData.GetUser(), model);

            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;


            if (branchName != "")
                strFilter += " <b> Branch :</b>" + branchName;

            ViewData["filters"] = strFilter;
            ViewData["ReportBody"] = GenerateReport(dt);
            return View("Report", dt);
        }
        public ActionResult AgentDetail()
        {
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("AgentDetailType"), "1");
            return View();
        }
        public ActionResult CommissionPayable()
        {

            ViewData["CommissionTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("CommissionType"), "");
            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
         
            return View();
        }
        public DataTable PlanWiseBusiness()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var Branch = Request.Form["Branch"];
            var branchName = Request.Form["BranchName"];
            var Plan = Request.Form["Plan"];
            
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "PlanWiseBusiness";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = Branch;
            model.Plan =Plan;
            ViewData["head"] = "PlanWise Business Report";


            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "4|5|6";
            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += " <b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;
            if (branchName != "")
                strFilter += " <b>Branch </b>" + branchName;



            ViewData["filters"] = strFilter;

            return dt;
        }

        private DataTable GetPassiveAgentReport()
        {
            {
                var FromDate = Request.Form["FromDate"];
                var ToDate = Request.Form["ToDate"];

                DynamicReportCommon model = new DynamicReportCommon();
                model.ReportName = "PassiveAgentReport";
                model.FromDate = StaticData.FrontToDBDate(FromDate);
                model.ToDate = StaticData.FrontToDBDate(ToDate);

                ViewData["head"] = "Passive Agent Report";

                var strFilter = "";

                if (model.FromDate != "")
                    strFilter += "<b>From </b>" + StaticData.DBToFrontDate(model.FromDate);
                if (model.ToDate != "")
                    strFilter += " <b>To </b>" + StaticData.DBToFrontDate(model.ToDate);

                ViewData["filters"] = strFilter;

                DataTable dt = buss.GetReport(StaticData.GetUser(), model);
                return dt;
            }
        }
        public ActionResult CashBankReport()
        {

            //ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("ReportTypeBankOrCash"), "1");
            ViewData["CollectionType"] = StaticData.SetDDLValue(ddl.SetDropdown("CollectionTypeForBank", StaticData.GetUser()), "", "Select Collection Type");

            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            return View();
        }

        public DataTable GetCashBankReport()
        {
            var FromDate = Request.Form["FromDate"];
            var ToDate = Request.Form["ToDate"];
            var ReportType = Request.Form["ReportType"];
            var Branch = Request.Form["Branch"];
            var CollectionType = Request.Form["CollectionType"];


            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "CashBankReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.ReportType = ReportType;
            model.Branch = Branch;
            model.CollectionType = CollectionType;




            ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("ReportTypeBankOrCash"), "1");

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            //TotalInColumn = "2|3";
            ViewData["head"] = "Cash/Cheque/Bank Report";

            var strFilter = "";

            if (model.FromDate != "")
                model.FromDate = StaticData.DBToFrontDate(model.FromDate);
            strFilter += "<b>From </b>" + model.FromDate;
            if (model.ToDate != "")
                model.ToDate = StaticData.DBToFrontDate(model.ToDate);
            strFilter += " <b>To </b>" + model.ToDate;

            ViewData["filters"] = strFilter;

            return dt;
        }
        public ActionResult GroupPolicyCurrentDateReport()
        {

            //ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("ReportTypeBankOrCash"), "1");
            //ViewData["CollectionType"] = StaticData.SetDDLValue(ddl.SetDropdown("CollectionTypeForBank", StaticData.GetUser()), "", "Select Collection Type");

            ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            return View();
        }
        public DataTable GetGroupPolicyCurrentDateReport()
        {
            //var reporttype = request.form["reporttype"];
            var FromDate = Request.Form["fromdate"];
            var ToDate = Request.Form["todate"];
            var Branch = Request.Form["branch"];

            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "GroupPolicyCurrentDateReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.Branch = Branch;

            //model.ReportType = ReportType;
            //ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");

            ViewData["head"] = "Group Policy Current Date Report";

            var strFilter = "";
            if (FromDate != "")
                
            strFilter += "<b>From </b>" +FromDate;
            if (ToDate != "")
                
            strFilter += " <b>To </b>" + ToDate;
            ViewData["filters"] = strFilter;

            //ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            //TotalInColumn = "2|3|4|5|6|7|8|9|10|11";
            return dt;
        }
        public ActionResult GroupComissionSummary()
        {

            //ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("ReportTypeBankOrCash"), "1");
            //ViewData["CollectionType"] = StaticData.SetDDLValue(ddl.SetDropdown("CollectionTypeForBank", StaticData.GetUser()), "", "Select Collection Type");
            ViewData["ComissionOf"] = new List<SelectListItem>
            {
               // new SelectListItem { Selected = true, Text = string.Empty, Value = ""},
                new SelectListItem { Selected = false, Text = "All", Value = ""}, 
               new SelectListItem { Selected = false, Text = "S", Value = "S"},
                new SelectListItem { Selected = false, Text = "C", Value = "C"},
            };
           // ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");
            return View();
        }
        public DataTable GetGroupComissionSummary()
        {
            //var reporttype = request.form["reporttype"];
            var FromDate = Request.Form["fromdate"];
            var ToDate = Request.Form["todate"];
           // var Branch = Request.Form["branch"];
            var ComissionOf = Request.Form["ComissionOf"];
            var PolicyNo = Request.Form["PolicyNo"];
            var GroupId = Request.Form["GroupId"];
            var Year = Request.Form["Year"];


            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "GroupComissionSummary";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
            model.ComissionOf = ComissionOf;
            model.PolicyNo = PolicyNo;
            model.GroupId = GroupId.Split('|')[0];
            model.Year = Year;
            //model.Branch = Branch;

            //model.ReportType = ReportType;
            //ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");

            ViewData["head"] = "Group Comission Summary";

            var strFilter = "";
            if (FromDate != "")

                strFilter += "<b>From </b>" + FromDate;
            if (ToDate != "")

                strFilter += " <b>To </b>" + ToDate;
            ViewData["filters"] = strFilter;

            //ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            //TotalInColumn = "2|3|4|5|6|7|8|9|10|11";
            return dt;
        }



        public DataTable AgentSuperoirIncentiveReport()
        {
            
            var FromDate = Request.Form["fromdate"];
            var ToDate = Request.Form["todate"];
            
            DynamicReportCommon model = new DynamicReportCommon();
            model.ReportName = "AgentSuperoirIncentiveReport";
            model.FromDate = StaticData.FrontToDBDate(FromDate);
            model.ToDate = StaticData.FrontToDBDate(ToDate);
          
            //model.Branch = Branch;

            //ViewData["BranchList"] = StaticData.SetDDLValue(ddl.SetDropdown("BranchList"), "", "All");

            ViewData["head"] = "Agent Superoir Incentive Report";

            var strFilter = "";
            if (FromDate != "")

                strFilter += "<b>From </b>" + FromDate;
            if (ToDate != "")

                strFilter += " <b>To </b>" + ToDate;
            ViewData["filters"] = strFilter;

            //ViewData["ReportTypeList"] = StaticData.SetDDLValue(ddl.SetDropdown("NewBusinessReportType"), "1");

            DataTable dt = buss.GetReport(StaticData.GetUser(), model);
            TotalInColumn = "3|4|7|8|9|11|12|13|14|15";
            return dt;
        }
        private string GenerateReport(DataTable dt)
        {
            if (null == dt) return "";

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder("<table width=\"100%\" style=\"border-collapse: collapse; margin-top: 15px;\">");
            sb.Append("<thead>");
            if (MergeColumnHead)
            {
                var columns = new Dictionary<string, string>();

                foreach (DataColumn col in dt.Columns)
                {
                    var splitPos = col.ColumnName.IndexOf('_');
                    if (splitPos == -1)
                    {
                        columns.Add(col.ColumnName, col.ColumnName);
                    }
                    else
                    {
                        var key = col.ColumnName.Substring(0, splitPos);
                        var value = col.ColumnName.Substring(splitPos + 1, col.ColumnName.Length - splitPos - 1);
                        if (!columns.ContainsKey(key))
                        {
                            columns.Add(key, value);
                        }
                        else
                        {
                            columns[key] = columns[key] + "|" + value;
                        }
                    }
                }

                var row1 = "";
                var row2 = "";

                foreach (var kvp in columns)
                {
                    string[] values = kvp.Value.Split('|');

                    if (values.Length == 1)
                    {
                        row1 = row1 + "<th rowspan=\"2\" style=\"padding: 5px; border: 1px solid #ccc;text-align:center\">" + kvp.Key + "</th>";
                    }
                    else
                    {
                        row1 = row1 + "<th  style=\"padding: 5px; border: 1px solid #ccc;text-align:center\" colspan=\"" + values.Length + "\">" + kvp.Key + "</th>";

                        foreach (string value in values)
                        {
                            row2 = row2 + "<th style=\"padding: 5px; border: 1px solid #ccc;text-align:center\" >" + value + "</th>";
                        }
                    }
                }

                sb.Append("<tr><th rowspan='2'  style=\"padding: 5px; border: 1px solid #ccc;text-align:center\">S.N.</td> " + row1 + "</tr>");
                sb.Append("<tr>" + row2 + "</tr>");

            }
            else
            {
                sb.Append("<th  style=\"padding: 5px; border: 1px solid #ccc;text-align:center\">S.N.</th>");
                foreach (var item in dt.Columns)
                {
                    sb.Append("<th  style=\"padding: 5px; border: 1px solid #ccc;text-align:center\">" + item + "</th>");
                }
            }

            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            int CN = 0;

            string[] totalFieldsArray = new string[TotalInColumn.Replace(" ", "").Split('|').Length];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int arrayCnt = 0;
                sb.Append("<tr>");
                CN++;
                sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc\">" + CN + "</td>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    bool isTotal = false;
                    foreach (var item in TotalInColumn.Replace(" ", "").Split('|'))
                    {
                        if (item == j.ToString())
                        {
                            isTotal = true;
                        }
                    }
                    if (isTotal)
                    {
                        double arrayVal = (totalFieldsArray[arrayCnt] == null ? 0 : StaticData.ParseDouble(totalFieldsArray[arrayCnt].ToString()));
                        var dbVal = dt.Rows[i][j].ToString();
                        totalFieldsArray[arrayCnt] = (arrayVal + StaticData.ParseDouble(dbVal)).ToString();
                        sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc ;text-align:right \">" + StaticData.ParseMinusValue(dbVal) + "</td>");
                        arrayCnt++;
                    }
                    else
                    {
                        string[] isLink = dt.Rows[i][j].ToString().Split('#');
                        if (isLink.Length > 1)
                        {
                            sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc;text-align:center \">" +
                                "<a href='AcReport?q=" + StaticData.Base64Encode_URL(isLink[0]) + "'>" + isLink[1] + "</a>"
                                + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc;text-align:center \">" + dt.Rows[i][j] + "</td>");
                        }

                    }
                }
                sb.Append("</tr>");
            }

            if (totalFieldsArray.Length > 0)
            {
                sb.Append("<tr>");
                sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc\"><strong>Total</strong></td>");
                CN = 0;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    bool isTotal = false;
                    foreach (var item in TotalInColumn.Replace(" ", "").Split('|'))
                    {
                        if (item == j.ToString())
                        {
                            isTotal = true;
                        }
                    }
                    if (isTotal)
                    {
                        sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc ;text-align:right \"><strong>" + StaticData.ParseMinusValue(totalFieldsArray[CN]) + "</strong></td>");
                        CN++;
                    }
                    else
                    {
                        sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc;text-align:center \">&nbsp;</td>");
                    }
                }
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
        }
    }
}
