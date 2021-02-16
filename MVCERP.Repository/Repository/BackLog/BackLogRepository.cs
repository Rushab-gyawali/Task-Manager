using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.BackLog
{
    
     public class BackLogRepository : IBackLogRepository
    {
        RepositoryDao dao;

        public BackLogRepository()
        {
            dao = new RepositoryDao();
        }


        public DbResponse AddBackLogTask(BackLogCommon setup)
        {
            var sql = "EXEC proc_BackLog ";
            sql += "@Flag = " + dao.FilterString((setup.BackLogId == null ? "Insert" : "Update"));
            sql += ",@TaskName = " + dao.FilterString(setup.TaskName);
            sql += ",@TaskDescription = " + dao.FilterString(setup.TaskDescription);
            sql += ",@TaskReportedDate = " + dao.FilterString(setup.TaskReportedDate);
            sql += ",@DiscussionDate = " + dao.FilterString(setup.DiscussionDate);
            sql += ",@Owner = " + dao.FilterString(setup.Owner);
            sql += ",@ClientId = " + setup.ClientId;
            sql += ",@StoryPoint = " + dao.FilterString(setup.StoryPoint);
            sql += ",@CreatedBy = " + dao.FilterString(setup.CreatedBy);
            
            if (setup.BackLogId == null)
            {
                return dao.ParseDbResponse(sql);
            }
            else
            {
                sql += ",@BackLogId = " + dao.FilterString(setup.BackLogId.ToString());
                return dao.ParseDbResponse(sql);
            }
        }

        public DbResponse DeleteTask(BackLogCommon common)
        {
            var sql = "exec proc_BackLog ";
            sql += "@Flag = 'Delete'";
            sql += ",@BackLogId  =" + common.BackLogId;
            return dao.ParseDbResponse(sql);
        }

        public List<BackLogCommon> GetById(string ID)
        {
            var list = new List<BackLogCommon>();
            try
            {
                var sql = "Exec proc_BackLog ";
                sql += "@Flag = 'GetById'";
                sql += ",@BackLogId = " + dao.FilterString(ID);

                var dt = dao.ExecuteDataTable(sql);
                if (null != dt)
                {
                    int sn = 1;
                    foreach (DataRow item in dt.Rows)
                    {
                        var common = new BackLogCommon
                        {
                            BackLogId = item["BackLogId"].ToString(),
                            TaskName = item["TaskName"].ToString(),
                            TaskDescription = item["TaskDescription"].ToString(),
                            TaskReportedDate = item["TaskReportedDate"].ToString(),
                            DiscussionDate = item["DiscussionDate"].ToString(),
                            Owner = item["Owner"].ToString(),
                            ClientId = Convert.ToInt32(item["ClientId"]),
                            StoryPoint = item["StoryPoint"].ToString(),
                            CreatedBy = item["CreatedBy"].ToString(),
                            CreatedDate = item["CreatedDate"].ToString(),


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

        public List<BackLogCommon> ListBackLogTask()
        {
            var list = new List<BackLogCommon>();
            try
            {
                var sql = "EXEC proc_BackLog ";
                sql += "@Flag = 'List'";
                var dt = dao.ExecuteDataTable(sql);

                if (null != dt)
                {
                    int sn = 1;
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        var common = new BackLogCommon()
                        {
                            
                            BackLogId = item["BackLogId"].ToString(),
                            TaskName = item["TaskName"].ToString(),
                            TaskDescription = item["TaskDescription"].ToString(),
                            TaskReportedDate = item["TaskReportedDate"].ToString(),
                            DiscussionDate = item["DiscussionDate"].ToString(),
                            Owner = item["Owner"].ToString(),
                            ClientId = Convert.ToInt32(item["ClientId"]),
                            StoryPoint = item["StoryPoint"].ToString(),
                            CreatedBy = item["CreatedBy"].ToString(),
                            CreatedDate = item["CreatedDate"].ToString(),
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
