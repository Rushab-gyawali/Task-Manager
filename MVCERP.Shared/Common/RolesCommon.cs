using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
   public class RolesCommon : Common
    {
        public string RoleName { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
    public class RolesDetails
    {
        public string Sno { get; set; }
        public string menuGroup { get; set; }
        public string menuName { get; set; }
        public string parentFunctionId { get; set; }
        public string ParentGroup { get; set; }
        public string functionId { get; set; }
        public string functionName { get; set; }
        public int RoleId { get; set; }
        public string AddEdit { get; set; }
        public string DeleteId { get; set; }
        public string ViewId { get; set; }
        public string BreadCum { get; set; }
        public string hasChecked { get; set; }

        public string groupPosition { get; set; }
    }

}
