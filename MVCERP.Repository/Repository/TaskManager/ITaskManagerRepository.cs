using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.TaskManager
{
    public interface ITaskManagerRepository
    {
        DbResponse TaskManager(TaskReportingCommon common);

        List<TaskReportingCommon> GetById(string TaskId);
        DbResponse DeleteTask(TaskReportingCommon common);

    }
}
