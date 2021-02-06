using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
   public class TaskReportingCommon:Common
    {
        public int RowId { get; set; }
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public bool IsActive { get; set; }
        public string  Status { get; set; } 
        public string TaskStartDate { get; set; }
        public string TaskEndDate { get; set; }


    }
}
