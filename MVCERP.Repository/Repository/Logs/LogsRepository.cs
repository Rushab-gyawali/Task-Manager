using MVCERP.Repository.Repository.Logs;

namespace MVCERP.Repository.Repository.Logs
{
    public class LogsRepository : ILogsRepository
    {
        RepositoryDao dao;
        public LogsRepository()
        {
            dao = new RepositoryDao();
        }
        public MVCERP.Shared.Common.DbResponse UnAuthorizedLog(MVCERP.Shared.Common.ErrorLogsCommon log)
        {
            var sql = "EXEC Proc_tblFraudLogs ";
            sql += " @FLAG = " + dao.FilterString("I");
            sql += ",@User = " + dao.FilterString(log.User);
          //  sql += ",@FunctionId = " + dao.FilterString(log.ErrorPage);
            sql += ",@LogType = " + dao.FilterString(log.Action);
            sql += ",@IpAddress = " + dao.FilterString(log.ErrorMsg);
            sql += ",@Browser = " + dao.FilterString(log.ErrorDetail);
            return dao.ParseDbResponse(sql);
        }
    }
}
