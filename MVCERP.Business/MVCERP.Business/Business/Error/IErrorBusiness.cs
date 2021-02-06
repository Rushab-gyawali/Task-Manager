using MVCERP.Shared.Common;

namespace MVCERP.Business.Business.Error
{
    public interface IErrorBusiness
    {
        ErrorLogsCommon Details(string id,string User);
    }
}
