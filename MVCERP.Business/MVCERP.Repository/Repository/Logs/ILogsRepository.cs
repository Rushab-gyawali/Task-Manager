using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Logs
{
    public interface ILogsRepository
    {
       DbResponse UnAuthorizedLog(ErrorLogsCommon log);
    }
}
