using System.Collections.Generic;
using MVCERP.Shared.Common;
using System.Data;
using iSolutionLife.Shared.Common;

namespace MVCERP.Repository.Repository.User
{
   public interface IUserRepository
    {
       DbResponse ManageUse(UserCommon setup);
       UserMenuFunctions GetMenuByUser(string User);

       List<UserCommon> GetUserList(string User, string UserId);

       object GetUserRole(string User, string UserId);

       DbResponse AssignUserRole(string User, string Username, string RoleList);
       DbResponse Changeuserpassword(string User,string pwd, string id);

       UserCommon UserLogin(LoginCommon login);

       List<UserCommon> GetAllList(string User, string Search, int Pagesize);

       DataSet GetDashBoardDetail(string User);
       DataSet GetDashBoardChartDetail(string User);

       DbResponse ChangePassword(ChangePassword model);
        List<UserCommon> GetGridList(GridParam gridParam);
    }
}
