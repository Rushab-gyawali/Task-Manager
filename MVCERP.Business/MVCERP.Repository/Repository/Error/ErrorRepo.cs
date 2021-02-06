using MVCERP.Shared.Common;

namespace MVCERP.Repository.Repository.Error
{
   public class ErrorRepo : IErrorRepo
    {
       RepositoryDao dao;
       public ErrorRepo()
        {
            dao = new RepositoryDao();
        }

        public ErrorLogsCommon Details(string id,string User)
        {
            var sql = "EXEC proc_tblErrors ";
            sql += " @FLAG = " + dao.FilterString("DETAIL");
            sql += ",@RowId = " + dao.FilterString(id);
            sql += ",@USER = " + dao.FilterString(User);
            var result=dao.ExecuteDataRow(sql);
            ErrorLogsCommon errorLogs = new ErrorLogsCommon()
            {
                ErrorPage = result["ErrorPage"].ToString(),
                ErrorMsg = result["ErrorMsg"].ToString(),
                ErrorDetail = result["ErrorDetail"].ToString(),
                CreatedBy = result["CreatedBy"].ToString(),
                CreatedDate = result["CreatedDate"].ToString()
            };
            return errorLogs;
        }

    }
}
