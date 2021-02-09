using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Member
{
        public interface IMemberRepository
        {
            List<MemberCommon> ListUsers();
        DbResponse AddUser(MemberCommon setup);
        List<MemberCommon> GetById(string ID);
    }

    
}
