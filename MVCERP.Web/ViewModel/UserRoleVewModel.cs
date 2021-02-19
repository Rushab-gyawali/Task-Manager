using MVCERP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCERP.Web.ViewModel
{
    public class UserRoleVewModel
    {
        public MemberModel members  { get; set; }
        public List<RoleModel> roles { get; set; }
    } 
}