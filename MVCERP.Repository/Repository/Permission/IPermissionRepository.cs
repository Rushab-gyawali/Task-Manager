﻿using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Permission
{
     public interface IPermissionRepository
    {
        List<PermissionCommon> ListPermission();
        DbResponse Manage(PermissionCommon common);

        List<PermissionCommon> GetById(string ID);
        DbResponse Delete(int ID);
        List<PermissionCommon> MenuList();
        List<PermissionCommon> GetMenuByUser(string User);
        DbResponse GetMenuPermission(PermissionCommon common, string user);
    }
}
