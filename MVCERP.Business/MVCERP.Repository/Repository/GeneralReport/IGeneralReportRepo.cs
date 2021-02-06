using iSolutionLife.Shared.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace iSolutionLife.Repository.Repository.GeneralReport
{
   public interface IGeneralReportRepo
    {
        List<GeneralReportCommon> GetPolicyAsOn(GeneralReportCommon model);
    }
}
