using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace MVCERP.Web.Models
{
    public class SprintAndBacklogModel
    {
      
        public TaskObjectList TaskObjectList { get; set; }
  
        //public string[] TaskObjectList { get; set; }
        public string SprintName { get; set; }
        public string StartedDate { get; set; }
        public string EndDate { get; set; }
        public string BacklogList { get; set; }
    }
    public class TaskObjectList
    {
        public string Id { get; set; }
        public string TaskName { get; set; }
    }
}