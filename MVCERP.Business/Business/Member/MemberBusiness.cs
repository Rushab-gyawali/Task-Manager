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

        public DbResponse DeleteUser(int ID)
        {
            return repo.DeleteUser(ID);
        }

        public List<MemberCommon> GetById(string ID)
        {
            return repo.GetById(ID);
        }

        public List<MemberCommon> ListUsers()
        {
            return repo.ListUsers();
        }
    }
}
