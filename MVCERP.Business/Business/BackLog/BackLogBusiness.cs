using MVCERP.Repository.Repository.BackLog;
using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.BackLog
{
     public class BackLogBusiness : IBackLogBusiness
    {
        IBackLogRepository repo;


        public BackLogBusiness(IBackLogRepository _repo)
        {
            repo = _repo;
        }

        public DbResponse AddBackLogTask(BackLogCommon setup)
        {
            return repo.AddBackLogTask(setup);
        }

        public DbResponse DeleteTask(BackLogCommon common)
        {
            return repo.DeleteTask(common);
        }

        public List<BackLogCommon> GetById(string ID)
        {
            return repo.GetById(ID);
        }

        public List<BackLogCommon> ListBackLogTask()
        {
            return repo.ListBackLogTask(); ;
        }

       
    }
}
