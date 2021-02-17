using MVCERP.Repository.Repository.Sprint;
using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.SprintBusiness
{
    public class SprintBusiness : ISprintBusiness
    {
        ISprintRepository _repo;
        public SprintBusiness(ISprintRepository repo)
        {
            _repo = repo;
        }

        public List<BacklogCommon> GetBacklogs()
        {
            return _repo.GetBacklogs();
        }

        public List<SprintCommon> GetSprints()
        {
            return _repo.GetSprints();
        }

        public DbResponse SprintAndBacklog(SprintCommon sprint)
        {
            return _repo.GetSprintAndBacklog(sprint);
        }


        SprintCommon ISprintBusiness.GetById(string SprintId)
        {
            return _repo.GetById(SprintId);
        }
    }
}
