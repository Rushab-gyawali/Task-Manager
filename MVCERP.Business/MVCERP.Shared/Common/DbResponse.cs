using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
   public class DbResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
        public string Extra { get; set; }

        public void SetError(int errorCode, string msg, string id)
        {
            ErrorCode = errorCode;
            Message = msg;
            Id = id;
        }
    }
}
