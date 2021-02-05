using System.Collections.Generic;
using MVCERP.Repository.Repository.StaticData;
using MVCERP.Business.Business.StaticData;
using MVCERP.Repository.Repository.StaticData;

namespace MVCERP.Business.Business.StaticData
{
    public class StaticDataBusiness : IStaticDataBusiness
    {
        IStaticDataRepository repo;
        public StaticDataBusiness(StaticDataRepository _repo)
        {
            repo = _repo;
        }

        public StaticDataBusiness()
        {
            // TODO: Complete member initialization
        }
        public List<MVCERP.Shared.Common.StaticDataCommon> GetList(string User, string Id, string Search, int Pagesize)
        {
            return repo.GetList(User, Id,Search,Pagesize);
        }
        public List<MVCERP.Shared.Common.StaticDataCommon> GetSubList(string User, string Id)
        {
            return repo.GetSubList(User, Id);
        }
        

        public MVCERP.Shared.Common.DbResponse Manage(MVCERP.Shared.Common.StaticDataCommon model)
        {
            return repo.Manage(model);
        }


        public MVCERP.Shared.Common.DbResponse Delete(string User, int id)
        {
            return repo.Delete(User, id);
        }
        public MVCERP.Shared.Common.DbResponse InsertErrorLog(MVCERP.Shared.Common.ErrorLogsCommon log)
        {
            if (null==repo)
                repo = new StaticDataRepository();
            
            return repo.InsertErrorLog(log);
        }

        public Shared.Common.DbResponse EOD(string User)
        {
            return repo.EOD(User);
        }


        public Shared.Common.DbResponse BOD(string User, string Date)
        {
            return repo.BOD(User, Date);
        }
    }
}
