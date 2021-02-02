using MVCERP.Shared.Common;

namespace MVCERP.Repository.Repository.Error
{
    public interface IErrorRepo
    {
        ErrorLogsCommon Details(string id,string User);
    }
}
