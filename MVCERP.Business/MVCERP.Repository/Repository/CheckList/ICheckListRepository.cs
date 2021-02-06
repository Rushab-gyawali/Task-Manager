using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.CheckList
{
    public interface ICheckListRepository
    {
        List<ChecklistCommon> GetDoctypes(string docid, string user, string DocIdCode);
        DbResponse InsertQuestionnaire(string question, string questionfor);

        DbResponse InsertQuestionnaireCheckList(string xml, string id, string user);
        List<ChecklistCommon> GetList(string User, string Search, int Pagesize);
    }
}
