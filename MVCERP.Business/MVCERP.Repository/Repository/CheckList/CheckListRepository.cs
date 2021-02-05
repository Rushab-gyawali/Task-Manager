using MVCERP.Shared.Common;
using MVCERP.Shared.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.CheckList
{
    public class CheckListRepository : ICheckListRepository
    {
        RepositoryDao dao;
        public CheckListRepository()
        {

            dao = new RepositoryDao();
        }

        public List<ChecklistCommon> GetList(string User, string Search, int Pagesize)
        {
            var list = new List<ChecklistCommon>();
            try
            {
                var sql = "EXEC proc_tblDocChecklist ";
                sql += " @FLAG = " + dao.FilterString("A");
                sql += ",@User = " + dao.FilterString(User);
                sql += ",@Search = " + dao.FilterString(Search);
                sql += ",@Pagesize = " + dao.FilterString(Pagesize.ToString());

                var dt = dao.ExecuteDataTable(sql);
                if (null != dt)
                {
                    int sn = 1;
                    foreach (DataRow item in dt.Rows)
                    {
                        var common = new ChecklistCommon()
                        {
                            UniqueId = Convert.ToInt32(item["RowId"]),
                            User = item["CreatedBy"].ToString(),
                            DocType = item["DocType"].ToString(),
                            DocFullName = item["DocFullName"].ToString(),
                            Questionnaire = item["Questionnaire"].ToString(),
                            Remarks = item["Remarks"].ToString(),
                            CreatedDate = item["CreatedDate"].ToString(),
                        };
                        sn++;
                        list.Add(common);
                    }
                }
            }
            catch (Exception)
            {
                return list;
            }
            return list;
        }

        public List<ChecklistCommon> GetDoctypes(string docid, string user, string DocIdCode)
        {
            var sql = "EXEC proc_tblDocChecklist ";
            sql += " @FLAG = " + dao.FilterString(docid);
            sql += " ,@id = " + dao.FilterString(DocIdCode);
            var dt = dao.ExecuteDataTable(sql);

            return (List<ChecklistCommon>)Mapper.DataTableToClass<ChecklistCommon>(dt);
        }

        public DbResponse InsertQuestionnaire(string xml, string questionfor)
        {
            var sql = "EXEC proc_tblDocChecklist ";
            sql += " @FLAG = " + dao.FilterString("insert");
            sql += ",@xml = " + dao.FilterString(xml);
            sql += ",@questionfor = " + dao.FilterString(questionfor);
            return dao.ParseDbResponse(sql);
        }

        public DbResponse InsertQuestionnaireCheckList(string xml, string id, string user)
        {
            var sql = "EXEC proc_tblDocChecklist ";
            sql += " @FLAG = " + dao.FilterString("insertchecklist");
            sql += ",@xml = " + dao.FilterString(xml);
            sql += ",@id = " + dao.FilterString(id);
            sql += ",@user = " + dao.FilterString(user);
            return dao.ParseDbResponse(sql);
        }
    }
}
