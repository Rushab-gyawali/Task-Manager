using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Library.NepaliCalender
{
   public interface IConvertToNepali
    {
        NepaliDate GetNepaliDate(DateTime enDate);
    }
}
