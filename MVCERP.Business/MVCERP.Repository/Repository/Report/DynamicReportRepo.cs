using MVCERP.Shared.Common;
using MVCERP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCERP.Shared.Library;
using System.Data;

namespace MVCERP.Repository.Repository.Report
{
    public class DynamicReportRepo : IDynamicReportRepo
    {
        RepositoryDao dao;
        public DynamicReportRepo()
        {
            dao = new RepositoryDao();
        }
        public List<DynamicReportData> GetColumns(string User)
        {
            List<DynamicReportData> list = new List<DynamicReportData>();
            var sql = "EXEC proc_DynamicReport @flag='column' ";
            sql += ",@user = " + dao.FilterString(User);
            var dt = dao.ExecuteDataTable(sql);
            foreach (DataRow item in dt.Rows)
            {
                var data = new DynamicReportData()
                {
                    COLUMN_NAME = item["COLUMN_NAME"].ToString(),
                    DISPNAME = item["DISPNAME"].ToString()
                };
                list.Add(data);
            }
            return list;
            //return (List<DynamicReportData>)Mapper.DataTableToClass<DynamicReportData>(dt);
        }
        public DataTable GetReport(string User, DynamicReportCommon model)
        {
            DataTable dt = null;
            if (model.ReportName == "DynamicReport")
                dt = GetReportDynamic(User, model);
            else if (model.ReportName == "NewBusiness")
                dt = GetReportNewBusiness(User, model);
            else if (model.ReportName == "NonPaid")
                dt = GetReportNotPaid(User, model);
            else if (model.ReportName == "AgentDetail")
                dt = GetAgentDetail(User, model);
            else if (model.ReportName == "PolicyAsOn")
                dt = GetPolicyAsOn(User, model);
            else if (model.ReportName == "RenewalBusiness")
                dt = GetReportRenewalBusiness(User, model);
            else if (model.ReportName == "NewBusinessSummary")
                dt = GetReportNewBusinessSummary(User, model);
            else if (model.ReportName == "RenewalBusinessSummary")
                dt = GetReportRenewalBusinessSummary(User, model);
            else if (model.ReportName == "CommissionSummary")
                dt = GetReportCommissionSummary(User, model);
            else if (model.ReportName == "CommissionPayable")
                dt = GetReportCommissionPayable(User, model);
            else if (model.ReportName == "RenewalDates")
                dt = GetRenewalDates(User, model);
            else if (model.ReportName == "CommissionDetail")
                dt = GetReportCommissionDetail(User, model);
            else if (model.ReportName == "Forfeiture")
                dt = GetReportForfeiture(User, model);
            else if (model.ReportName == "LapsedBusiness")
                dt = GetReportLapsedBusiness(User, model);
            else if (model.ReportName == "LapsedBusinessSummary")
                dt = GetReportLapsedBusinessSummary(User, model);
            else if (model.ReportName == "LapsedRevivalBusiness")
                dt = GetReportLapsedRevivalBusiness(User, model);
            else if (model.ReportName == "LapsedRevivalBusinessSummary")
                dt = GetReportLapsedRevivalBusinessSummary(User, model);
            else if (model.ReportName == "MaturityDetail")
                dt = GetReportMaturityDetail(User, model);
            else if (model.ReportName == "MaturitySummary")
                dt = GetReportMaturitySummary(User, model);
            else if (model.ReportName == "PremiumSummary")
                dt = GetReportPremiumSummary(User, model);
            else if (model.ReportName == "SurrenderDetail")
                dt = GetReportSurrenderDetail(User, model);
            else if (model.ReportName == "SurrenderSummary")
                dt = GetReportSurrenderSummary(User, model);
            else if (model.ReportName == "DeathClaimDetail")
                dt = GetReportDeathClaimDetail(User, model);
            else if (model.ReportName == "DeathClaimSummary")
                dt = GetReportDeathClaimSummary(User, model);
            else if (model.ReportName == "AgentBusiness")
                dt = GetReportAgentBusiness(User, model);
            else if (model.ReportName == "AgentNewBusinessDetail")
                dt = GetReportAgentBusinessDetail(User, model);
            else if (model.ReportName == "AgentNewBusinessSummary")
                dt = GetReportAgentBusinessSummary(User, model);
            else if (model.ReportName == "SurvivalBenifitDetail")
                dt = GetReportSurvivalBenifitDetail(User, model);
            else if (model.ReportName == "SurvivalBenefitSummary")
                dt = GetReportSurvivalBenefitSummary(User, model);
            else if (model.ReportName == "PolicyLoanPayment")
                dt = GetReportPolicyLoanPayment(User, model);
            else if (model.ReportName == "PolicyLoanRepayment")
                dt = GetReportPolicyLoanRepayment(User, model);
            else if (model.ReportName == "PolicyLoanSummary")
                dt = GetReportPolicyLoanSummary(User, model);
            else if (model.ReportName == "ProposalDetail")
                dt = GetReportProposalDetail(User, model);
            else if (model.ReportName == "ForeignPolicy")
                dt = GetReportForeignPolicyDetail(User, model);
            else if (model.ReportName == "PlanWiseBusiness")
                dt = GetReportPlanWiseBusiness(User, model);
            else if (model.ReportName == "GroupPolicy")
                dt = GetReportGroupPolicyReport(User, model);
            else if (model.ReportName == "GroupPolicySummary")
                dt = GetReportGroupPolicySummaryReport(User, model);
            else if (model.ReportName == "CurrentDay")
                dt = GetCurrentDayReport(User, model);
            else if (model.ReportName == "AML")
                dt = GetAML(User, model);
            else if (model.ReportName == "PassiveAgentReport")
                dt = GetPassiveAgentReport(User, model);
            else if (model.ReportName == "IncentiveCriteriaReport")
                dt = GetIncentiveCriteriaReport(User, model);

            else if (model.ReportName == "AgentNewBusinessSummaryRegionalReport")
                dt = GetAgentNewBusinessSummaryRegionalReport(User, model);

            else if (model.ReportName == "AgentNewBusinessDetailRegionalReport")
                dt = GetAgentNewBusinessDetailRegionalReport(User, model);

            else if (model.ReportName == "RegionalReportAsOnReport")
                dt = GetRegionalReportAsOnReport(User, model);
            else if (model.ReportName == "SalesReport")
                dt = GetSalesReport(User, model);
            else if (model.ReportName == "CashBankReport")
                dt = GetCashBankReport(User, model);

            else if (model.ReportName == "PolicyAsOnDetail")
                dt = GetPolicyAsOnDetailReport(User, model);

            else if (model.ReportName == "GroupPolicySummaryNewReport")
                dt = GetGroupPolicySummaryReportNew(User, model);

            else if (model.ReportName == "GroupPolicyCurrentDateReport")
                dt = GetGroupPolicyCurrentDateReport(User, model);
            else if (model.ReportName == "GroupComissionSummary")
                dt = GetGroupComissionSummary(User, model);

            else if (model.ReportName == "AgentSuperoirIncentiveReport")
                dt = AgentSuperoirIncentiveReport(User, model);

            else if (model.ReportName == "CheckPremiumReport")
                dt = CheckPremiumReport(User, model);

            else if (model.ReportName == "CheckPremiumReport")
                dt = CheckPremiumReport(User, model);

            else if (model.ReportName == "RBEDownLineReport")
                dt = GetRBEDownLineReport(User, model);

            else if (model.ReportName == "RBEDrillDownReport")
                dt = GetRBEDrillDownReport(User, model);

            else if (model.ReportName == "GroupReport")
                dt = GetGroupReport(User,model);

            else if (model.ReportName == "AgentTravelIncentiveReport")
                dt = GetAgentTravelIncentiveReport(User, model);
            else if (model.ReportName == "CashReport")
                dt = GetCashReport(User, model);

            return dt;
        }

