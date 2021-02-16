using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Models
{
    public class PermissionModel
    {
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
        public IEnumerable<SelectListItem> ModelList { get; set; }
    }
}