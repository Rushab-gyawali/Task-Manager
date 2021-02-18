using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.TaskManager
{
    public interface ITaskManagerBusiness
    {
        DbResponse TaskManager(TaskReportingCommon common);
        List<TaskReportingCommon> GetById(string TaskId);
        DbResponse DeleteTask(TaskReportingCommon common);
    }
}
