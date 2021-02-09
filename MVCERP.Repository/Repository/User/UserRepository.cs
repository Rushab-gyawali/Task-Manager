using System;
using System.Collections.Generic;
using System.Data;
using iSolutionLife.Shared.Common;
using MVCERP.Shared.Common;

namespace MVCERP.Repository.Repository.User
{
    public class UserRepository : IUserRepository
    {
        RepositoryDao dao;
        public UserRepository() {
            dao = new RepositoryDao();
        }
        public DbResponse ManageUse(UserCommon setup)
        {
            var sql = "exec proc_tblUsers ";
            sql += "@FLAG = " + dao.FilterString((setup.UniqueId > 0 ? "U" : "I"));
            sql += ",@rowId = " + dao.FilterString(setup.UniqueId.ToString());
            sql += ",@UserId = " + dao.FilterString(setup.UserID);
            sql += ",@User = " + dao.FilterString(setup.User);
            sql += ",@UserPwd = " + dao.FilterString(setup.UserPwd);
            sql += ",@FullName = " + dao.FilterString(setup.FullName);
            sql += ",@Email = " + dao.FilterString(setup.Email);
            sql += ",@PhoneNo = " + dao.FilterString(setup.PhoneNo);
            sql += ",@Branch = " + dao.FilterString(setup.Branch.ToString());
            sql += ",@DepartmentId = " + dao.FilterString(setup.DepartmentId.ToString());
            sql += ",@DesignationId = " + dao.FilterString(setup.DesignationId.ToString());
            sql += ",@IsAdminUser = " + dao.FilterString(setup.IsAdminUser);
            sql += ",@IsActive = " + dao.FilterString(setup.IsActive);
            sql += ",@AllowApproveDate = " + setup.AllowApproveDate;
            sql += ",@AllowBackDate = " + setup.AllowBackDate;
            return dao.ParseDbResponse(sql);
        }

        public UserMenuFunctions GetMenuByUser(string User)
        {
            var sql = "exec proc_GetMenus 'MENU' ";
            sql += " ," + dao.FilterString(User);

            var ds = dao.ExecuteDataset(sql);

            var func = new UserMenuFunctions();

            func.menu = ds.Tables[0].DataTableToList<MenuCommon>();
            func.function = ds.Tables[1].DataTableToList<UserFunctionId>();

            return func;
            //var dt = ds.Tables[0];
            //var funcDT = ds.Tables[1];
            //var list = new List<MenuCommon>();
            //if (null != dt)
            //{
            //    foreach (DataRow item in dt.Rows)
            //    {
            //        var m = new MenuCommon
            //        {
            //            menuName = item["menuName"].ToString(),
            //            linkPage = item["Url"].ToString(),
            //            menuGroup = item["MenuGroup"].ToString(),
            //            parentGroup = item["ParentGroup"].ToString(),
            //            Class = item["Class"].ToString()
            //        };
            //        list.Add(m);
            //    }
            //}
            
            //return list;
        }


        public List<UserCommon> GetUserList(string User, string UserId)
        
        {
            var sql = "EXEC proc_tblUsers ";
            sql += "@FLAG = 'S'";
            sql += ",@User = " + dao.FilterString(User);
            sql += ",@UserId = " + dao.FilterString(UserId);
            
            var dt = dao.ExecuteDataTable(sql);
            var list = new List<UserCommon>();
            if (null != dt)
            {
                int sn = 1;
                foreach (DataRow item in dt.Rows)
                {
                    var common = new UserCommon
                    {
                        
                        UniqueId = Convert.ToInt32(item["RowId"]),
                        UserID = item["UserID"].ToString(),
                        FullName = item["FullName"].ToString(),
                        Email = item["Email"].ToString(),
                        PhoneNo = item["PhoneNo"].ToString(),
                        Branch = Convert.ToInt32(item["Branch"].ToString()),
                        DesignationId = Convert.ToInt32(item["DesignationId"].ToString()),
                        DepartmentId = Convert.ToInt32(item["DepartmentId"].ToString()),
                       
                        IsActive = item["IsActive"].ToString(),
                        AllowApproveDate = Convert.ToBoolean(item["CanApproveTransaction"]),
                        AllowBackDate = Convert.ToBoolean(item["AllowBackDate"]),
                        User = item["CreatedBy"].ToString(),
                        CreatedDate = item["CreatedDate"].ToString()
                        };
                    sn++;
                    list.Add(common);
                }
            }

            return list;
        }

        public object GetUserRole(string User, string UserId)
        {
            var sql = "EXEC proc_Role @FLAG ='GetUserRole'";
            sql += ",@User = " + dao.FilterString(User);
            sql += ",@id = " + dao.FilterString(UserId);
            var data = new object();
            var dt = dao.ExecuteDataTable(sql);
            if (null !=dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    data = (new { userName = item["userName"].ToString(), roleId = item["roleId"].ToString() });
                }
            }
            return data;
        }
      
        public DbResponse AssignUserRole(string User, string Username, string RoleList)
        {
            var sql = "EXEC proc_Role @FLAG ='AssignUserRole'";
            sql += ",@User = " + dao.FilterString(User);
            sql += ",@RoleName = " + dao.FilterString(Username);
            sql += ",@id = " + dao.FilterString(RoleList);

            return dao.ParseDbResponse(sql);
        }

