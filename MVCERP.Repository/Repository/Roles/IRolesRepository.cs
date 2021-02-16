using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Roles
{
    public interface IRolesRepository
    {
        DbResponse Manage(RolesCommon setup);
        DbResponse AssignRoles(string user, string id, string ViewId, string addId, string DeleteId);

        DbResponse AssignRoles(string user, string id, string functionRole);

    }
}
