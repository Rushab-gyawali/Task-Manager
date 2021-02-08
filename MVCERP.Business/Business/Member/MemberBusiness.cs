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

        public List<MemberCommon> ListUsers()
        {
            return repo.ListUsers();
        }
    }
}
