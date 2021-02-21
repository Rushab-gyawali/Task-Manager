using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
    public class PermissionCommon
    {
        public string ParentMenu { get; set; }
        public string Menu { get; set; }
        public int MenuId { get; set; }
        public string URL { get; set; }
        public string UserName { get; set; }
        public string FK_ID { get; set; }
        public string FK_RoleId { get; set; }
        public string FK_MenuId { get; set; }
        public string RoleName { get; set; }
        public string Id { get; set; }
        public string IsActive { get; set; }
        public string RolePermission { get; set; }  
    }    
    public class RolePermission 
    {
        public string Id { get; set; }
        public string RoleID { get; set; }
    }

}
