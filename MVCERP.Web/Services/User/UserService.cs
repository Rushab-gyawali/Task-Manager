using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCERP.Business.Business.User;
using MVCERP.Shared.Common;
using MVCERP.Web.Services.UserService;

namespace MVCERP.Web.Services.UserService
{

    public class UserService : IUserService
    {
        IUserBusiness buss;
        public UserService(IUserBusiness _buss) {
            buss = _buss;
        }
        public Shared.Common.DbResponse ManageUse(Shared.Common.UserCommon setup)
        {
            return buss.ManageUse(setup);
        }

        public UserMenuFunctions GetMenuByUser(string User)
        {
            return buss.GetMenuByUser(User);
        }

        public List<Shared.Common.UserCommon> GetUserList(string User, string UserId)
        {
            return buss.GetUserList(User, UserId);
        }

    }
}