using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
    public class AgentEndorsementCommon : Common
    {
       // public int UniqueId { get; set; }
        public string AgentCode { get; set; }
        public string DOB { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string LicenseNo { get; set; }
        public string LicenseExpiryDate { get; set; }
        public string LicenseIssueDate { get; set; }
        public string BankAcNo { get; set; }
        public string BankCode { get; set; }
        public string BankBranch { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public string NomineeAddress { get; set; }
        public string AccountNo { get; set; }
        public string User { get; set; }
        public string CreatedBy { get; set; }
        public string Branch { get; set; }
        public string PANNo { get; set; }
        public string NepName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string WebSite { get; set; }
        public string Address { get; set; }
        public string NepAddress { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string Email { get; set; }
        public string TemporaryAddress { get; set; }
        public string Occupation { get; set; }
        public string Qualification { get; set; }

        //added for traineedetails
        public string Gender { get; set; }
        public string CorporateName { get; set; }

        public string DOBEng { get; set; }


        public string ProvinceID { get; set; }
        public string DistrictID { get; set; }
        public string LocalUnitId { get; set; }
        public string WardId { get; set; }

        public string IDType { get; set; }
        public string IDNo { get; set; }
        public string IDIssuePlace { get; set; }
        public string TrainingId { get; set; }
        public string SuperiorID { get; set; }
        public string Remarks { get; set; }
        public string PersonalAgentCode { get; set; }
        public string IsActive { get; set; }
    }
}
