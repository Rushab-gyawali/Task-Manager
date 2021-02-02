using MVCERP.Repository.Repository.Logs;
using MVCERP.Business.Business.Logs;
using MVCERP.Repository.Repository.Logs;

namespace MVCERP.Business.Business.Logs
{
    public class LogsBusiness : ILogsBusiness
    {
        ILogsRepository repo=new LogsRepository();
        public MVCERP.Shared.Common.DbResponse UnAuthorizedLog(MVCERP.Shared.Common.ErrorLogsCommon log)
        {
            return repo.UnAuthorizedLog(log);
        }
    }
}
