using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCERP.Web.Models
{
    public class SprintModel
    {
        public int RowId { get; set; }
        public string SprintId { get; set; }
        public string SprintName { get; set; }
        public bool SprintStatus { get; set; }
        public string StartedDate { get; set; }
        public string EndDate { get; set; }

    }
}