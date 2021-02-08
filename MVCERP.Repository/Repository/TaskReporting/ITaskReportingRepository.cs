using MVCERP.Shared.Common;
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
<<<<<<< HEAD
        IEnumerable<TaskReportingCommon> GetStatus();
=======
        List<TaskReportingCommon> GetCompletedTask();
        List<TaskReportingCommon> GetPendingTask();
        List<TaskReportingCommon> GetAssignedTask();

>>>>>>> 011b1b73ea1fc4b9ebbbe8c6c13a7a035989b58a
    }
}
