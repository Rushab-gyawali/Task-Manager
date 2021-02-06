using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
    public class EmailSMSCommon:Common
    {
        public string rowId { get; set; }
        public string uniqueId { get; set; }
        public string typeId { get; set; }
        public string mobileNo { get; set; }
        public string email { get; set; }
        public string msg { get; set; }
    }
}
