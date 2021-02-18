using MVCERP.Repository.Repository.Member;
using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.Member
{
    public class MemberBusiness : IMemberBusiness
    {

        IMemberRepository repo;
        public MemberBusiness(IMemberRepository _repo)
        {
            repo = _repo;
        }

        public DbResponse AddUsers(MemberCommon setup)
        {
            return repo.AddUser(setup);
        }

        public DbResponse AssignUserRole(MemberCommon model)
        {
            return repo.AssignUserRole(model);
        }

        public DbResponse ChangePassword(ChangePasswordCommon common)
        {
            return repo.ChangePassword(common);
        }

        public DbResponse DeleteUser(int ID)
        {
            return repo.DeleteUser(ID);
        }

        public List<MemberCommon> GetById(string ID)
        {
            return repo.GetById(ID);
        }

        public object GetUserRole(string User, string UserId)
        {
            return repo.GetUserRole(User, UserId);
        }

        public List<MemberCommon> ListUsers()
        {
            return repo.ListUsers();
        }

        public List<MemberCommon> ListUsersProfile(MemberCommon common)
        {
            return repo.ListUsersProfile(common);
        }
    }
}
