using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.TaskManager
{
    public class TaskManagerRepository : ITaskManagerRepository
    {
        RepositoryDao dao;

        public TaskManagerRepository()
        {
            dao = new RepositoryDao();
        }

        public List<TaskReportingCommon> GetById(string TaskId)
        {
            var list = new List<TaskReportingCommon>();
            try {
                var sql = "EXEC PROC_TASKMANAGER ";
                sql += "@Flag = 'ListUpdate'";
                sql += ",@TaskId = " + dao.FilterString(TaskId);

                var dt = dao.ExecuteDataTable(sql);
                if (null != dt)
                {
                    int sn = 1;
                    foreach (DataRow item in dt.Rows)
                    {
                        var common = new TaskReportingCommon
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
                            AssignTo = item["AssignTo"].ToString(),
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

        public DbResponse TaskManager(TaskReportingCommon common)
        {
            var sql = "exec PROC_TASKMANAGER ";
            sql += "@Flag = " + dao.FilterString((common.TaskId == null ? "Insert" : "Update"));
            sql += ",@TaskName = " + dao.FilterString(common.TaskName.ToString());
            sql += ",@TaskStartDate = " + dao.FilterString(common.TaskStartDate);
            sql += ",@TaskEndDate = " + dao.FilterString(common.TaskEndDate);
            sql += ",@TaskDescription = " + dao.FilterString(common.TaskDescription.ToString());
            sql += ",@Status = " + dao.FilterString(common.Status.ToString());
            sql += ",@CreatedBy = " + dao.FilterString(common.CreatedBy.ToString());
            sql += ",@AssignTo = " + dao.FilterString(common.AssignTo.ToString());
            if (common.TaskId == null)
            {
                return dao.ParseDbResponse(sql);
            }
            else
            {
                sql += ",@TaskId = " + dao.FilterString(common.TaskId.ToString());
                return dao.ParseDbResponse(sql);
            }
        }
    }
}
