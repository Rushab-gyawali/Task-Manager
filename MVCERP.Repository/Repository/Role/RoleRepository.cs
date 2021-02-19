using System;
using System.Collections.Generic;
using MVCERP.Repository.Repository.Role;
using MVCERP.Shared.Common;

namespace MVCERP.Repository.Repository.Role
{
    public class RoleRepository : IRoleRepository
    {
         RepositoryDao dao;
         public RoleRepository()
         {
            dao = new RepositoryDao();
        }
        public MVCERP.Shared.Common.DbResponse Manage(MVCERP.Shared.Common.RoleCommon setup)
        {
            var sql = "exec proc_Role ";
            sql += "@FLAG = " + dao.FilterString((setup.Id > 0 ? "U" : "I"));
            sql += ",@User = " + dao.FilterString(setup.User);
            sql += ",@Id = " + dao.FilterString(setup.Id.ToString());
            sql += ",@RoleName = " + dao.FilterString(setup.RoleName);
            sql += ",@IsActive = " + setup.IsActive;
            return dao.ParseDbResponse(sql);
        }

        public List<MVCERP.Shared.Common.RoleCommon> GetList()
        {
            var list = new List<RoleCommon>();
            try
            {
                var sql = "EXEC proc_Roles ";
                sql += "@FLAG = " + dao.FilterString("List");
                var dt = dao.ExecuteDataTable(sql);
                
                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var common = new RoleCommon()
                        {
                            Sno = sn.ToString(),
                            Id = Convert.ToInt32(item["RoleId"]),
                            RoleName = item["RoleName"].ToString(),
                        };
                        sn++;
                        list.Add(common);
                    }
                }
                return list;
            }

            catch (Exception e)
            {
                throw e;
            }
         
        }


        public List<RoleDetails> GetAssignedList(string User, string id)
        {
            var sql = "EXEC proc_Role @FLAG = 'getAssignList'";
            sql += ",@User = " + dao.FilterString(User);
            sql += ",@Id = " + dao.FilterString(id.ToString());

            var dt = dao.ExecuteDataTable(sql);
            var list = new List<RoleDetails>();
            if (null != dt)
            {
                int sn = 1;
                foreach (System.Data.DataRow item in dt.Rows)
                {
                    var common = new RoleDetails()
                    {
                        Sno = sn.ToString(),
                        menuGroup = item["menuGroup"].ToString(),
                        menuName = item["menuName"].ToString(),
                        parentFunctionId = item["parentFunctionId"].ToString(),
                        functionId = item["functionId"].ToString(),
                        functionName = item["functionName"].ToString(),
                        RoleId = Convert.ToInt32(item["RoleId"].ToString()),
                        hasChecked = item["hasChecked"].ToString(),
                        ParentGroup = item["ParentGroup"].ToString(),
                        groupPosition = item["groupPosition"].ToString()
                    };
                    list.Add(common);
                }
            }

            return list;

        }

        public DbResponse AssignRole(string user, string id, string ViewId,string addId,string DeleteId)
        {
            var sql = "EXEC proc_Role @FLAG = 'AssignRole'";
            sql += ",@User = " + dao.FilterString(user);
            sql += ",@Id = " + dao.FilterString(id.ToString());
            sql += ",@ViewId = " + dao.FilterString(ViewId);
            sql += ",@addId = " + dao.FilterString(addId);
            sql += ",@DeleteId = " + dao.FilterString(DeleteId);

            return dao.ParseDbResponse(sql);
        }


        public List<RoleCommon> GetListById(string User, string id)
        {
            var sql = "EXEC proc_Role ";
            sql += "@FLAG = " + dao.FilterString("S");
            sql += ",@User = " + dao.FilterString(User);
            sql += ",@Id = " + dao.FilterString(id.ToString());

            var dt = dao.ExecuteDataTable(sql);
            var list = new List<RoleCommon>();
            if (null != dt)
            {
                int sn = 1;
                foreach (System.Data.DataRow item in dt.Rows)
                {
                    var common = new RoleCommon()
                    {
                        
                        Id = Convert.ToInt32(item["RoleId"]),
                        RoleName = item["RoleName"].ToString(),
                        IsActive = Convert.ToBoolean(item["IsActive"]),
                        User = item["CreatedBy"].ToString(),
                        CreatedDate = item["CreatedDate"].ToString()
                    };
                    sn++;
                    list.Add(common);
                }
            }

            return list;
        }


        public List<RoleDetails> GetAssignedList(string User, int id)
        {
            var sql = "EXEC proc_Role @FLAG = 'getAssignList'";
            sql += ",@User = " + dao.FilterString(User);
            sql += ",@Id = " + dao.FilterString(id.ToString());

            var dt = dao.ExecuteDataTable(sql);
            var list = new List<RoleDetails>();
            if (null != dt)
            {
                int sn = 1;
                foreach (System.Data.DataRow item in dt.Rows)
                {
                    var common = new RoleDetails()
                    {
                        Sno = item["Sno"].ToString(),
                        menuGroup = item["menuGroup"].ToString(),
                        menuName = item["menuName"].ToString(),
                        parentFunctionId = item["parentFunctionId"].ToString(),
                        functionId = item["functionId"].ToString(),
                        functionName = item["functionName"].ToString(),
                        RoleId = Convert.ToInt32(item["RoleId"].ToString()),
                        hasChecked = item["hasChecked"].ToString(),
                        ParentGroup = item["ParentGroup"].ToString()
                    };
                    list.Add(common);
                }
            }

            return list;
        }

        public DbResponse GetAssignedList(string user, string id, string functionRole)
        {
            var sql = "EXEC proc_Role @FLAG = 'AssignRole'";
            sql += ",@User = " + dao.FilterString(user);
            sql += ",@Id = " + dao.FilterString(id.ToString());
            sql += ",@functionRole = " + dao.FilterString(functionRole);

            return dao.ParseDbResponse(sql);
        }
    }
}
