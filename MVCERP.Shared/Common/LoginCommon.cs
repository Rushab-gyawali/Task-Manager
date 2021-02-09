using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
   public class LoginCommon
    {


        public string UserName { get; set; }

        public string Password { get; set; }
        public string Ip { get; set; }
        public string BrowserInfo { get; set; }
    }
   public class ChangePassword
   {
       public string User { get; set; }
       public string UserName { get; set; }
       public string OldPassword { get; set; }
       public string NewPassword { get; set; }
       public string ConfirmPassword { get; set; }

   }
}
