using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
    public class PermissionCommon :RoleCommon
    {
        public string ParentMenu { get; set; }
        public string Menu { get; set; }
        public int MenuId { get; set; }
        public bool MenuCheck { get; set; }
        public string URL { get; set; }
        public string UserName { get; set; }
        public string FK_ID { get; set; }
        public string FK_RoleId { get; set; }
        public string FK_MenuId { get; set; }
    }
}
