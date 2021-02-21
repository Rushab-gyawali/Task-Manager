using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
   public class SprintCommon:Common
    {
        public int RowId { get; set; }
        public string SprintId { get; set; }
        public string SprintName { get; set; }
        public bool SprintStatus { get; set; }
        public string StartedDate { get; set; }
        public string EndDate { get; set; }
        public string BacklogList { get; set; }
        public List<TaskObjectList> TaskObjectList { get; set; }
        public string taskobjlist { get; set; }
    }
    public class TaskObjectList
    {
        public string Id { get; set; }
        public string TaskName { get; set; }
        public string SprintId { get; set; }
        
    }
}
