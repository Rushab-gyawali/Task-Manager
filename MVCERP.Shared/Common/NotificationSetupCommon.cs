using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
    public class NotificationSetupCommon :Common
    {
        public string Department { get; set; }
        public string GroupName { get; set; } 
        public string Branch { get; set; }
        public string BranchId { get; set; }
        public string DepartmentId { get; set; }
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public string UserID { get; set; }
        public string IsActive { get; set; }
        public string UserXML { get; set; }
        public string AlreadyExist { get; set; }
        public string Content { get; set; }
        public string NotificationType { get; set; }  


        public string Value { get; set; }
        public string Code { get; set; }  
    }
}