        public DataTable GetCashReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='CashReport' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate = " + dao.FilterString(model.FromDate);
            sql += ",@toDate = " + dao.FilterString(model.ToDate);
            sql += ",@branch = " + dao.FilterString(model.Branch);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable GetAgentTravelIncentiveReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptAgentTravelIncentiveReport' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@branch = " + dao.FilterString(model.Branch);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }

        public DataTable GetGroupReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='GroupReport' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@Status = " + dao.FilterStringDR(model.Status);
            sql += ",@groupId = " + dao.FilterStringDR(model.GroupId);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }

        public DataTable GetRegionalReportAsOnReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC Proc_GetDynamicRegionalReport @flag='rptNewBusiness' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@branch = " + dao.FilterStringDR(model.Branch);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable CheckPremiumReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC Proc_GetDynamicReport @flag='CheckPremiumDetails' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@branch = " + dao.FilterStringDR(model.Branch);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable GetAgentNewBusinessDetailRegionalReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC Proc_GetDynamicRegionalReport @flag='rptAgentNewBusinessDetailRegionalReport' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@branch = " + dao.FilterStringDR(model.Branch);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable GetAgentNewBusinessSummaryRegionalReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC Proc_GetDynamicRegionalReport @flag='rptAgentNewBusinessSummaryRegionalReport' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@branch = " + dao.FilterStringDR(model.Branch);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        private DataTable GetCurrentDayReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptCurrentDay ' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);

