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
        public PermissionRepository()
        {
            dao = new RepositoryDao();
        }

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


        public List<PermissionCommon> MenuList()
        {
            var list = new List<PermissionCommon>();
            try
            {
                var sql = "EXEC PROC_PERMISSION ";
                sql += "@Flag = 'List'";
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var commonrole = new PermissionCommon()
                        {
                            ParentMenu = item["ParentMenu"].ToString(),
                            Menu = item["Menu"].ToString(),
                            MenuId = Convert.ToInt32(item["MenuId"]),
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
        public List<PermissionCommon> GetMenuByUser(string User)
        {
            var list = new List<PermissionCommon>();
            try
            {
                var sql = "EXEC PROC_PERMISSION ";
                sql += "@Flag = 'UserMenu'";
                sql += ",@User = " + User;
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var commonrole = new PermissionCommon()
                        {
                            ParentMenu = item["ParentMenu"].ToString(),
                            Menu = item["Menu"].ToString(),
                            URL = item["URL"].ToString(),
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

        public DbResponse GetMenuPermission(PermissionCommon common,string user)
        {
            var sql = "EXEC PROC_PERMISSION ";
            sql += "@Flag = 'AddPermissionRole'";
            sql += ",@FK_MenuId =" + common.MenuId;
            sql += ",@RoleName =" + dao.FilterString(common.RoleName);
            sql += ",@UserName =" + dao.FilterString(user);
            return dao.ParseDbResponse(sql);
        }
    }
}
