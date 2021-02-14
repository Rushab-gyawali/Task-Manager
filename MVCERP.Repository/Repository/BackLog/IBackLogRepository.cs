using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.BackLog
{
    public interface IBackLogRepository
    {
        List<BackLogCommon> ListBackLogTask();
        DbResponse AddBackLogTask(BackLogCommon setup);
        List<BackLogCommon> GetById(string ID);
        DbResponse DeleteTask(BackLogCommon common);
    }
}
