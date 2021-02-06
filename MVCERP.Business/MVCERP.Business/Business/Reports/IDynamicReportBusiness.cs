using MVCERP.Shared;
using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.Reports
{
    public interface IDynamicReportBusiness
    {
        List<DynamicReportData> GetColumns(string User);
        DataTable GetReport(string User, DynamicReportCommon model);

        DbResponse UpdateAgentCommission(string commFromDate, string commToDate, string PaymentAc, string User);

    }
}
