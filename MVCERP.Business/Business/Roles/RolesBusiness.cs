using MVCERP.Repository.Repository.Role;
using MVCERP.Repository.Repository.Roles;
using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.Roles
{
    public class RolesBusiness : IRolesBusiness
    {
        IRolesRepository repo;
        public RolesBusiness(IRolesRepository _repo)
        {
            repo = _repo;
        }
        public DbResponse AssignRoles(string user, string id, string ViewId, string addId, string DeleteId)
        {
            return repo.AssignRoles(user, id, ViewId, addId, DeleteId);
        }

        public DbResponse AssignRoles(string user, string id, string functionRole)
        {
            return repo.AssignRoles(user, id, functionRole);
        }

        public DbResponse Manage(RolesCommon setup)
        {
            return repo.Manage(setup);
        }
    }
}
