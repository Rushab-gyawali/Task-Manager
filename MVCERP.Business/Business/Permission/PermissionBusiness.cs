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
        public PermissionBusiness(IPermissionRepository _repo)
        {
            repo = _repo;
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

        DbResponse IPermissionBusiness.Manage(PermissionCommon common)
        {
            return repo.Manage(common);
        }

        public List<PermissionCommon> MenuList()
        {
            return repo.MenuList();
        }

        public List<PermissionCommon> GetMenuByUser(string User)
        {
            return repo.GetMenuByUser(User);
        }

        public DbResponse GetMenuPermission(PermissionCommon common, string user)
        {
            return repo.GetMenuPermission(common,user);
        }
    }
}
