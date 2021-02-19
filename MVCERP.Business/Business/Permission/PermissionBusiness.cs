using MVCERP.Repository.Repository.Permission;
using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.Permission
{
    public class PermissionBusiness : IPermissionBusiness
    {

        IPermissionRepository repo;
        public PermissionBusiness (IPermissionRepository _repo)
        {
            _repo = repo;
        }
        public DbResponse Delete(int ID)
        {
          return repo.Delete(ID);
        }

        public List<PermissionCommon> GetById(string ID)
        {
            return repo.GetById(ID);
        }

        public List<PermissionCommon> ListPermission()
        {
            return repo.ListPermission();
        }

        public List<PermissionCommon> RolesList(string status, string user)
        {
            return repo.RolesList(status, user);
        }

        DbResponse IPermissionBusiness.Manage(PermissionCommon common)
        {
            return repo.Manage(common);
        }
    }
}
