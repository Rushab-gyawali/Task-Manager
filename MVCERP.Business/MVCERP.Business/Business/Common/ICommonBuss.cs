using System.Collections.Generic;
using System.Data;

namespace MVCERP.Business.Business.Common
{
   public interface ICommonBuss
    {
        Dictionary<string, string> StaticDropdown(string ddlName);
        List<Dictionary<string, string>> SetDropdownList(string ddlName,string User);
        Dictionary<string, string> SetDropdown(string ddlName, string Param = "");

        DataSet GetDropDownLists(string flag);

        List<object> LoadAutocomplete(string type, string param,string user);

        object GetDropdownForJQuery(string flag, string param,string User);
    }
}
