using MVCERP.Shared.Common;
using MVCERP.Shared.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Notification
{
    public class NotificationRepository:INotificationRepository
    {
        RepositoryDao dao;
        private NotificationData nid;
        private NotificationDetail notificationdetail;
        NotificationSetupCommon data;
        public NotificationRepository()
        {
            dao = new RepositoryDao();
            nid = new NotificationData();
            notificationdetail = new NotificationDetail();
        }

      

        public NotificationData GetNotificationDetails(string user, string branch, string rowId)
        {
            string sql = "EXEC proc_tblNotification @flag='notificationdetails' ";
            sql += ", @user = " + dao.FilterString(user);
            sql += ", @branch = " + dao.FilterString(branch);
            sql += ", @rowId = " + rowId;
            var ds = dao.ExecuteDataset(sql);
            var dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                nid.RowId = Convert.ToInt32(dr["RowId"]);
                nid.NotificationText = dr["NotificationText"].ToString();
                nid.NotificationUser = dr["NotificationUser"].ToString();
                nid.NotificationDetails = dr["NotificationDetails"].ToString();
                nid.PostedDate = dr["PostedDate"].ToString();
            }
            return nid;
        }

       

        public DbResponse SaveNotification(string user, NotificationData data, StringBuilder sb, string branch)
        {
            string sql = "EXEC proc_tblNotification @flag='savenotification' ";
            sql += ", @notificationText = " + dao.FilterString(data.NotificationText);
            sql += ", @notificationDetails = " + dao.FilterString(data.NotificationDetails);
            sql += ", @user = " + dao.FilterString(user);
            sql += ", @branch = " + dao.FilterString(branch);
            return dao.ParseDbResponse(sql);
        }

        //public List<NotificationSetupCommon> GetSetUpList(string user, string Search, int Pagesize)
        //{
        //    var list = new List<NotificationSetupCommon>();
        //    try
        //    {
        //        string sql = "EXEC proc_tblNotification @flag='GetSetUpList' ";
        //        sql += ", @user = " + dao.FilterString(user);
        //        var dt = dao.ExecuteDataTable(sql);

        //        return (List<Shared.Common.NotificationSetupCommon>)Mapper.DataTableToClass<NotificationSetupCommon>(dt);
        //    }

        //    catch (Exception e)
        //    {
        //        return list;
        //    }
        //}

        public List<NotificationSetupCommon> GetListById(string user, string id)
        {
            var list = new List<NotificationSetupCommon>();
            try
            {
                var sql = "EXEC proc_tblNotification ";
                sql += " @FLAG = " + dao.FilterString("s");
                sql += ",@User = " + dao.FilterString(user);
                sql += ",@RowId = " + dao.FilterString(id);
                var dt = dao.ExecuteDataTable(sql);
                return (List<NotificationSetupCommon>)Mapper.DataTableToClass<NotificationSetupCommon>(dt);
            }
            catch (Exception)
            {
                return list;
            }
        }

        public List<NotificationSetupCommon> GetRecordById(string user, string branchId, string departmentId, string id)
        {
            var list = new List<NotificationSetupCommon>();
            try
            {
                var sql = "EXEC proc_tblNotification ";
                sql += " @FLAG = " + dao.FilterString("getList");
                sql += ",@User = " + dao.FilterString(user);
                sql += ",@branch = " + dao.FilterString(branchId);
                sql += ",@departmentId = " + dao.FilterString(departmentId);
                sql += ",@RowId = " + dao.FilterString(id);  
                var dt = dao.ExecuteDataTable(sql);
                return (List<NotificationSetupCommon>)Mapper.DataTableToClass<NotificationSetupCommon>(dt);
            }
            catch (Exception)
            {
                return list;
            }
        }

        public DbResponse SaveNotificationGroup(NotificationSetupCommon data)
        {
            string sql = "EXEC proc_tblNotification";
            sql += " @FLAG = " + dao.FilterString((data.UniqueId > 0 ? "U" : "I"));
            sql += ", @groupName = " + dao.FilterString(data.GroupName);
            sql += ", @branch = " + dao.FilterString(data.BranchId);
            sql += ", @departmentId = " + dao.FilterString(data.DepartmentId);
            sql += ", @userXML = " + dao.FilterString(data.UserXML);
            sql += ", @user = " + dao.FilterString(data.User);
            sql += ", @RowId = " + dao.FilterString(data.UniqueId.ToString()); 
            return dao.ParseDbResponse(sql);
        }

        public List<NotificationSetupCommon> GetList(string User, string Search, int Pagesize)
        {
            var list = new List<NotificationSetupCommon>();
            try
            {
                var sql = "EXEC proc_tblNotification ";
                sql += " @FLAG = " + dao.FilterString("A");
                sql += ",@User = " + dao.FilterString(User);
                sql += ",@Search = " + dao.FilterString(Search);
                sql += ",@Pagesize = " + dao.FilterString(Pagesize.ToString());
                var dt = dao.ExecuteDataTable(sql);
                return (List<NotificationSetupCommon>)Mapper.DataTableToClass<NotificationSetupCommon>(dt);
            }
            catch (Exception)
            {
                return list;
            }
        }

        public DbResponse MarkAsRead(string user, string id)
        {
            string sql = "EXEC proc_tblNotification @flag='markasread' ";
            sql += ", @user = " + dao.FilterString(user);
            sql += ", @notificationId = " + dao.FilterString(id);
            return dao.ParseDbResponse(sql);
        }

        public List<NotificationSetupCommon> GetNotificationType(string user) 
        {
            var list = new List<NotificationSetupCommon>();
            try
            {
                var sql = "EXEC proc_tblNotification ";
                sql += " @FLAG = " + dao.FilterString("GetNotificationType");
                sql += ",@User = " + dao.FilterString(user);
                var dt = dao.ExecuteDataTable(sql);
                return (List<NotificationSetupCommon>)Mapper.DataTableToClass<NotificationSetupCommon>(dt);
            }
            catch (Exception)
            {
                return list;
            }
        }

        public List<NotificationSetupCommon> GetNotificationGroup(string user) 
        {
            var list = new List<NotificationSetupCommon>();
            try
            {
                var sql = "EXEC proc_tblNotification ";
                sql += " @FLAG = " + dao.FilterString("GetNotificationGroup");
                sql += ",@User = " + dao.FilterString(user);
                var dt = dao.ExecuteDataTable(sql);
                return (List<NotificationSetupCommon>)Mapper.DataTableToClass<NotificationSetupCommon>(dt);
            }
            catch (Exception)
            {
                return list;
            }
        }

        public DbResponse QuickMessaging(NotificationSetupCommon data) 
        {
            string sql = "EXEC proc_tblNotification @flag='broadcastmsg'";
            sql += ", @notificationType = " + dao.FilterString(data.NotificationType);
            sql += ", @groupName = " + dao.FilterString(data.GroupName); 
            sql += ", @phoneNo = " + dao.FilterString(data.PhoneNo);
            sql += ", @content = " + dao.FilterString(data.Content);
            sql += ", @user = " + dao.FilterString(data.User);
            return dao.ParseDbResponse(sql);
        }

        public List<NotificationSetupCommon> GetMessages(string user)
        {
            var list = new List<NotificationSetupCommon>();
            try
            {
                var sql = "EXEC proc_tblNotification ";
                sql += " @FLAG = " + dao.FilterString("getMessages");
                sql += ",@User = " + dao.FilterString(user);
                var dt = dao.ExecuteDataTable(sql);
                return (List<NotificationSetupCommon>)Mapper.DataTableToClass<NotificationSetupCommon>(dt);
            }
            catch (Exception)
            {
                return list;
            }
        }
    }
}
