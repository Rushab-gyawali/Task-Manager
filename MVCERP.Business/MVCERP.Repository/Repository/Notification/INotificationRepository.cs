using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Notification
{
    public interface INotificationRepository
    {
        NotificationData GetNotificationDetails(string user, string branch, string rowId);

        DbResponse SaveNotification(string user, NotificationData data, StringBuilder sb, string branch);

        //List<NotificationSetupCommon> GetSetUpList(string user, string Search, int Pagesize);
        List<NotificationSetupCommon> GetListById(string user, string id);
        List<NotificationSetupCommon> GetRecordById(string user, string branchId, string departmentId, string id);
        DbResponse SaveNotificationGroup(NotificationSetupCommon data);
        List<NotificationSetupCommon> GetList(string User, string Search, int Pagesize);
        DbResponse MarkAsRead(string user, string id);



        List<NotificationSetupCommon> GetNotificationType(string user);
        List<NotificationSetupCommon> GetNotificationGroup(string user);
        DbResponse QuickMessaging(NotificationSetupCommon data);
        List<NotificationSetupCommon> GetMessages(string user);
    }
}
