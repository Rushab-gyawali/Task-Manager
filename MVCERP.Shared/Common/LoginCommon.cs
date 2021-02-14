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
   
}
