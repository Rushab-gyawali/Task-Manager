using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Sprint
{
    public class SprintRepository : ISprintRepository
    {
        RepositoryDao dao;
        public SprintRepository()
        {
            dao = new RepositoryDao();
        }

        public List<BacklogCommon> GetBacklogs()
        {
            var list = new List<BacklogCommon>();
            try
            {
                var sql = "EXEC PROC_TBLTASK ";
                sql += "@Flag = 'B'";
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var common = new BacklogCommon()
                        {
                            RowId = Convert.ToInt32(item["RowId"]),
                            BackLogId = item["BackLogId"].ToString(),
                            TaskName = item["TaskName"].ToString(),
                            TaskDescription = item["TaskDescription"].ToString(),
                            TaskReportedDate = item["TaskReportedDate"].ToString(),
                            DiscussionDate = item["DiscussionDate"].ToString(),
                            Owner = item["Owner"].ToString(),
                            ClientId = Convert.ToInt32(item["ClientId"]),
                            StoryPoint = Convert.ToInt32(item["StoryPoint"]),
                            CreatedBy = item["CreatedBy"].ToString(),
                            CreatedDate = item["CreatedBy"].ToString()
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

        public List<SprintCommon> GetSprints()
        {
            var list = new List<SprintCommon>();
            try
            {
                var sql = "EXEC PROC_TBLTASK ";
                sql += "@Flag = 'S'";
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var common = new SprintCommon()
                        {
                            RowId = Convert.ToInt32(item["RowId"]),
                            SprintId = item["SprintId"].ToString(),
                            SprintName = item["SprintName"].ToString(),
                            StartedDate = item["StartedDate"].ToString(),
                            EndDate = item["EndDate"].ToString(),
                            SprintStatus = Convert.ToBoolean(item["SprintStatus"]),
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

        public DbResponse GetSprintAndBacklog(SprintCommon sprint)
        {
            var sql = "exec PROC_TBLTASK ";
            sql += "@Flag = " + dao.FilterString((sprint.SprintId == null ? "Insert" : "Update"));
            sql += ",@SprintName = " + dao.FilterString(sprint.SprintName.ToString());
            sql += ",@StartedDate = " + dao.FilterString(sprint.StartedDate);
            sql += ",@EndDate = " + dao.FilterString(sprint.EndDate);
            sql += ",@BacklogList = " + dao.FilterString(sprint.BacklogList);         
             return dao.ParseDbResponse(sql);
            
        }

        public SprintCommon GetById(string SprintId)
        {
            var sql = "exec PROC_TBLTASK ";
            sql += "@Flag = 'GetbyId'";
            sql += ",@SprintId = " +dao.FilterString(SprintId);
            

            var dt = dao.ExecuteDataset(sql);
            SprintCommon sc = new SprintCommon();
            List<TaskObjectList> tol = new List<TaskObjectList>();
            var sprint = dt.Tables[0];
            if ((sprint.Rows.Count) > 0)
            {
                sc.SprintId = sprint.Rows[0]["SprintId"].ToString();
                sc.SprintName = sprint.Rows[0]["SprintName"].ToString();
                sc.StartedDate = sprint.Rows[0]["StartedDate"].ToString();
                sc.EndDate = sprint.Rows[0]["EndDate"].ToString();
            }
     

            var tasks = dt.Tables[1];
            int count = tasks.Rows.Count;
            for(int i=0;i<count;i++)
            {
                TaskObjectList obj = new TaskObjectList();
                obj.Id = tasks.Rows[i]["TaskId"].ToString();
                obj.TaskName = tasks.Rows[i]["TaskName"].ToString();
                obj.SprintId = tasks.Rows[i]["SprintId"].ToString();
                tol.Add(obj);
            }
            sc.TaskObjectList = tol;
            return sc;
        }
    }
    }
