using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
    public class LogsCommon
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Ip { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime LoginTime { get; set; }
        public string BrowserInfo { get; set; }
        public int Status { get; set; }
    }
}
