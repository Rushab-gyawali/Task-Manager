using System.Collections.Generic;
using MVCERP.Repository.Repository.Common;
using MVCERP.Business.Business.Common;

namespace MVCERP.Business.Business.Common
{
    public class CommonBuss : ICommonBuss
    {
        MVCERP.Repository.Repository.Common.ICommonRepository repo;
        public CommonBuss(CommonRepository _repo)
        {
            repo = _repo;
        }

        public Dictionary<string, string> StaticDropdown(string ddlName)
        {
            return repo.StaticDropdown(ddlName);
        }

        public List<Dictionary<string, string>> SetDropdownList(string ddlName, string User)
        {
            return repo.DropdownList(ddlName,User);
        }

        public Dictionary<string, string> SetDropdown(string ddlName, string Param = "")
        {
            return repo.Dropdown(ddlName, Param);
        }


        public System.Data.DataSet GetDropDownLists(string flag)
        {
            return repo.GetDropDownLists(flag);
        }


        public List<object> LoadAutocomplete(string type, string param,string user)
        {
            return repo.LoadAutocomplete(type, param,user);
        }


        public object GetDropdownForJQuery(string flag, string param,string User)
        {
            return repo.GetDropdownForJQuery(flag,param,User);
        }

        public Dictionary<string, string> SetDropdownUser(string ddlName, string Param = "")
        {
            return repo.SetDropdownUser(ddlName,Param);
        }

        public Dictionary<string, string> SetDropdownRoles(string ddlName, string Param = "")
        {
            return repo.SetDropdownRoles(ddlName, Param);
        }
    }
}
