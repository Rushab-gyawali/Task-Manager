using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.Permission
{
    public interface IPermissionBusiness
    {
        List<PermissionCommon> ListPermission();
        DbResponse Manage(PermissionCommon common);

        List<PermissionCommon> GetById(string ID);
        DbResponse Delete(int ID);

        List<PermissionCommon> RolesList(string status, string user);

    }
}
