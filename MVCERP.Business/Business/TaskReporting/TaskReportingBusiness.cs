using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCERP.Repository.Repository.TaskReporting;

namespace MVCERP.Business.Business.TaskReporting
{
    public class TaskReportingBusiness : ITaskReportingBusiness
    {
        ITaskReportingRepository repo;
        public TaskReportingBusiness(ITaskReportingRepository _repo)
        {
            repo = _repo;
        }

        public List<TaskReportingCommon> GetAllAssignedTask()
        {
           return repo.GetAssignedTask();
        }

        public List<TaskReportingCommon> GetAllCompletedTask()
        {
            return repo.GetCompletedTask();
        }

        public List<TaskReportingCommon> GetAllPendingTask()
        {
            return repo.GetPendingTask();
        }

        public List<TaskReportingCommon> GetAllTask()
        {
            return repo.GetAllTask();
        }
    }
}
