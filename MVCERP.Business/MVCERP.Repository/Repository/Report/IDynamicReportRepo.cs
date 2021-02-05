using MVCERP.Shared;
using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Report
{
    public interface IDynamicReportRepo
    {
        List<DynamicReportData> GetColumns(string User);
        DataTable GetReport(string User, DynamicReportCommon model);

        DbResponse UpdateAgentCommission(string commFromDate, string commToDate, string PaymentAc, string User);
        object LockCommissionDetail(string frmdate, string todatee, string getUser);
        object LockCommissionSummary(string frmdate, string todatee, string getUser);
    }
}
