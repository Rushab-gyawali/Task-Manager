﻿using System;
using System.Collections.Generic;
using System.Data;
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

        public string AssignTo { get; set; }

        //for count status
        public int StatusCount { get; set; }
        public string StatusListCount { get; set; }

        //report of user
        public string ReportTitle { get; set; }
        public string ExcelLink { get; set; }
        public string ReportName { get; set; }
        public bool ShowHeader { get; set; }
        public DataTable ReportData { get; set; }
        public DataTable ReportHeader { get; set; }
        

    }
}
