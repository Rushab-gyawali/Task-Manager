using System;
using System.Collections.Generic;
using System.Data;
using MVCERP.Repository.Repository.Common;
using MVCERP.Shared.Common;

namespace MVCERP.Repository.Repository.Common
{
    public class CommonRepository : ICommonRepository
    {
        RepositoryDao dao;
        public CommonRepository()
         {
            dao = new RepositoryDao();
        }
        public Dictionary<string, string> StaticDropdown(string ddlName)
        {
            var sql = "exec proc_DropdownList @flag=" + dao.FilterString("StaticData");
            sql += ", @StaticType = " + dao.FilterString(ddlName);
            return dao.ParseDictionary(sql);
        }

        public List<Dictionary<string, string>> DropdownList(string ddlName,string User)
        {
            var list = new List<Dictionary<string, string>>();
            var sql = "exec proc_DDLSet @flag=" + dao.FilterString(ddlName);
            sql += ", @User = " + dao.FilterString(User);
            var ds = dao.ExecuteDataset(sql);
            if (null != ds)
            {
                foreach (DataTable dt in ds.Tables )
                {
                    var dictionary = dao.ParseDictionary(dt);
                    list.Add(dictionary);
                }
            }
                return list;
        }


        public Dictionary<string, string> Dropdown(string ddlName, string Param)
        {
               var sql = "exec PROC_DROPDOWNLIST @Flag=" + dao.FilterString(ddlName);
               sql += ", @Param = " + dao.FilterString(Param); 
                var dt = dao.ExecuteDataTable(sql);
               return dao.ParseDictionary(sql);
        }


        public DataSet GetDropDownLists(string flag)
        {
            var sql = "exec proc_DDLSet @flag=" + dao.FilterString(flag);

            return dao.ExecuteDataset(sql);
        }

        public List<object> LoadAutocomplete(string type, string param,string user)
        {
            var sql = "exec proc_Autocomplete @flag=" + dao.FilterString(type);
            sql += ", @param1 = " + dao.FilterString(param);
            sql += ", @User = " + dao.FilterString(user);
            return dao.DropdownList(sql);
        }


        public object GetDropdownForJQuery(string flag, string param, string User)
        {
            var sql = "exec proc_DropdownList @flag=" + dao.FilterString(flag);
            sql += ", @Param = " + dao.FilterString(param);
            sql += ", @User = " + dao.FilterString(User);
            return dao.DropdownList(sql);
        }

        public Dictionary<string, string> SetDropdownUser(string ddlName, string Param = "")
        {
            var sql = "exec [dbo].[proc_tblUsers] @Flag=" + dao.FilterString(ddlName);
            var dt = dao.ExecuteDataTable(sql);
            return dao.ParseDictionary(sql);
        }
    }
}
