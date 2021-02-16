using iSolutionLife.Shared.Common;
using MVCERP.Shared.Common;
using System.Collections.Generic;
using System.Data;

namespace MVCERP.Business.Business.User
{
    public interface IUserBusiness
    {
        DbResponse ManageUse(UserCommon setup);
        UserMenuFunctions GetMenuByUser(string User);

        List<UserCommon> GetUserList(string User, string UserId);
        object GetUserRole(string User, string UserId);

        DbResponse AssignUserRole(string User, string Username, string RoleList);
        DbResponse Changeuserpassword(string username,string pwd,string id);

        MemberCommon UserLogin(LoginCommon login);

        List<UserCommon> GetAllList(string User, string Search, int Pagesize);


        DataSet GetDashBoardDetail(string User);
        DataSet GetDashBoardChartDetail(string User);


        DbResponse ChangePassword(ChangePassword model);
        List<UserCommon> GetGridList(GridParam gridParam);

    }
}
