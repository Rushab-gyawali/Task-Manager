using MVCERP.Repository.Repository.TaskManager;
using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.TaskManager
{
    public class TaskManagerBusiness : ITaskManagerBusiness
    {
        ITaskManagerRepository repo;
        public TaskManagerBusiness(ITaskManagerRepository _repo)
        {
            repo = _repo;
        }

        public List<TaskReportingCommon> GetById(string TaskId)
        {
           return repo.GetById(TaskId);
        }

        public DbResponse TaskManager(TaskReportingCommon common)
        {
           return repo.TaskManager(common);
        }

        public DbResponse DeleteTask(TaskReportingCommon common)
        {
            return repo.DeleteTask(common);
        }
    }
}
