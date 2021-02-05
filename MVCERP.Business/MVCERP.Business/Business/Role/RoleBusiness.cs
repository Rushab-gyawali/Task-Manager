using System.Collections.Generic;
using MVCERP.Repository.Repository.Role;
using MVCERP.Business.Business.Role;
using MVCERP.Shared.Common;

namespace MVCERP.Business.Business.Role
{
    public class RoleBusiness : IRoleBusiness
    {
        IRoleRepository repo;
        public RoleBusiness(RoleRepository _repo)
        {
            repo = _repo;
        }
        public DbResponse Manage(RoleCommon setup)
        {
            return repo.Manage(setup);
        }

        public List<RoleCommon> GetList(string User, string Search, int Pagesize)
        {
            return repo.GetList(User,Search,Pagesize);
        }


        public List<RoleDetails> GetAssignedList(string User, string id)
        {
            return repo.GetAssignedList(User, id);
        }


        public DbResponse AssignRole(string user, string id,string ViewId,string addId,string DeleteId)
        {
            return repo.AssignRole(user, id, ViewId, addId, DeleteId);
        }


        public List<RoleCommon> GetListById(string User, string id)
        {
            return repo.GetListById(User,id);
        }


        public List<RoleDetails> GetAssignedList(string User, int id)
        {
            return repo.GetAssignedList(User,id);
        }

        public DbResponse AssignRole(string user, string id, string functionRole)
        {
            return repo.GetAssignedList(user, id, functionRole);
        }
    }
}