        public DbResponse Changeuserpassword( string username,string pwd,string id)
        {
            var sql = "EXEC proc_tblUsers @FLAG ='changeuserpassword'";
            sql += ",@User = " + dao.FilterString(username);
            sql += ",@UserPwd=" + dao.FilterString(pwd);
           sql += ",@UserId = " + dao.FilterString(id);
            return dao.ParseDbResponse(sql);
        }

        public UserCommon UserLogin(LoginCommon login)
        {
            var sql = "EXEC proc_UserLogin @FLAG ='login'";

            sql += ",@UserName = " + dao.FilterString(login.UserName);
            sql += ",@Password = " + dao.FilterString(login.Password);
            sql += ",@Ip = " + dao.FilterString(login.Ip);
            sql += ",@BrowserInfo = " + dao.FilterString(login.BrowserInfo);





            var dr = dao.ExecuteDataRow(sql);
          var model = new UserCommon();
            if (null !=dr)
            {
                model.Code = dr["CODE"].ToString();
                model.Msg = dr["msg"].ToString();
                if (model.Code == "0")
                {
                    return model;
                }               
              
            }
            return model;
        }

        public List<UserCommon> GetAllList(string User, string Search, int Pagesize)
        {
            var sql = "EXEC proc_tblUsers ";
            sql += "@FLAG = 'A'" ;
            sql += ",@User = " + dao.FilterString(User);
            sql += ",@Search = " + dao.FilterString(Search);
            sql += ",@Pagesize = " + dao.FilterString(Pagesize.ToString());
            var dt = dao.ExecuteDataTable(sql);
            var list = new List<UserCommon>();
            if (null != dt)
            {
                int sn = 1;
                foreach (DataRow item in dt.Rows)
                {
                    var common = new UserCommon
                    {
                        
                        UniqueId = Convert.ToInt32(item["RowId"]),
                        UserID = item["UserID"].ToString(),
                        FullName = item["FullName"].ToString(),
                        Email = item["Email"].ToString(),
                        PhoneNo = item["PhoneNo"].ToString(),
                        BranchName = item["BranchName"].ToString(),
                        DesignationName = item["DesignationName"].ToString(),
                        DepartmentName = item["DepartmentName"].ToString(),
                        IsActive = item["IsActive"].ToString(),
                        User = item["CreatedBy"].ToString(),
                        CreatedDate = item["CreatedDate"].ToString()
                    };
                    sn++;
                    list.Add(common);
                }
            }

            return list;
        }


        public DataSet GetDashBoardDetail(string User)
        {
            var sql = "EXEC proc_dashboard ";
            sql += "@FLAG = 'dashboardDetails'";
            sql += ",@User = " + dao.FilterString(User);
            var ds = dao.ExecuteDataset(sql);
            return ds;
        }
        public DataSet GetDashBoardChartDetail(string User)
        {
            var sql = "EXEC proc_dashboard ";
            sql += "@FLAG = 'dashboardChart'";
            sql += ",@User = " + dao.FilterString(User);
            var ds = dao.ExecuteDataset(sql);
            return ds;
        }


        public DbResponse ChangePassword(ChangePassword model)
        {
            var sql = "EXEC proc_tblUsers ";
            sql += "@FLAG = 'ChangePwd'";
            sql += ",@User = " + dao.FilterString(model.User);
            sql += ",@OldPwd = " + dao.FilterString(model.OldPassword);
            sql += ",@UserPwd = " + dao.FilterString(model.NewPassword);
            sql += ",@UserID = " + dao.FilterString(model.UserName);



            return dao.ParseDbResponse(sql);
        }

        public List<UserCommon> GetGridList(GridParam gridParam)
        {
            var list = new List<UserCommon>();
            try
            {
                var sql = "EXEC proc_tblUsers ";
                sql += " @FLAG = " + dao.FilterString("A");
                sql += ",@User = " + dao.FilterString(gridParam.UserName);
                sql += ",@Search = " + dao.FilterString(gridParam.Search);
                sql += ",@DisplayLength = " + dao.FilterString(gridParam.DisplayLength.ToString());
                sql += ",@DisplayStart = " + dao.FilterString(gridParam.DisplayStart.ToString());
                sql += ",@SortDir = " + dao.FilterString(gridParam.SortDir);
                sql += ",@SortCol = " + dao.FilterString(gridParam.SortCol.ToString());
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var common = new UserCommon()
                        {


                            UniqueId = Convert.ToInt32(item["RowId"]),
                            UserID = item["UserID"].ToString(),
                            FullName = item["FullName"].ToString(),
                            Email = item["Email"].ToString(),
                            PhoneNo = item["PhoneNo"].ToString(),
                            BranchName = item["BranchName"].ToString(),
                            DesignationName = item["DesignationName"].ToString(),
                            DepartmentName = item["DepartmentName"].ToString(),
                            IsActive = item["IsActive"].ToString(),
                            User = item["CreatedBy"].ToString(),
                            CreatedDate = item["CreatedDate"].ToString(),
                            FilterCount = item["FilterCount"].ToString()
                        };
                        sn++;
                        list.Add(common);
                    }
                }
                return list;
            }

            catch (Exception e)
            {
                return list;
            }
        }

    }
}