            //sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            //sql += ",@status = " + dao.FilterStringDR(model.Status);

            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }

        private DataTable GetReportGroupPolicySummaryReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptGroupPolicySummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            //sql += ",@status = " + dao.FilterStringDR(model.Status);

            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable GetGroupPolicySummaryReportNew(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptGroupPolicySummaryNew' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            sql += ",@Datetype = " + dao.FilterStringDR(model.DateType);
            sql += ",@GroupId = " + dao.FilterStringDR(model.GroupId);
            //sql += ",@status = " + dao.FilterStringDR(model.Status);

            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }

        private DataTable GetReportGroupPolicyReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptGroupPolicy' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            sql += ",@Datetype = " + dao.FilterStringDR(model.DateType);
            sql += ",@GroupId = " + dao.FilterStringDR(model.GroupId);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable GetReportDynamic(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_DynamicReport @flag='rptPolicyDetail' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@strSelect  = " + dao.FilterStringDR(model.Column);
            sql += ",@strClause = " + dao.FilterStringDR(model.Clause);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable GetReportNewBusiness(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptNewBusiness' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@reportType = " + dao.FilterStringDR(model.ReportType);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetIncentiveCriteriaReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_tblIncentive @flag='incentiveCriteriaReport' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@StartDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@EndDate = " + dao.FilterStringDR(model.ToDate);
            //sql += ",@agentCode  = " + dao.FilterStringDR(model.AgentCode);
            sql += ",@IncentiveType  = " + dao.FilterStringDR(model.IncentiveType);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }

        public DataTable GetPolicyAsOn(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptPolicyAsOn' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            sql += ",@reportType = " + dao.FilterStringDR(model.ReportType);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetPolicyAsOnDetailReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptPolicyAsOnDetail' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            sql += ",@reportType = " + dao.FilterStringDR(model.ReportType);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }

        public DataTable GetReportNotPaid(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='ApprovedNotPaid' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable GetAgentDetail(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='AgentDetail' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@AgentCode = " + dao.FilterStringDR(model.AgentCode);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);



            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable GetRenewalDates(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='renewalDates' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@paymode = " + dao.FilterStringDR(model.PayMode);
            sql += ",@policyNo = " + dao.FilterStringDR(model.PolicyNo);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetReportNewBusinessSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptNewBusinessSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@reportType = " + dao.FilterStringDR(model.ReportType);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetReportPlanWiseBusiness(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptPlanWiseBusiness' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@branch = " + dao.FilterStringDR(model.Branch);
            sql += ",@plan = " + dao.FilterStringDR(model.Plan);
            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetReportRenewalBusiness(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptRenewalBusiness' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@reportType = " + dao.FilterStringDR(model.ReportType);
            sql += ",@branch=" + dao.FilterStringDR(model.Branch);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetReportRenewalBusinessSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptRenewalBusinessSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@reportType = " + dao.FilterStringDR(model.ReportType);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetReportCommissionSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_CommissionReport @flag='rptCommissionSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@agentCode  = " + dao.FilterStringDR(model.AgentCode);
            //sql += ",@branch = " + dao.FilterStringDR(model.Branch);

            sql += ",@commissionType = " + dao.FilterStringDR(model.CommissionType);


            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetReportCommissionPayable(string User, DynamicReportCommon model)
        {
            var sql = "EXECproc_GetDynamicReport @flag='rptCommissionPayable' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@branch = " + dao.FilterStringDR(model.Branch);
            sql += ",@commissionType = " + dao.FilterStringDR(model.CommissionType);
            sql += ",@agentCode  = " + dao.FilterStringDR(model.AgentCode);

            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable GetReportCommissionDetail(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_CommissionReport @flag='rptCommissionDetail' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@agentCode  = " + dao.FilterStringDR(model.AgentCode);
            sql += ",@commissionType = " + dao.FilterStringDR(model.CommissionType);
            //sql += ",@Branch= " + dao.FilterStringDR(model.Branch);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }

        //FORFEITURE
        public DataTable GetReportForfeiture(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptForfeiture' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //LAPSED BUSINESS
        public DataTable GetReportLapsedBusiness(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptLapsedBusiness' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //LAPSED BUSINESS Summary
        public DataTable GetReportLapsedBusinessSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptLapsedBusinessSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //LAPSED REVIVAL BUSINESS 
        public DataTable GetReportLapsedRevivalBusiness(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptLapsedRevivalBusiness' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //LAPSED REVIVAL BUSINESS Summary
        public DataTable GetReportLapsedRevivalBusinessSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptLapsedRevivalBusinessSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //MATURITY DETAIL
        public DataTable GetReportMaturityDetail(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptMaturityDetail' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //MATURITY SUMMARY
        public DataTable GetReportMaturitySummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptMaturitySummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //PREMIUM SUMMARY
        public DataTable GetReportPremiumSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptPremiumSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //SURRENDER BUSINESS DETAILS
        public DataTable GetReportSurrenderDetail(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptSurrenderDetail' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //SURRENDER BUSINESS SUMMARY
        public DataTable GetReportSurrenderSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptSurrenderSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //DEATH CLAIM DETAILS
        public DataTable GetReportDeathClaimDetail(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptDeathClaimDetail' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //DEATH CLAIM SUMMARY
        public DataTable GetReportDeathClaimSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptDeathClaimSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }

        // Agent Business Report

        public DataTable GetReportAgentBusiness(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptAgentBusiness' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@AgentCode = " + dao.FilterStringDR(model.AgentCode);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            sql += ",@LicenseNo = " + dao.FilterStringDR(model.LicenseNo);
            sql += ",@agentBusinessStatus = " + dao.FilterStringDR(model.AgentBusinessStatus);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }

        // Agent New Business Detail Report

        public DataTable GetReportAgentBusinessDetail(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptAgentBusinessDetail' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@Branch = " + dao.FilterString(model.Branch);

            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }

        // Agent New Business Summary Report

        public DataTable GetReportAgentBusinessSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptAgentBusinessSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@Branch = " + dao.FilterString(model.Branch);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }


        // Survival Benefit Detail Report

        public DataTable GetReportSurvivalBenifitDetail(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptSurvivalBenifitDetail' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }

        // Survival Benefit Summary Report

        public DataTable GetReportSurvivalBenefitSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptSurvivalBenefitSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }


        // POLICYLOAN  PAYMENT REPORT

        public DataTable GetReportPolicyLoanPayment(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptPolicyLoanPayment' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }

        //POLICYLOAN  REPAYMENT REPORT
        public DataTable GetReportPolicyLoanRepayment(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptPolicyLoanRepayment' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        //POLICYLOAN  SUMMARY REPORT

        public DataTable GetReportPolicyLoanSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptPolicyLoanSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetReportProposalDetail(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptProposalDetail' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@status = " + dao.FilterStringDR(model.Status);
            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetReportForeignPolicyDetail(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptForeignPolicy' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@status = " + dao.FilterStringDR(model.Status);
            sql += ",@country = " + dao.FilterStringDR(model.Country);
            sql += ",@manpower = " + dao.FilterStringDR(model.Manpower);

            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DbResponse UpdateAgentCommission(string commFromDate, string commToDate, string PaymentAc, string User)
        {
            var sql = "EXEC proc_Commission_Payment @Flag='SaveCommission' ";
            sql += " ,@User = " + dao.FilterString(User);
            sql += ",@fromDate = " + dao.FilterString(commFromDate);
            sql += ",@toDate = " + dao.FilterString(commToDate);
            sql += ",@PayableNo = " + dao.FilterString(PaymentAc);

            return dao.ParseDbResponse(sql);
        }

        private DataTable GetAML(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='AML' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@policyNo = " + dao.FilterStringDR(model.PolicyNo);
            sql += ",@branch  = " + dao.FilterStringDR(model.Branch);
            sql += ",@status  = " + dao.FilterStringDR(model.Status);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }

        public List<DynamicReportCommon> GetRecordFromPolicy(string User, string PolicyNo)
        {
            var list = new List<DynamicReportCommon>();
            try
            {
                var sql = "EXEC proc_GetDynamicReport  @flag='getRecordFromPolicy' ";
                sql += ",@User = " + dao.FilterString(User);
                sql += ",@policyNo = " + dao.FilterString(PolicyNo);
                var dt = dao.ExecuteDataTable(sql);
                return (List<DynamicReportCommon>)Mapper.DataTableToClass<DynamicReportCommon>(dt);
            }
            catch (Exception)
            {
                return list;
            }
        }

        private DataTable GetPassiveAgentReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='PassiveAgent' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable GetSalesReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC Proc_GetDynamicReport @flag='rptSalesReport' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@branch = " + dao.FilterStringDR(model.Branch);
            sql += ",@salesCode = " + dao.FilterStringDR(model.SalesCode);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }

        public DataTable GetCashBankReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptCashBankReport' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@reportType = " + dao.FilterStringDR(model.ReportType);
            sql += ",@collectionType = " + dao.FilterStringDR(model.CollectionType);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetRBEDownLineReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptRBEDownLineReport' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);         
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetRBEDrillDownReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='rptRBEDrillDownReport' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            sql += ",@Superiorcode = " + dao.FilterStringDR(model.SuperiorCode);
            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }
        public DataTable GetGroupPolicyCurrentDateReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='GroupPolicyCurrentDateReport' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            //sql += ",@reportType = " + dao.FilterStringDR(model.ReportType);
            //sql += ",@collectionType = " + dao.FilterStringDR(model.CollectionType);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }

        public DataTable AgentSuperoirIncentiveReport(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_AgentSuperior_Incentive_Report  ";
            sql += "@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            var dt = dao.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable GetGroupComissionSummary(string User, DynamicReportCommon model)
        {
            var sql = "EXEC proc_GetDynamicReport @flag='GroupComissionSummary' ";
            sql += ",@user = " + dao.FilterString(User);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            //sql += ",@reportType = " + dao.FilterStringDR(model.ReportType);
            //sql += ",@collectionType = " + dao.FilterStringDR(model.CollectionType);
           // sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            sql += ",@ComissionOf = " + dao.FilterStringDR(model.ComissionOf);
            sql += ",@PolicyNo = " + dao.FilterStringDR(model.PolicyNo);
            sql += ",@GroupId = " + dao.FilterStringDR(model.GroupId);
            sql += ",@Year = " + dao.FilterStringDR(model.Year);
            var dt = dao.ExecuteDataTable(sql);
            return dt;

        }


        public object LockCommissionDetail(string frmdate, string todatee, string getUser)
        {
            var sql = "EXEC proc_CommissionReport @flag='lockcommssiondetail'";
            sql += ",@user = " + dao.FilterString(getUser);
            sql += ",@fromDate  = " + dao.FilterStringDR(frmdate);
            sql += ",@toDate = " + dao.FilterStringDR(todatee);
            return dao.ParseDbResponse(sql);
        }

        public object LockCommissionSummary(string frmdate, string todatee, string getUser)
        {
            var sql = "EXEC proc_CommissionReport @flag='lockcommssionsummary' ";
            sql += ",@user = " + dao.FilterString(getUser);
            sql += ",@fromDate  = " + dao.FilterStringDR(frmdate);
            sql += ",@toDate = " + dao.FilterStringDR(todatee);
            return dao.ParseDbResponse(sql);
        }
    }
}
