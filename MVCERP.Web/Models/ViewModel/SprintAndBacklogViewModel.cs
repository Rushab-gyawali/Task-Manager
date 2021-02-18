using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCERP.Web.Models.ViewModel
{
    public class SprintAndBacklogViewModel
    {
        public List<BacklogModel> Backlogs { get; set; }
        public string Sprint { get; set; }
        public string SprintName { get; set; }
        public string StartedDate { get; set; }
        public string EndDate { get; set; }
        public string BacklogList { get; set; }
        public string SprintId { get; set; }
        

        //  public List<TaskObjectList> TaskObjectList { get; set; }


    }


}