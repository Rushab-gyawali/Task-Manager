using MVCERP.Repository.Repository.Error;

namespace MVCERP.Business.Business.Error
{
    public class ErrorBusiness : IErrorBusiness
    {
        IErrorRepo repo;
        public ErrorBusiness(ErrorRepo _repo)
        {
            repo = _repo;
        }
        public Shared.Common.ErrorLogsCommon Details(string id, string User)
        {
            return repo.Details(id,User);
        }
    }
}
