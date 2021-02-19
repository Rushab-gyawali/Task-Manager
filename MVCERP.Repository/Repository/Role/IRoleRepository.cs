using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Role
{
   public  interface IRoleRepository
    {
        DbResponse Manage(RoleCommon setup);
        List<RoleCommon> GetList();

        List<RoleDetails> GetAssignedList(string User, string id);
        DbResponse AssignRole(string user, string id, string ViewId,string addId,string DeleteId);

        List<RoleCommon> GetListById(string User, string id);

        List<RoleDetails> GetAssignedList(string User, int id);

        DbResponse GetAssignedList(string user, string id, string functionRole);
    }
}
