using System.Collections.Generic;
using MVCERP.Repository.Repository.User;
using MVCERP.Shared.Common;
using System.Data;
using iSolutionLife.Shared.Common;

namespace MVCERP.Business.Business.User
{
    public class UserBusiness : IUserBusiness
    {
        IUserRepository repo;
        public UserBusiness(UserRepository _repo) {
            repo = _repo;
        }
        public DbResponse ManageUse(UserCommon setup)
        {
            return repo.ManageUse(setup);
        }

        public UserMenuFunctions GetMenuByUser(string User)
        {
            return repo.GetMenuByUser(User);
        }

        public List<UserCommon> GetUserList(string User, string UserId)
        {
            return repo.GetUserList(User,UserId);
        }


        public object GetUserRole(string User, string UserId)
        {
            return repo.GetUserRole(User,UserId);
        }

        public DbResponse AssignUserRole(string User, string Username, string RoleList)
        {
            return repo.AssignUserRole(User,Username,RoleList);
        }
        public DbResponse Changeuserpassword( string username,string pwd,string id)
        {
            return repo.Changeuserpassword(username,pwd,id);
        }
        public UserCommon UserLogin(LoginCommon login)
        {
            return repo.UserLogin(login);
        }


        public List<UserCommon> GetAllList(string User, string Search, int Pagesize)
        {
            return repo.GetAllList(User, Search,Pagesize);
        }

        public DataSet GetDashBoardDetail(string User)
        {
            return repo.GetDashBoardDetail(User);
        }
        public DataSet GetDashBoardChartDetail(string User)
        {
            return repo.GetDashBoardChartDetail(User);
        }

        public DbResponse ChangePassword(ChangePassword model)
        {
            return repo.ChangePassword(model);
        }
        public List<UserCommon> GetGridList(GridParam gridParam)
        {
            return repo.GetGridList(gridParam);
        }
    }
}
