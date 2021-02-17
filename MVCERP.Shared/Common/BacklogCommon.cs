using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
   public class BacklogCommon:Common
    {
        public long RowId { get; set; }
        public string BackLogId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskReportedDate { get; set; }
        public string DiscussionDate { get; set; }
        public string Owner { get; set; }
        public long ClientId { get; set; }
        public int StoryPoint { get; set; }
    }
}
