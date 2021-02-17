using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.SprintBusiness
{
   public interface ISprintBusiness
    {
        List<SprintCommon> GetSprints();
        List<BacklogCommon> GetBacklogs();
        DbResponse SprintAndBacklog(SprintCommon sprint);
        SprintCommon GetById(string SprintId);
    }
}
