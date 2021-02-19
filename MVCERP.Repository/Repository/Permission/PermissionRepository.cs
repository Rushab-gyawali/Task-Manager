using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Permission
{
    public class PermissionRepository : IPermissionRepository
    {
        RepositoryDao dao;
        public DbResponse Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<PermissionCommon> GetById(string ID)
        {
            throw new NotImplementedException();
        }

        public List<PermissionCommon> ListPermission()
        {
            throw new NotImplementedException();
        }

        public DbResponse Manage(PermissionCommon common)
        {
            throw new NotImplementedException();
        }

        public List<PermissionCommon> RolesList(string status, string user)
        {
            var list = new List<PermissionCommon>();
            try
            {
                var sql = "EXEC PROC_RoleList ";
                sql += "@Flag = 'ListRoles'";
                sql += ",@Roles = " + dao.FilterString(user);
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var commonrole = new PermissionCommon()
                        {
                            RoleId = Convert.ToInt32(item["RowId"]),
                            RoleName = item["RoleName"].ToString(),
                        };
                        sn++;
                        list.Add(commonrole);
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
