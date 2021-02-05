using System.Collections.Generic;

namespace MVCERP.Shared
{
    public class DynamicReportCommon
    {
        public string ReportName { get; set; }
        public List<DynamicReportData> ColumnList { get; set; }
        public string Column { get; set; }
        public string Clause { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ReportType { get; set; }
        public string AgentCode { get; set; }
        public string CommissionType { get; set; }

        public string BatchNo { get; set; }
        public string IsApproved { get; set; }

        public string OfficerCode { get; set; }
        public string OfficerName { get; set; }
        public string ManagerCode { get; set; }
        public string ManagerName { get; set; }
        public string Branch { get; set; }
        public string AgentBusinessStatus { get; set; }
        public string LicenseNo { get; set; }
        public string ContactNo { get; set; }
        public string BankACNo { get; set; }

        public string PolicyNo { get; set; }
        public string HolderName { get; set; }
        public string DOB { get; set; }
        public string DOC { get; set; }
        public string PT { get; set; }
        public string NextFUP { get; set; }
        public string PolicyStatus { get; set; }
        public string PayMode { get; set; }
        public string SA { get; set; }
        public string Status { get; set; }
        public string Country { get; set; }
        public string Manpower { get; set; }

        public string Plan { get; set; }
        public string GroupId { get; set; }

        public string FullName { get; set; }

        public string PlanID { get; set; }
        public string EngFatherName { get; set; }
        public string RegisterNo { get; set; }
        public string Term { get; set; }
        // public string DOB { get; set; }

        public string Mobile { get; set; }
        public string NomineeName { get; set; }
        public string NomRelation { get; set; }
        public string EngNomineeFatherName { get; set; }
        public string Age { get; set; }

        public string Qualification { get; set; }
        public string Occupation2 { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string ProposerName { get; set; }
        public string ProposerMobileNo { get; set; }

        public string ProposerOcc { get; set; }
        public string Income { get; set; }
        public string ProposerDOB { get; set; }
        public string ProposerAge { get; set; }
        public string ProposerHeight { get; set; }
        public string ProposerWeight { get; set; }

        public string SalesCode { get; set; }
        public string CollectionType { get; set; }
        public string DateType { get; set; }
        public string IncentiveType { get; set; }

        public string ComissionOf { get; set; }
        public string Year { get; set; }
        public string SuperiorCode { get; set; }
        
    }
    public class DynamicReportData
    {
        public string COLUMN_NAME;
        public string DISPNAME;
    }
}
