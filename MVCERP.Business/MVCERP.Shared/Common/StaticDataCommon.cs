using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
   public class StaticDataCommon:Common
    {
        public string TypeCode { get; set; }
        public string DataCode { get; set; }
        public string DataValue { get; set; }
        public string NepValue { get; set; }
    }
}
