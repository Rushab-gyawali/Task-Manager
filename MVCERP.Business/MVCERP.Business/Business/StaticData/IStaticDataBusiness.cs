using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.StaticData
{
   public interface IStaticDataBusiness
    {
       List<StaticDataCommon> GetList(string User, string Id, string Search, int Pagesize);

       List<StaticDataCommon> GetSubList(string User, string Id);
       DbResponse Manage(StaticDataCommon model);

       DbResponse Delete(string User, int id);
       DbResponse InsertErrorLog(ErrorLogsCommon log);

       DbResponse EOD(string User);

       DbResponse BOD(string User, string Date);
    }
}
