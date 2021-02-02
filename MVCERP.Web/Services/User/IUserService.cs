using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Web.Services.UserService
{
   public interface IUserService
    {
        DbResponse ManageUse(UserCommon setup);
        UserMenuFunctions GetMenuByUser(string User);

        List<UserCommon> GetUserList(string User, string UserId);
    }
}
