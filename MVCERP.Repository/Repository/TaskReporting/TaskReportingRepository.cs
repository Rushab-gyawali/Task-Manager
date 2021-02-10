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

        public DbResponse ChangeTask(string id, string task)
        {
            var sql = "EXEC proc_tblTaskManagers ";
            sql += "@Flag = 'C'";
            sql += ",@TaskId = " + dao.FilterString(id);
            sql += ",@Status = " + dao.FilterString(task);
            return dao.ParseDbResponse(sql);
        }

        public List<TaskReportingCommon> GetAllTask()
        {
            var list = new List<TaskReportingCommon>();
            try
            {
                var sql = "EXEC proc_tblTaskManagers ";
                sql += "@Flag = 'A'";
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
    }
}
