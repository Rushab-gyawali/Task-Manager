using iSolutionLife.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSolutionLife.Web.Services.Utilities
{
   public interface IUtilities
    {
       DbResponse SendMail(string To,string Cc,string Subject,string Body);
    }
}
