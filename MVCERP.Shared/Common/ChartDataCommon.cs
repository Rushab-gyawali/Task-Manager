using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
    public class ChartDataCommon
    {
        public IList<ChartData> ChartDataList;
    }
    public class ProgressBarCommon
    {
        public IList<ProgressBarData> ProgressBarList;

    }
    public class ScheduleTaskCommon
    {
        public IList<ScheduleTaskData> ScheduleTaskDataList;

    }
    public class AgentActivityCommon
    {
        public IList<AgentActivityData> AgentActivityDataList;
    }
    public class PlanHistoryCommon
    {
        public IList<PlanHistoryData> PlanHistoryDataList;
    }
    public class ChartData
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
    public class ProgressBarData
    {
        public string label { get; set; }
        public string value { get; set; }
        public string color { get; set; }
    }
    public class ScheduleTaskData
    {
        public string topic { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public string color { get; set; }
    }
    public class AgentActivityData
    {
        public string month { get; set; }
        public string day { get; set; }
        public string date { get; set; }
        public string agentname { get; set; }
        public string activitytext { get; set; }
        public string activitystatus { get; set; }
        public string dayscount { get; set; }
    }
    public class PlanHistoryData
    {
        public string date { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string range { get; set; }
    }
}
