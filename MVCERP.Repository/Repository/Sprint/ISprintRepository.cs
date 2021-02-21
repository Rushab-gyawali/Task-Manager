using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Sprint
{
   public interface ISprintRepository
    {
        List<SprintCommon> GetSprints();
        List<BackLogCommon> GetBacklogs();
        DbResponse GetSprintAndBacklog(SprintCommon sprint);
        SprintCommon GetById(string SprintId);
    }
}
