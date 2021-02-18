using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.BackLog
{
    public interface IBackLogBusiness 
    {
        List<BackLogCommon> ListBackLogTask();
        DbResponse AddBackLogTask(BackLogCommon setup);
        List<BackLogCommon> GetById(string ID);
        DbResponse DeleteTask(BackLogCommon common);
    }
}
