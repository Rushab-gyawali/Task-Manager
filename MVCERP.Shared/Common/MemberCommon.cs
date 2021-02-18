using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MVCERP.Shared.Common
{
   public class MemberCommon : Common
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public bool AdminRight { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public DataTable UserData { get; set; }


    }
    public class ChangePassword
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
    public class ChangePasswordCommon : Common
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }


   


}
