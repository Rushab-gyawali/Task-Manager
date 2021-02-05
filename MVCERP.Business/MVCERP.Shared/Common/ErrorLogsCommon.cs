using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
   public class ErrorLogsCommon:Common
    {
        public string ErrorPage { get; set; }
        public string ErrorMsg { get; set; }
        public string ErrorDetail { get; set; }
        //public string CreatedBy { get; set; }
        //public string CreatedDate { get; set; }
    }
}
