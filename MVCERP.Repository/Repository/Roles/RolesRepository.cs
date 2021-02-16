using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Roles
{
    public class RolesRepository : IRolesRepository
    {
        RepositoryDao dao;
        public RolesRepository()
        {
            dao = new RepositoryDao();
        }
        public DbResponse AssignRoles(string user, string id, string ViewId, string addId, string DeleteId)
        {
            var sql = "EXEC proc_Role @FLAG = 'AssignRole'";
            sql += ",@User = " + dao.FilterString(user);
            sql += ",@Id = " + dao.FilterString(id.ToString());
            sql += ",@ViewId = " + dao.FilterString(ViewId);
            sql += ",@addId = " + dao.FilterString(addId);
            sql += ",@DeleteId = " + dao.FilterString(DeleteId);

            return dao.ParseDbResponse(sql);
        }

        public DbResponse AssignRoles(string user, string id, string functionRole)
        {
            throw new NotImplementedException();
        }




        public DbResponse Manage(RolesCommon setup)
        {
            var sql = "exec proc_Role ";
            sql += "@FLAG = " + dao.FilterString((setup.Id > 0 ? "U" : "I"));
            sql += ",@User = " + dao.FilterString(setup.User);
            sql += ",@Id = " + dao.FilterString(setup.Id.ToString());
            sql += ",@RoleName = " + dao.FilterString(setup.RoleName);
            sql += ",@IsActive = " + setup.IsActive;
            return dao.ParseDbResponse(sql);
        }
    }
}
