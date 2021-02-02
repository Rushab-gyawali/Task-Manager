using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
   public  class UserCommon:Common
    {
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string UserPwd { get; set; }
        public string ForcePwdChange { get; set; }
        public int Branch { get; set; }
        public string BranchName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string IsAdmin { get; set; }
        public string IsActive { get; set; }
        public bool AllowApproveDate { get; set; }
        public bool AllowBackDate { get; set; }
        public string IsAdminUser { get; set; }
        public string CompanyNameNep { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddressNep { get; set; }
        public string CompanyAddress { get; set; }
        public string sysDate { get; set; }
        public object FilterCount { get; set; }
    }
}
