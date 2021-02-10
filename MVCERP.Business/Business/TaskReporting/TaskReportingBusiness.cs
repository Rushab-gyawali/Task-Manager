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

        public DbResponse ChangeTask(string id, string task)
        {
            return repo.ChangeTask(id, task);
        }

        public List<TaskReportingCommon> GetAllTask()
        {
            return repo.GetAllTask();
        }

        public IEnumerable<TaskReportingCommon> GetStatus()
        {
            return repo.GetStatus();
        }
    }
}
