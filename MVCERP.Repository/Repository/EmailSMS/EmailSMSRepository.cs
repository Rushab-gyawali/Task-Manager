using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.EmailSMS
{
    public class EmailSMSRepository:IEmailSMSRepository
    {
        RepositoryDao dao;
        public EmailSMSRepository()
        {
            dao = new RepositoryDao();
        }
        public List<EmailSMSCommon> GetSMSList(string user)
        {
            var list = new List<EmailSMSCommon>();
            try
            {
                var sql = "EXEC proc_tblSMSQueue ";
                sql += " @flag = " + dao.FilterString("s");
                sql += ",@User = " + dao.FilterString(user);
               

                var dt = dao.ExecuteDataTable(sql);
                if (null != dt)
                {
                    int sn = 1;
                    foreach (DataRow item in dt.Rows)
                    {
                        var common = new EmailSMSCommon()
                        {
                            
                            UniqueId = Convert.ToInt32(item["rowId"].ToString()),
                            typeId = item["typeId"].ToString(),
                            mobileNo = item["mobileNo"].ToString(),
                            msg = item["msg"].ToString(),
                            User = item["createdBy"].ToString(),
                        };
                        sn++;
                        list.Add(common);
                    }
                }
            }
            catch (Exception)
            {
                return list;
            }
            return list;
        }
        public List<EmailSMSCommon> GetSMS(string user, int id)
        {
            var list = new List<EmailSMSCommon>();
            try
            {
                var sql = "EXEC proc_tblSMSQueue ";
                sql += " @flag = " + dao.FilterString("s-id");
                sql += ",@User = " + dao.FilterString(user);
                sql += ",@rowId = " + dao.FilterString(id.ToString());

                var dt = dao.ExecuteDataTable(sql);
                if (null != dt)
                {
                    int sn = 1;
                    foreach (DataRow item in dt.Rows)
                    {
                        var common = new EmailSMSCommon()
                        {
                            
                            UniqueId = Convert.ToInt32(item["rowId"].ToString()),
                            typeId = item["typeId"].ToString(),
                            mobileNo = item["mobileNo"].ToString(),
                            msg = item["msg"].ToString(),
                            User = item["createdBy"].ToString(),
                        };
                        sn++;
                        list.Add(common);
                    }
                }
            }
            catch (Exception)
            {
                return list;
            }
            return list;
        }
        public List<EmailSMSCommon> GetEmailList(string user)
        {
            var list = new List<EmailSMSCommon>();
            try
            {
                var sql = "EXEC proc_tblEmailQueue ";
                sql += " @flag = " + dao.FilterString("s");
                sql += ",@User = " + dao.FilterString(user);
               
                var dt = dao.ExecuteDataTable(sql);
                if (null != dt)
                {
                    int sn = 1;
                    foreach (DataRow item in dt.Rows)
                    {
                        var common = new EmailSMSCommon()
                        {
                            
                            UniqueId = Convert.ToInt32(item["rowId"].ToString()),
                            typeId = item["typeId"].ToString(),
                            email = item["email"].ToString(),
                            msg = item["msg"].ToString(),
                            User = item["createdBy"].ToString(),
                        };
                        sn++;
                        list.Add(common);
                    }
                }
            }
            catch (Exception)
            {
                return list;
            }
            return list;
        }
        public List<EmailSMSCommon> GetEmail(string user, int id)
        {
            var list = new List<EmailSMSCommon>();
            try
            {
                var sql = "EXEC proc_tblEmailQueue ";
                sql += " @flag = " + dao.FilterString("s");
                sql += ",@User = " + dao.FilterString(user);
                sql += ",@rowId = " + dao.FilterString(id.ToString());

                var dt = dao.ExecuteDataTable(sql);
                if (null != dt)
                {
                    int sn = 1;
                    foreach (DataRow item in dt.Rows)
                    {
                        var common = new EmailSMSCommon()
                        {
                            
                            UniqueId = Convert.ToInt32(item["rowId"].ToString()),
                            typeId = item["typeId"].ToString(),
                            email = item["email"].ToString(),
                            msg = item["msg"].ToString(),
                            User = item["createdBy"].ToString(),
                        };
                        sn++;
                        list.Add(common);
                    }
                }
            }
            catch (Exception)
            {
                return list;
            }
            return list;
        }
        public DbResponse UpdateSMSQueue(string User, int id)
        {
            var sql = "EXEC proc_tblSMSQueue ";
            sql += " @flag = " + dao.FilterString("u");
            sql += ",@User = " + dao.FilterString(User);
            sql += ",@rowID = " + dao.FilterString(id.ToString());
            return dao.ParseDbResponse(sql);
        }
        public DbResponse UpdateEmailQueue(string User, int id)
        {
            var sql = "EXEC proc_tblEmailQueue ";
            sql += " @flag = " + dao.FilterString("u");
            sql += ",@User = " + dao.FilterString(User);
            sql += ",@rowID = " + dao.FilterString(id.ToString());
            return dao.ParseDbResponse(sql);
        }
    }
}
