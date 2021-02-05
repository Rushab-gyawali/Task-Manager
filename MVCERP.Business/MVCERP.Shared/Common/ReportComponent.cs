using System;
using System.Collections.Generic;
using System.Data;

namespace MVCERP.Shared.Common.ReportComponent
{
    [Serializable()]
    public class ReportComponent
    {
        public string ReportTitle { get; set; }
        public string ID { get; set; }
        public string SumColumns { get; set; }
        public List<RightAlignColumnPosition> RightAlignColumnPosition { get; set; }
        public DataTable ReportData { get; set; }
        public DataTable ReportHeader { get; set; }
        public DataTable ColumnInfo { get; set; }
        public string ExcelLink { get; set; }
        public string ReportName { get; set; }
        public string GroupId { get; set; }
        public bool ShowHeader { get; set; }
        public string ToDate { get; set; }
        public string Department { get; set; }
        public string FromDate { get; set; }
        public string commissionType { get; set; }
        public string AgentCode { get; set; }
        public string SuperiorType { get; set; }
        public string IsCorporateAgent { get; set; }
        public string AgentType { get; set; }
        public string Branch { get; set; }
        public string Region { get; set; }
        public string StaffCode { get; set; }
        public string TrainingID { get; set; }
        public string GroupID { get; set; }
        public string BatchNo { get; set; }
        public string PolicyNo { get; set; }
        public string ReportType { get; set; }
        public string Plan { get; set; }
        public string BusinessType { get; set; }
        public string IsDoc { get; set; }
        public string NepaliYear { get; set; }
        public string NepaliMonth { get; set; }
        public string PayMode { get; set; }
        public string Status { get; set; }
        public string LicenseNo { get; set; }
        public string AMLReport { get; set; }
        public string PositionOfColumnToBeInTotal { get; set; }
        public string AgentBusinessStatus { get; set; }
        public string Flag { get; set; }
        public string Type { get; set; }
        public string ProductName { get; set; }
        public string NatureOfEvent { get; set; }
        public string ClaimStatus { get; set; }
        public string FiscalYear { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string VoucherNo { get; set; }
        public string PlanId { get; set; }
        public string UnitCode { get; set; }
        public string ManpowerCode { get; set; }
        public string CreatedBy { get; set; }
        public string RIType { get; set; }
        public string FiscalYear1 { get; set; }
        public string FiscalYear2 { get; set; }
        public string Quarter1 { get; set; }
        public string Quarter2 { get; set; }
        public string Month1 { get; set; }
        public string Month2 { get; set; }
    }
    public class RightAlignColumnPosition
    {
        public string ColumnPosition { get; set; }
    }
}
