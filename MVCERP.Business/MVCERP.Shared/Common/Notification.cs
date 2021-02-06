using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
    public class NotificationData
    {
        public int RowId { get; set; }
        public int NotificationId { get; set; }
        public string NotificationText { get; set; }
        public string NotificationDetails { get; set; }
        public string NotificationLink { get; set; }
        public string NotificationUser { get; set; }
        public string NotificationUserGroup { get; set; }
        public string RoleId { get; set; }
        public string IsRead { get; set; }
        public string PostedDate { get; set; }
        public string UserId { get; set; }

        // public IList<Notification> notifications { get; set; }
    }
    public class NotificationDetail
    {
        public string NotificationCount { get; set; }
        public IList<NotificationData> notifications { get; set; }
    }
}
