using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Models
{
    public class PermissionModel : RoleModel
    {
        public string ParentMenu { get; set; }
        public string Menu { get; set; }
        public string URL { get; set; }
    }
}