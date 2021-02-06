using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.Role
{
   public interface IRoleBusiness
    {
       DbResponse Manage(RoleCommon setup);
        List<RoleCommon> GetList(string User,string Search, int Pagesize);

        List<RoleDetails> GetAssignedList(string User, string id);

        List<RoleDetails> GetAssignedList(string User, int id);
        DbResponse AssignRole(string user, string id, string ViewId,string addId,string DeleteId);

        DbResponse AssignRole(string user, string id, string functionRole);
        List<RoleCommon> GetListById(string User, string id);
    }
}
