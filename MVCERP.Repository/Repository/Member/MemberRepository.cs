using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Member
{
    public class MemberRepository : IMemberRepository 
    {
        RepositoryDao dao;
        public MemberRepository()
        {
            dao = new RepositoryDao();
            
        }

   

        public DbResponse AddUser(MemberCommon setup)
        {
            var sql = "exec proc_tblUsers ";
            sql += "@Flag = " + dao.FilterString((setup.ID > 0 ? "Update" : "Insert"));
            sql += ",@FullName = " + dao.FilterString(setup.FullName);
            sql += ",@UserName = " + dao.FilterString(setup.UserName);
            sql += ",@Email = " + dao.FilterString(setup.Email);
            sql += ",@PhoneNo = " + dao.FilterString(setup.PhoneNo);
            sql += ",@Password = " + dao.FilterString(setup.Password);
            sql += ",@AdminRIght = " + dao.FilterString(setup.AdminRight.ToString());
            sql += ",@CreatedBy = " + dao.FilterString(setup.CreatedBy.ToString());
            if (setup.ID == 0)
            {
                return dao.ParseDbResponse(sql);
            }
            else
            {

                sql += ",@ID = " + dao.FilterString(setup.ID.ToString());
                return dao.ParseDbResponse(sql);
            }
           
        }

        public DbResponse ChangePassword(ChangePasswordCommon common)
        {
            var sql = "EXEC proc_tblUsers ";
            sql += "@FLAG = 'ChangePwd'";
            sql += ",@UserName = " + dao.FilterString(common.UserName);
            sql += ",@OldPwd = " + dao.FilterString(common.OldPassword);
            sql += ",@Password = " + dao.FilterString(common.NewPassword);
            sql += ",@ID = " + dao.FilterString(common.ID.ToString());
            return dao.ParseDbResponse(sql);
        }

        public DbResponse DeleteUser(int ID)
        {
            var sql = "exec proc_tblUsers ";
            sql += "@Flag = 'Delete'";
            sql += ",@ID  =" + ID;
            return dao.ParseDbResponse(sql);
        }

        public List<MemberCommon> GetById(string ID)
        {
            var list = new List<MemberCommon>();
            try
            {
                var sql = "Exec proc_tblUsers ";
                sql += "@Flag = 'GetById'";
                sql += ",@ID = " + dao.FilterString(ID);

                var dt = dao.ExecuteDataTable(sql);
                if (null != dt)
                {
                    int sn = 1;
                    foreach (DataRow item in dt.Rows)
                    {
                        var common = new MemberCommon
                        {
                            ID = Convert.ToInt32(item["ID"]),
                            FullName = item["FullName"].ToString(),
                            UserName = item["UserName"].ToString(),
                            Email = item["Email"].ToString(),
                            PhoneNo = item["PhoneNo"].ToString(),
                            Password = item["Password"].ToString(),
                            AdminRight = Convert.ToBoolean(item["AdminRight"]),


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

        public List<MemberCommon> ListUsers()
        {
            var list = new List<MemberCommon>();
            try
            {
                var sql = "EXEC proc_tblUsers ";
                sql += "@Flag = 'List'";
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var common = new MemberCommon()
                        {
                            ID = Convert.ToInt32(item["ID"]),
                            FullName = item["FullName"].ToString(),
                            UserName = item["UserName"].ToString(),
                            Email = item["Email"].ToString(),
                            PhoneNo = item["PhoneNo"].ToString(),
                          //  Password = item["Password"].ToString(),
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

        public List<MemberCommon> ListUsersProfile(MemberCommon common)
        {
            var list = new List<MemberCommon>();
            try
            {
                var sql = "EXEC proc_tblUsers ";
                sql += "@Flag = 'ListProfile'";
                sql += ",@UserName = " + dao.FilterString(common.UserName);
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var commonmember = new MemberCommon()
                        {
                            ID = Convert.ToInt32(item["ID"]),
                            FullName = item["FullName"].ToString(),
                            UserName = item["UserName"].ToString(),
                            Email = item["Email"].ToString(),
                            PhoneNo = item["PhoneNo"].ToString(),
                            Password = item["Password"].ToString(),
                            //  Password = item["Password"].ToString(),
                        };
                        sn++;
                        list.Add(commonmember);
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
