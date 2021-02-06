
namespace iSolutionLife.Web.Services.Utilities
{
    public class Utilities : IUtilities
    {
        IUtilitiesBusiness util;
        public Utilities(IUtilitiesBusiness _util)
        {
            util = _util;
        }
        public Shared.Common.DbResponse SendMail(string To, string Cc, string Subject, string Body)
        {
            throw new NotImplementedException();
        }
    }
}