using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.TaskReport
{
    public interface ITaskReportBusiness
    {
        List<TaskReportingCommon> Report(TaskReportingCommon common);
    }
}
