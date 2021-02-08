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
        public string Status { get; set; } 
        public string TaskStartDate { get; set; }
        public string TaskEndDate { get; set; }
<<<<<<< HEAD
        
=======
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string AssignTo { get; set; }

>>>>>>> 011b1b73ea1fc4b9ebbbe8c6c13a7a035989b58a
    }
}
