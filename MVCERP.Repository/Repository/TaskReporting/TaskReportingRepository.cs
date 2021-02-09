using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCERP.Repository.Repository.TaskReporting
{
    public class TaskReportingRepository : ITaskReportingRepository
    {
        RepositoryDao dao;
        public TaskReportingRepository()
        {
            dao = new RepositoryDao();
        }

        public List<TaskReportingCommon> GetAllTask()
        {
            var list = new List<TaskReportingCommon>();
            try
            {
                var sql = "EXEC PROC_TASKMANAGER ";
                sql += "@Flag = 'List'";
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var common = new TaskReportingCommon()
                        {
                            RowId = Convert.ToInt32(item["RowId"]),
                            TaskId = item["TaskId"].ToString(),
                            TaskName = item["TaskName"].ToString(),
                            TaskDescription = item["TaskDescription"].ToString(),
                            CreatedBy = item["CreatedBy"].ToString(),
                            Status = item["Status"].ToString(),
                            TaskStartDate = item["TaskStartDate"].ToString(),
                            TaskEndDate = item["TaskEndDate"].ToString(),
                            CreatedDate = item["CreatedDate"].ToString(),
                            IsActive = Convert.ToBoolean(item["IsActive"])
                        };
                        sn++;
                        list.Add(common);
                    }
                }
                return list;
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        IEnumerable<TaskReportingCommon> ITaskReportingRepository.GetStatus()
        {
            var list = new List<TaskReportingCommon>();
            try
            {
                var sql = "EXEC proc_tblTaskManagers ";
                sql += "@FLAG = 'S'";
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var common = new TaskReportingCommon()
                        {
                            RowId = Convert.ToInt32(item["RowId"]),
                            TaskId = item["TaskId"].ToString(),
                            TaskName = item["TaskName"].ToString(),
                            TaskDescription = item["TaskDescription"].ToString(),
                            Status = item["Status"]?.ToString(),
                        };

                        sn++;
                        list.Add(common);
                    }
                }
                return list;
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TaskReportingCommon> StatusCount(TaskReportingCommon common)
        {
            var list = new List<TaskReportingCommon>();
            try
            {
                var sql = "EXEC PROC_TASKMANAGER ";
                sql += "@Flag = 'CountList'";
                sql += ",@Status = " + dao.FilterString(common.Status);
                sql += ",@CreatedBy = " + dao.FilterString(common.CreatedBy);
                sql += ",@CountStatus = " + dao.FilterString(common.Status);
                sql += ",@StatusList = " + dao.FilterString(common.StatusListCount);
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var commontask = new TaskReportingCommon()
                        {
                            StatusCount = Convert.ToInt32(item["StatusCount"]),
                        };
                        sn++;
                        list.Add(commontask);
                    }
                }
                return list;
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TaskReportingCommon> StatusList(string status, string user)
        {
            var list = new List<TaskReportingCommon>();
            try
            {
                var sql = "EXEC PROC_TASKMANAGER ";
                sql += "@Flag = 'ListStatus'";
                sql += ",@Status = " + dao.FilterString(status);
                sql += ",@CreatedBy = " + dao.FilterString(user);
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var commontask = new TaskReportingCommon()
                        {
                            RowId = Convert.ToInt32(item["RowId"]),
                            TaskId = item["TaskId"].ToString(),
                            TaskName = item["TaskName"].ToString(),
                            TaskDescription = item["TaskDescription"].ToString(),
                            CreatedBy = item["CreatedBy"].ToString(),
                            Status = item["Status"].ToString(),
                            TaskStartDate = item["TaskStartDate"].ToString(),
                            TaskEndDate = item["TaskEndDate"].ToString(),
                            CreatedDate = item["CreatedDate"].ToString(),
                            IsActive = Convert.ToBoolean(item["IsActive"])
                        };
                        sn++;
                        list.Add(commontask);
                    }
                }
                return list;
            }

            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
