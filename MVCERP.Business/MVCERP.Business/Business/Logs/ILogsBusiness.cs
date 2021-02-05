using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.Logs
{
   public interface ILogsBusiness
    {
       DbResponse UnAuthorizedLog(ErrorLogsCommon log);
    }
}
