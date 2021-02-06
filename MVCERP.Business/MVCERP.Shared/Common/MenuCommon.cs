using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
    public class UserMenuFunctions {
        public List<MenuCommon> menu { get; set; }
        public List<UserFunctionId> function { get; set; }
    }
    public class MenuCommon
   {
       public string MenuName { get; set; }
       public string linkPage { get; set; }
       public string MenuGroup { get; set; }
       public string ParentGroup { get; set; }
       public string Class { get; set; }
   }
    public class UserFunctionId {
        public string FunctionId { get; set; }
    }
}
