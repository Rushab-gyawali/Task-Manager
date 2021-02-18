using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.TaskReporting
{
   public interface ITaskReportingBusiness
    {
        List<TaskReportingCommon> GetAllTask();
        IEnumerable<TaskReportingCommon> GetStatus();
        List<TaskReportingCommon> StatusCount(TaskReportingCommon common);
        List<TaskReportingCommon> StatusList(string status,string user);


        DbResponse ChangeTask(string id, string task);

    }
}
