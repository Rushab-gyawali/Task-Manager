using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.Common
{
   public interface ICommonRepository
    {
       Dictionary<string, string> StaticDropdown(string ddlName);

       List<Dictionary<string, string>> DropdownList(string ddlName, string User);

       Dictionary<string, string> Dropdown(string ddlName, string Param);

       System.Data.DataSet GetDropDownLists(string flag);

       List<object> LoadAutocomplete(string type, string param,string user);

       object GetDropdownForJQuery(string flag, string param, string User);
    }
}
