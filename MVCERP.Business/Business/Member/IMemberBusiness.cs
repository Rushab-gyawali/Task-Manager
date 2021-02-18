using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.Member
{
   public interface IMemberBusiness
    {
        List<MemberCommon> ListUsers();
        List<MemberCommon> ListUsersProfile(MemberCommon common);
        DbResponse ChangePassword(ChangePasswordCommon common);
        DbResponse AddUsers(MemberCommon setup);
        List<MemberCommon> GetById(string ID);
        DbResponse DeleteUser(int ID);
    }
}
