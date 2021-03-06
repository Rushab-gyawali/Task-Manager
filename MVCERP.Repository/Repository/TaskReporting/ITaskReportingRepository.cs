﻿using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCERP.Repository.Repository.TaskReporting
{
   public interface ITaskReportingRepository
    {
        List<TaskReportingCommon> GetAllTask();
        IEnumerable<TaskReportingCommon> GetStatus();
        DbResponse ChangeTask(string id, string task);
        List<TaskReportingCommon> StatusCount(TaskReportingCommon common);
        List<TaskReportingCommon> StatusList(string status, string user);

    }
}
