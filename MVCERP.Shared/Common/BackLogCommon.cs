﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
    class BackLogCommon : Common
    {
        public int BackLogId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskReportedDate { get; set; }
        public DateTime? DiscussionDate { get; set; }
        public string Owner { get; set; }
        public int ClientId { get; set; }
        public string StoryPoint { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }


    }
}
